using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utility
{

    /// <summary>
    /// SerializedDictionary element.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    [Serializable]
    public class DictionaryElement<T, U>
    {
        public T Key;
        public U Value;

        public DictionaryElement()
        {

        }

        public DictionaryElement(T key, U value)
        {
            Key = key;
            Value = value;
        }
    }

    /// <summary>
    /// A Serializable Dictionary implementing IDictionary interface.
    /// Complexity of operations are same with a system dictionary.
    /// Except for remove. Since serialization uses an internal List,
    /// removing from this dictionary has the complexity of O(n).
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    [Serializable]
    public class SerializedDictionary<T, U> : IDictionary<T, U>, ISerializationCallbackReceiver
    {

        [SerializeField]
        private List<DictionaryElement<T, U>> _elements;

        private Dictionary<T, DictionaryElement<T, U>> _dict;


        public void OnBeforeSerialize()
        {
            //
        }

        public void OnAfterDeserialize()
        {
            _dict = new Dictionary<T, DictionaryElement<T, U>>();
            foreach (var ele in _elements)
            {
                try
                {
                    _dict.Add(ele.Key, ele);
                }
                catch (ArgumentException)
                {
                    Debug.LogError(string.Format("An entry with the {0} key exist!", ele.Key));
                }
            }
        }

        public SerializedDictionary()
        {

        }

        public U this[T key]
        {
            get => _dict[key].Value;
            set => _dict[key].Value = value;
        }

        public ICollection<T> Keys => _dict.Keys;

        public ICollection<U> Values => _dict.Values.ToList().Select(v => v.Value).ToList();

        public int Count => _dict.Count;

        public void Add(T key, U value)
        {
            try
            {
                var e = new DictionaryElement<T, U>(key, value);
                _dict.Add(key, e);
                _elements.Add(e);
            }
            catch (ArgumentException e)
            {
                throw e;
            }
        }

        public void Clear()
        {
            _elements.Clear();
            _dict.Clear();
        }

        public bool ContainsKey(T key)
        {
            return _dict.ContainsKey(key);
        }

        public bool Remove(T key)
        {
            try
            {
                var e = _dict[key];
                _elements.Remove(e);
                return _dict.Remove(key);
            }
            catch
            {
                return false;
            }
        }


        bool ICollection<KeyValuePair<T, U>>.IsReadOnly => throw new NotImplementedException();

        void ICollection<KeyValuePair<T, U>>.Add(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<T, U>>.Contains(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        void ICollection<KeyValuePair<T, U>>.CopyTo(KeyValuePair<T, U>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        IEnumerator<KeyValuePair<T, U>> IEnumerable<KeyValuePair<T, U>>.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        bool ICollection<KeyValuePair<T, U>>.Remove(KeyValuePair<T, U> item)
        {
            throw new NotImplementedException();
        }

        bool IDictionary<T, U>.TryGetValue(T key, out U value)
        {
            throw new NotImplementedException();
        }

    }
}
