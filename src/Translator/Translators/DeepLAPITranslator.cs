/*
#define ENABLE_SEND

using System;
using System.Threading;
using System.Threading.Tasks;
using Gekka.Language.Translator.Interfaces;


namespace Gekka.Language.Translator.Translators
{
    class DeepLAPITranslator : ITranslator
    {
        public DeepLAPITranslator()
        {
            string keyFree = new Uri(DL.Translators.Deepl.Hosts.ApiFree).Host;
            authFree = GetPasswordFromCredentialsManager(keyFree, false);

            string keyPro = new Uri(DL.Translators.Deepl.Hosts.ApiPro).Host;
            authPro = GetPasswordFromCredentialsManager(keyPro, false);

            HasAuth = authFree != null || authPro != null;

            isPro = authPro != null;

            Comment=isPro ? DeepLAPITranslatorFactory.CommentPro : DeepLAPITranslatorFactory.CommentFree;
        }

        internal readonly string? authPro;
        internal readonly string? authFree;

        internal readonly bool isPro;
        internal string? authKey => authPro ?? authFree;

        public string TranslatorName => DeepLAPITranslatorFactory.Default.TranslatorName;
        public string Comment { get; }
        public bool IsMultipleTranslator => DeepLAPITranslatorFactory.Default.IsMultipleTranslator;

        public bool HasAuth { get; }

        public int LimitLength { get; } = 2000;

        private static string? GetPasswordFromCredentialsManager(string key, bool throwIfNotFound)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return null;
            }

            if (!Gekka.Language.Translator.Win.WinCredensial.GetPass(key, out var pass))
            {
                if (throwIfNotFound)
                {
                    throw new ApplicationException("Windows資格情報でログイン情報が見つかりませんでした");
                }
                else
                {
                    return null;
                }
            }

            return pass;
        }



        public async Task<string?> GetLocalizeTextAsync(string text, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return text;
            }
            text = text.Trim();
            if (text.Length > LimitLength)
            {
                throw new ArgumentOutOfRangeException("文字が長すぎます");
            }


            var tt = new DL.Translators.Deepl.TranslatingText(DL.Translators.Deepl.TargetLang.JA, DL.Translators.Deepl.SourceLang.EN, text);
            tt.HostSelectMode = DL.Translators.Deepl.Hosts.HostSelectMode.Auto;

            tt.auth_key = authPro ?? authFree;

#if ENABLE_SEND
            var result = await DL.RestClient.SendAndResponse(tt, token);
            if (result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }

            return result.Result.translations[0].text;
#else
            return await Task.FromResult<string?>(null);
#endif
        }


        public async Task<bool> GetUsageAsync(System.Threading.CancellationToken token)
        {
            var us = new DL.Translators.Deepl.Usage();
            us.auth_key = authKey;

            var usage = await DL.RestClient.SendAndResponse(us, us, token);
            if (usage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                this.CharacterLimit = usage.Result.character_limit;
                this.UsageCharacterCount = usage.Result.character_count;
                return true;
                //if (usage.Result.character_limit - usage.Result.character_count < 100000)
                //{
                //    translator = new DeeplWebTranslator(cts.Token);
                //}
            }
            return false;
        }

        public long CharacterLimit { get; private set; }
        public long UsageCharacterCount { get; private set; }


        void IDisposable.Dispose() { }
    }

    internal class DeepLAPITranslatorFactory : TranslatorFactoryBase<DeepLAPITranslator, DeepLAPITranslatorFactory>
    {
        internal static string CommentPro = "DeepLのAPI Proで翻訳を行いました。翻訳内容をOSSに使用する場合は、元の契約内容を確認してください。\r\nThis translated text in this file was translated using Deepl API Pro. If you use the translated content for OSS, please refer to the original agreement.";
        internal static string CommentFree = "DeepLのAPI Freeで翻訳を行いました。翻訳内容をOSSに使用することは禁止です。\r\nThis translated text in this file was translated using Deepl API Free. This content must not be used for OSS.";

        public DeepLAPITranslatorFactory() : base("DeepL API", false)
        {
        }

        public override ITranslator Create() => new DeepLAPITranslator();
    }
}

*/
