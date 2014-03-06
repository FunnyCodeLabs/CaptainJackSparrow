using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Common
{
    public class ConcurrentBiDictionary<TKey, TValue> 
        where TKey : class 
        where TValue : class
    {
        IDictionary<TKey, TValue> __FirstToSecond = new ConcurrentDictionary<TKey, TValue>();
        IDictionary<TValue, TKey> __SecondToFirst = new ConcurrentDictionary<TValue, TKey>();

        public ConcurrentBiDictionary()
        { }

        public ConcurrentBiDictionary(IDictionary<TKey, TValue> biDict)
        {
            foreach (var item in biDict)
            {
                Add(item.Key, item.Value);
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (__FirstToSecond.ContainsKey(key) || __SecondToFirst.ContainsKey(value))
            {
                throw new ArgumentException("Duplicate first or second");
            }

            __FirstToSecond.Add(key, value);
            __SecondToFirst.Add(value, key);
        }

        public void Remove(TKey key, TValue value)
        {
            __FirstToSecond.Remove(key);
            __SecondToFirst.Remove(value);
        }

        public void Remove(TKey key)
        {
            TValue value = __FirstToSecond[key];
            Remove(key, value);
        }

        public void Remove(TValue value)
        {
            TKey key = __SecondToFirst[value];
            Remove(key, value);
        }

        public TValue GetByKey(TKey key)
        {
            TValue value;
            if (!__FirstToSecond.TryGetValue(key, out value))
                return null;

            return value;
        }

        public TKey GetByValue(TValue value)
        {
            TKey key;
            if (!__SecondToFirst.TryGetValue(value, out key))
                return null;

            return key;
        }

        public bool ContainsKey(TKey key)
        {
            return __FirstToSecond.ContainsKey(key);
        }

        public bool ContainsValue(TValue value)
        {
            return __SecondToFirst.ContainsKey(value);
        }

        public ICollection<TKey> Keys
        {
            get
            {
                return __SecondToFirst.Values;
            }
        }

        public ICollection<TValue> Values
        {
            get
            {
                return __FirstToSecond.Values;
            }
        }
    }
}
