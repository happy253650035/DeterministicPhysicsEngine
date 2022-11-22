using System.Collections.Generic;
using Base;

namespace Managers
{
    public class ObjectManager
    {
        private static ObjectManager _instance;
        private readonly Dictionary<int, BaseObject> _dictionary = new();
        private readonly List<BaseObject> _baseObjects = new();

        public static ObjectManager Instance
        {
            get { return _instance ??= new ObjectManager(); }
        }
        
        public BaseObject GetBaseObjectById(int id)
        {
            return _dictionary[id];
        }

        public void Add(BaseObject bo)
        {
            _baseObjects.Add(bo);
            if (!_dictionary.ContainsKey(bo.id))
            {
                _dictionary.Add(bo.id, bo);
            }
        }

        public void Remove(BaseObject bo)
        {
            _baseObjects.Remove(bo);
            if (_dictionary.ContainsKey(bo.id)) _dictionary.Remove(bo.id);
        }
    }
}