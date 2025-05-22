using System;
using System.Collections.Generic;
using System.Linq;

namespace DrawingRecognition.Models
{
    [Serializable]
    public class SerializableDictionaryStringCharacter
    {
        public List<SerializableDictionaryStringCharacterElement> elements = new();

        [Serializable]
        public class SerializableDictionaryStringCharacterElement
        {
            public string key;
            public Character value;
        }

        public bool ContainsKey(string key) => elements.Exists(element => element.key == key);
        public int IndexOf(string key) => elements.FindIndex(element => element.key == key);

        public void Add(string key, Character value) => elements.Add(new SerializableDictionaryStringCharacterElement
            { key = key, value = value });

        public void Remove(string key) => elements.RemoveAll(element => element.key == key);

        public SerializableDictionaryStringCharacterElement GetByKey(string key) =>
            elements.FirstOrDefault(element => element.key == key);
    }
}