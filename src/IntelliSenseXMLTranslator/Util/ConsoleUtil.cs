namespace Gekka.Language.IntelliSenseXMLTranslator.Util
{
    using System;
    using System.Threading.Tasks;

    static class ConsoleUtil
    {
        /// <summary>Ctrl+Cをすると子プロセスも終了してしまうので、Console.CancelKeyPressイベントでは対応できない</summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task WaitCTRL_C(System.Threading.CancellationToken token, int checkIntervalMs = 100)
        {
            Console.TreatControlCAsInput = true;

            while (!token.IsCancellationRequested)
            {
                if (!Console.KeyAvailable)
                {
                    await Task.Delay(checkIntervalMs, token);
                    continue;
                }

                if (token.IsCancellationRequested)
                {
                    return;
                }

                while (Console.KeyAvailable)
                {
                    var k = Console.ReadKey(false);
                    if (k.Modifiers == ConsoleModifiers.Control && k.Key == ConsoleKey.C)
                    {
                        return;
                    }
                }
            }
        }
    }

}
