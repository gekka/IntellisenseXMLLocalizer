using System.Threading.Tasks;
using System.Collections.Generic;
using Gekka.Language.Translator.Translators;

namespace Gekka.Language.Translator.Interfaces
{
    /// <summary></summary>
    public interface IMultipleTranslator : ITranslator
    {
        Task GetLocalizeTextAsync(IDictionary<string, string?> texts, System.Threading.CancellationToken token);
        event System.EventHandler<MultipleTranslatorEventArgs>? Progress;
    }
}
