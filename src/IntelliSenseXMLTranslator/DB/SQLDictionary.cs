using System;

namespace Gekka.Language.IntelliSenseXMLTranslator.DB
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Linq;

    public class SQLDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDisposable
        where TKey : IConvertible, IEquatable<TKey>
        where TValue : IEquatable<TValue>
    {
        //public SQLDictionary(string dbPath, string tableName)
        //{
        //    Type tk = typeof(TKey);
        //    if (tk == typeof(string))
        //    {
        //        toKeyString = (k) => (string)(object)k;
        //    }
        //    else
        //    {
        //        toKeyString = (k) => k.ToString();
        //    }

        //    Type tv = typeof(TValue);
        //    if (tv == typeof(string))
        //    {
        //        toValue = (s) => (TValue)(object)s;
        //        toString = (v) => (string)(object)v;
        //    }
        //    else
        //    {
        //        //toValue = (s) => s.ToString();
        //        toString = (v) => v.ToString();
        //    }


        //}

        protected SQLDictionary(string dbPath, string tableName, Func<string, TKey> toKey, Func<TKey, string> toKeyString, Func<TValue, string> toString, Func<string, TValue> toValue)
        {
            this.toKey = toKey;
            this.toKeyString = toKeyString;
            toValueString = toString;
            this.toValue = toValue;

            var connectionString = new SQLiteConnectionStringBuilder() { DataSource = dbPath }.ConnectionString;

            //if (!System.IO.File.Exists(dbPath))
            //{
            //    System.IO.File.Create(dbPath).Dispose();
            //}
            con = new SQLiteConnection(connectionString);
            con.Open();

            if (tableName.Contains('\"'))
            {
                throw new ArgumentException(nameof(tableName));
            }

            this.tableName = "\"" + tableName.Trim() + "\"";


            cmdAdd = new SQLiteCommand();
            {
                cmdAdd.Connection = con;
                cmdAdd.CommandText = $"INSERT OR REPLACE INTO {tableName}(key,Value) VALUES(@key,@value)";
                cmdAdd.Parameters.Add(new SQLiteParameter("key", System.Data.DbType.String));
                cmdAdd.Parameters.Add(new SQLiteParameter("value", System.Data.DbType.String));
            }

            Migrate();
        }

        //private string KEYTYPE;
        private readonly string tableName;
        protected readonly SQLiteConnection con;
        private readonly SQLiteCommand cmdAdd;


        private readonly Func<TKey, string> toKeyString;
        private readonly Func<TValue, string> toValueString;

        private readonly Func<string, TKey> toKey;
        private readonly Func<string, TValue> toValue;


        public void Migrate()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"CREATE TABLE IF NOT EXISTS  {tableName}(Key TEXT PRIMARY KEY, Value TEXT)";
            cmd.ExecuteNonQuery();
        }

        public void DeleteTable()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DROP TABLE IF EXISTS {tableName}";
            cmd.ExecuteNonQuery();
        }

        public void ImportFrom(IEnumerable<KeyValuePair<TKey, TValue>> kvs)
        {
            foreach (var kv in kvs)
            {
                Add(kv.Key, kv.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            cmdAdd.Parameters["key"].Value = toKeyString(key);
            cmdAdd.Parameters["value"].Value = toValueString(value);
            cmdAdd.ExecuteScalar();

            _Count = null;
        }

        public bool ContainsKey(TKey key)
        {
            return TryGetValue(key, out var v);
        }

        public bool Remove(TKey key)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"DELETE FROM {tableName} WHERE Key=@key";
            cmd.Parameters.Add(new SQLiteParameter("key", toKeyString(key)));
            var r = cmd.ExecuteReader();
            return r.RecordsAffected != 0;
        }        

        public bool TryGetValue(TKey key, out TValue value)
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT Value From {tableName} WHERE Key=@key";
            cmd.Parameters.Add(new SQLiteParameter("key", toKeyString(key)));
            var r = cmd.ExecuteReader();
            if (r.HasRows)
            {
                if (r.Read())
                {
                    var s = r.GetString(0);
                    value = toValue(s);
                    return true;
                }
            }

            value = default!;
            return false;
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!TryGetValue(key, out var v))
                {
                    throw new KeyNotFoundException();
                }
                return v;
            }
            set
            {
                Add(key, value);
            }
        }

        public ICollection<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = $"SELECT Key From {tableName}";
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {

                    while (r.Read())
                    {
                        var s = r.GetString(0);
                        keys.Add(toKey(s));
                    }
                }
                return keys;
            }
        }
        public ICollection<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                SQLiteCommand cmd = new SQLiteCommand();
                cmd.Connection = con;
                cmd.CommandText = $"SELECT Value From {tableName}";
                var r = cmd.ExecuteReader();
                if (r.HasRows)
                {

                    while (r.Read())
                    {
                        var s = r.GetString(0);
                        values.Add(toValue(s));
                    }

                }
                return values;
            }
        }

        public int Count
        {
            get
            {
                if (!_Count.HasValue)
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.Connection = con;
                    cmd.CommandText = $"SELECT COUNT(*) From {tableName}";
                    _Count = checked((int)(long)cmd.ExecuteScalar());
                }
                return _Count.Value;
            }
        }
        private int? _Count;

        public bool IsReadOnly => false;

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = con;
            cmd.CommandText = $"SELECT Key,Value From {tableName}";
            var r = cmd.ExecuteReader();
            if (r.HasRows)
            {
                if (r.Read())
                {

                    var k = toKey(r.GetString(0));
                    var v = toValue(r.GetString(1));
                    yield return new KeyValuePair<TKey, TValue>(k, v);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region ICollection

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            if (!TryGetValue(item.Key, out var v))
            {
                return false;
            }

            if (item.Value == null && v == null)
            {
                return true;
            }

            return item.Value?.Equals(v) == true;


        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        int ICollection<KeyValuePair<TKey, TValue>>.Count => Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => IsReadOnly;

        #endregion

        public void Dispose()
        {
            ((IDisposable)con)?.Dispose();
        }
    }
}
