namespace Gekka.Language.Translator.Interfaces
{
    public interface ITranslatorFactory
    {
        /// <summary>この翻訳機能の名前</summary>
        string TranslatorName { get; }

        ///// <summary>翻訳後のXMLファイルに追記する文字列</summary>
        //string Comment { get; }

        /// <summary>複数行の翻訳に対応しているか</summary>
        bool IsMultipleTranslator { get; }

        ITranslator Create();
    }

    public interface IBrowserTranslatorFactory : ITranslatorFactory
    {
        IWebDriverFactory DriverFactory { get; set; }
        Gekka.Language.Translator.Browser.CustomDriverOption Option { get; }
    }

}
