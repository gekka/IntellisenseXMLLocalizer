using System.Threading.Tasks;
namespace Gekka.Language.Translator.Interfaces
{
    public interface ITranslator : System.IDisposable
    {
        /// <summary>この翻訳機能の名前</summary>
        string TranslatorName { get; }

        /// <summary>翻訳後のXMLファイルに追記する文字列</summary>
        string Comment { get; }

        /// <summary>複数行の翻訳に対応しているか</summary>
        bool IsMultipleTranslator { get; }

        /// <summary>翻訳を行う</summary>
        /// <param name="text">翻訳元の文字列</param>
        /// <param name="token"></param>
        /// <returns>翻訳された文字列</returns>
        Task<string?> GetLocalizeTextAsync(string text, System.Threading.CancellationToken token);
    }

}
