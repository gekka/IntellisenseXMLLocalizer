using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Gekka.Language.Translator.Interfaces;
namespace Gekka.Language.Translator.Translators
{
    class DummyTranslator: IMultipleTranslator
    {
        public Task GetLocalizeTextAsync(IDictionary<string, string?> texts, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        public event EventHandler<MultipleTranslatorEventArgs>? Progress;

        public string TranslatorName { get; } = "Dummy";
        public string Comment { get; } = "";
        public bool IsMultipleTranslator { get; } = false;

        public Task<string?> GetLocalizeTextAsync(string text, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }

    class DummyTranslatorFactory : ITranslatorFactory
    {
        public string TranslatorName { get; } = "Dummy";
        public bool IsMultipleTranslator { get; } = false;

        public ITranslator Create()
        {
            return new DummyTranslator();
        }
    }
}
