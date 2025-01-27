namespace Gekka.Language.Translator.Translators
{
    public class TranslateLang
    {
        public TranslateLang(string from, string to)
        {
            From = from;
            To = to;
        }

        public readonly string From;
        public readonly string To;
    }

}
