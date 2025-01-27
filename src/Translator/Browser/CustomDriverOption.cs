namespace Gekka.Language.Translator.Browser
{
    using System.Collections.Generic;

    public class CustomDriverOption
    {
        public CustomDriverOption(params  Gekka.Language.Translator.Translators.TranslateLang[] langs)
        {
            this.TranslateLanguages.AddRange(langs);
        }

        public bool AllowLocalFile { get; set; } = true;

        public List< Gekka.Language.Translator.Translators.TranslateLang> TranslateLanguages { get; } 
            = new List< Gekka.Language.Translator.Translators.TranslateLang>();
    }

}
