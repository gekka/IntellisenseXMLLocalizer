namespace Gekka.Language.IntelliSenseXMLTranslator.DB
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;

    public class StringDictionary : IDictionary<string, string>, IDisposable , IStringDictionary
    {
        public StringDictionary(string dbPath, bool autoSave = true)
            : this(dbPath, new System.Text.UTF8Encoding(true), autoSave)
        {
        }

        public StringDictionary(string dbPath, System.Text.Encoding enc, bool autoSave = true)
        {
            this.dbPath = new System.IO.FileInfo(dbPath).FullName;
            this.enc = enc;
            this.autoSave = autoSave;

            Migrate();
        }

        private string dbPath;
        private bool autoSave;
        private System.Text.Encoding enc;

        const string X_CR = "\x240D";//␍
        const string X_LF = "\x240A";//␊

        public void Migrate()
        {
            Clear();
            if (System.IO.File.Exists(dbPath))
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(dbPath, enc))
                {
                    while (sr.Peek() != -1)
                    {
                        var k = sr.ReadLine()?.Replace(X_CR, "\r").Replace(X_LF, "\n");
                        var v = sr.ReadLine()?.Replace(X_CR, "\r").Replace(X_LF, "\n");
                        if (k != null && v != null)
                        {
                            Add(k, v);
                        }
                    }
                }
            }
            IsChanged = false;
        }

        public void SaveChanges()
        {
            if (!IsChanged)
            {
                return;
            }
            var temp = System.IO.Path.GetTempFileName();
            try
            {
                var dir = System.IO.Path.GetDirectoryName(dbPath);
                System.IO.Directory.CreateDirectory(dir!);

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(temp, false, enc))
                {
                    foreach (var kv in this)
                    {
                        var k = kv.Key.Replace("\r", X_CR).Replace("\n", X_LF);
                        var v = kv.Value.Replace("\r", X_CR).Replace("\n", X_LF);

                        sw.WriteLine(k);
                        sw.WriteLine(v);
                    }
                    sw.Flush();
                }

                System.IO.File.Copy(temp, dbPath, true);
                IsChanged = false;
            }
            finally
            {
                System.IO.File.Delete(temp);
            }
        }

        public bool IsChanged { get; private set; }

        public void Dispose()
        {
            try
            {
                if (autoSave)
                {
                    SaveChanges();
                }
            }
            catch
            {
            }
        }

        #region
        private System.Collections.Generic.IDictionary<string, string> dic = new SortedDictionary<string, string>();

        public void Add(string key, string value)
        {
            ((IDictionary<string, string>)dic).Add(key, value);
            IsChanged = true;
        }

        public bool ContainsKey(string key) => ((IDictionary<string, string>)dic).ContainsKey(key);
        public bool Remove(string key)
        {
            bool ret = ((IDictionary<string, string>)dic).Remove(key);
            IsChanged = true;
            return ret;
        }
        public bool TryGetValue(string key, [System.Diagnostics.CodeAnalysis. MaybeNullWhen(false)] out string value) => ((IDictionary<string, string>)dic).TryGetValue(key, out value);
        public string this[string key]
        {
            get => ((IDictionary<string, string>)dic)[key];
            set
            {
                ((IDictionary<string, string>)dic)[key] = value;
                IsChanged = true;
            }
        }
        public ICollection<string> Keys => ((IDictionary<string, string>)dic).Keys;
        public ICollection<string> Values => ((IDictionary<string, string>)dic).Values;
        public void Add(KeyValuePair<string, string> item)
        {
            ((ICollection<KeyValuePair<string, string>>)dic).Add(item);
            IsChanged = true;
        }
        public void Clear()
        {
            ((ICollection<KeyValuePair<string, string>>)dic).Clear();
            IsChanged = true;
        }
        public bool Contains(KeyValuePair<string, string> item) => ((ICollection<KeyValuePair<string, string>>)dic).Contains(item);
        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) => ((ICollection<KeyValuePair<string, string>>)dic).CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<string, string> item)
        {
            bool ret = ((ICollection<KeyValuePair<string, string>>)dic).Remove(item);
            IsChanged = true;
            return ret;
        }
        public int Count => ((ICollection<KeyValuePair<string, string>>)dic).Count;
        public bool IsReadOnly => ((ICollection<KeyValuePair<string, string>>)dic).IsReadOnly;
        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => ((IEnumerable<KeyValuePair<string, string>>)dic).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)dic).GetEnumerator();

        #endregion
    }
}
