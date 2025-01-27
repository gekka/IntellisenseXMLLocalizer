#define ENABLE_SEND

namespace Gekka.Language.Translator.Win
{
    class WinCredensial
    {
        public unsafe static bool GetPass(string name, out string? pass)
        {
            Windows.Win32.Security.Credentials.CREDENTIALW* pcred;

            if (!Windows.Win32.PInvoke.CredRead(name, Windows.Win32.Security.Credentials.CRED_TYPE.CRED_TYPE_GENERIC, out pcred))
            {
                pass = null;
                return false;
            }

            try
            {
                uint size = pcred->CredentialBlobSize;

                byte[] bs = new byte[size];

                System.Runtime.InteropServices.Marshal.Copy((System.IntPtr)pcred->CredentialBlob, bs, 0, (int)size);
                pass = System.Text.Encoding.Unicode.GetString(bs);
                return true;
            }
            finally
            {
                Windows.Win32.PInvoke.CredFree(pcred);
            }


        }
    }
}
