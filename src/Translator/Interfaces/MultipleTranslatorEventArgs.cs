namespace Gekka.Language.Translator.Interfaces
{
    /// <summary></summary>
    public class MultipleTranslatorEventArgs : System.EventArgs
    {
        public MultipleTranslatorEventArgs(int translatedCount, int sourceCount)
        {
            TranslatedCount = translatedCount;
            SourceCount = sourceCount;

            ProgressPercent = sourceCount == 0 ? 0 : ((double)translatedCount / (double)sourceCount);
        }

        public MultipleTranslatorEventArgs(int translatedCount, int sourceCount, double progressPercent)
        {
            SourceCount = sourceCount;
            TranslatedCount = translatedCount;
            ProgressPercent = progressPercent;
        }

        public int SourceCount { get; }
        public int TranslatedCount { get; }

        public double ProgressPercent { get; }
    }
}
