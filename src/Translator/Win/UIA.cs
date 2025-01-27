using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gekka.Language.Translator.Win
{
    using UIAutomationClient;

    static class UIA
    {
        static UIA()
        {
            dicControlType = new Dictionary<int, string>();

            foreach (var fi in typeof(UIA_ControlTypeIds).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            {
                dicControlType.Add((int)fi.GetValue(null)!, fi.Name);
            }
        }

        private static Dictionary<int, string> dicControlType;

        public static bool SelectContextMenu(IntPtr hwnd, Func<string, bool> callback)
        {
            List<object> list = new List<object>();
            UIAutomationClient.CUIAutomation8 uia = new UIAutomationClient.CUIAutomation8();
            list.Add(uia);
            try
            {

                var conWnd = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_NativeWindowHandlePropertyId, hwnd); list.Add(conWnd);
                var conMenu = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_MenuControlTypeId); list.Add(conMenu);
                var conMenuItem = uia.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_MenuItemControlTypeId); list.Add(conMenuItem);
                var conTRUE = uia.CreateTrueCondition(); list.Add(conTRUE);

                var root = uia.GetRootElement(); list.Add(root);
                var wnd = root.FindFirst(TreeScope.TreeScope_Children, conWnd);

                var menu = wnd.FindFirst(TreeScope.TreeScope_Subtree, conMenu); list.Add(menu);

                var arItems = menu.FindAll(TreeScope.TreeScope_Subtree, conMenuItem); list.Add(arItems);

                foreach (var mi in arItems.GetEnumerable())
                {
                    list.Add(mi);

                    string name = mi.CurrentName;
                    System.Diagnostics.Debug.WriteLine(name);
                    if (callback.Invoke(name))
                    {
                        if (mi.GetCurrentPattern(UIAutomationClient.UIA_PatternIds.UIA_InvokePatternId) is UIAutomationClient.IUIAutomationInvokePattern inv)
                        {
                            try
                            {
                                inv.Invoke();
                                return true;
                            }
                            catch
                            {
                            }
                            finally
                            {
                                inv.ReleaseCom();
                            }

                        }
                        return false;
                    }

                }

                return false;
            }
            catch
            {
                return false;
            }
            finally
            {
                list.ReleaseCom();
            }
        }

        private static void EnumTree(UIAutomationClient.IUIAutomationElement elem, UIAutomationClient.IUIAutomationCondition TRUE, int stack)
        {
            var ar = elem.FindAll(TreeScope.TreeScope_Children, TRUE);
            try
            {
                foreach (var e in ar.GetEnumerable())
                {
                    try
                    {

                        int ct = e.CurrentControlType;
                        string? sct = "";
                        try
                        {
                            if (!dicControlType.TryGetValue(ct, out sct))
                            {
                                sct = ct.ToString();
                            }
                        }
                        catch
                        {
                        }


                        System.Diagnostics.Debug.WriteLine("".PadRight(stack, '\t') + sct + "\t" + e.CurrentClassName + "\t" + e.CurrentNativeWindowHandle.ToInt32().ToString("X"));
                        EnumTree(e, TRUE, stack + 1);
                    }
                    finally
                    {
                        e.ReleaseCom();
                    }

                }
            }
            finally
            {
                ar.ReleaseCom();
            }

        }


        static IEnumerable<IUIAutomationElement> GetEnumerable(this IUIAutomationElementArray ar)
        {
            int count = ar.Length;
            for (int i = 0; i < count; i++)
            {
                var e = ar.GetElement(i);
                yield return e;
            }
        }

        static void ReleaseCom(this IEnumerable<object> ie)
        {
            foreach (var o in ie)
            {
                o.ReleaseCom();
            }
        }

        static void ReleaseCom(this object o)
        {
            if (o?.GetType().IsCOMObject == true)
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
        }
    }
}
