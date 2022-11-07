using System.Collections.Generic;
using Base;
using CharacterControllers;
using UnityEngine;

namespace Managers
{
    public class PlayerManager
    {
        public PlayerController myPlayer;
        private readonly List<BaseCharacterController> _characterControllers = new();
        private static PlayerManager _instance;

        public static PlayerManager Instance
        {
            get { return _instance ??= new PlayerManager(); }
        }
        
        public void Add(BaseCharacterController controller)
        {
            _characterControllers.Add(controller);
        }

        public void Remove(BaseCharacterController controller)
        {
            _characterControllers.Remove(controller);
        }

        public void Update()
        {
            foreach (var character in _characterControllers)
            {
                var worldPos = character.mCharacterController.Body.position;
                var x = (float) worldPos.X;
                var y = (float) worldPos.Y;
                var z = (float) worldPos.Z;
                character.transform.position = new Vector3(x, y, z);
            }
        }
    }
}