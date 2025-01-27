using Gekka.Language.Translator.Interfaces;

namespace Gekka.Language.Translator.Translators
{
    internal abstract class TranslatorFactoryBase<T, SELF> : ITranslatorFactory
        where T : ITranslator
        where SELF : TranslatorFactoryBase<T, SELF>, new()
    {
        public static ITranslatorFactory Default { get; } = new SELF();

        public TranslatorFactoryBase(string name,bool isMultipleTranslator)
        {
            TranslatorName = name;
            //Comment = comment;
            IsMultipleTranslator = isMultipleTranslator;

        }

        public string TranslatorName { get; protected set; }
        //public string Comment { get;  protected set;}
        public bool IsMultipleTranslator { get;  protected set;}

        public abstract ITranslator Create();

    }
}
