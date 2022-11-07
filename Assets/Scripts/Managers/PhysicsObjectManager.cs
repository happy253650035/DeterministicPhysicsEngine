using System.Collections.Generic;
using Base;
using UnityEngine;

namespace Managers
{
    public class PhysicsObjectManager
    {
        private static PhysicsObjectManager _instance;
        private readonly Dictionary<int, PhysicsObject> _dictionary = new();
        private readonly List<PhysicsObject> _physicsObjects = new();

        public static PhysicsObjectManager Instance
        {
            get { return _instance ??= new PhysicsObjectManager(); }
        }

        public PhysicsObject GetPhysicsObjectById(int id)
        {
            return _dictionary[id];
        }

        public void Add(PhysicsObject po)
        {
            _physicsObjects.Add(po);
            if (!_dictionary.ContainsKey(po.id))
            {
                _dictionary.Add(po.id, po);
            }
        }

        public void Remove(PhysicsObject po)
        {
            _physicsObjects.Remove(po);
            if (_dictionary.ContainsKey(po.id)) _dictionary.Remove(po.id);
        }

        public void Update()
        {
            foreach (var po in _physicsObjects)
            {
                var worldPos = po.mEntity.position;
                var x = (float) worldPos.X;
                var y = (float) worldPos.Y;
                var z = (float) worldPos.Z;
                po.transform.position = new Vector3(x, y, z) - po.center;
                var orientation = po.mEntity.orientation;
                po.transform.rotation = new Quaternion((float) orientation.X,
                    (float) orientation.Y, (float) orientation.Z,
                    (float) orientation.W);
            }
        }
    }
}