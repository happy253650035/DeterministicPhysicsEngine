using System.Collections.Generic;
using Base;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using MapComHandlers;

namespace Managers
{
    public class MapComManager
    {
        private static MapComManager _instance;
        private readonly List<BaseMapComHandler> _handlers = new();

        public static MapComManager Instance
        {
            get { return _instance ??= new MapComManager(); }
        }

        public void Init()
        {
            RegisterHandler(new DeathComHandler());
            RegisterHandler(new BounceComHandler());
            RegisterHandler(new SaveComHandler());
            RegisterHandler(new TumbleComHandler());
            RegisterHandler(new AccelerateComHandler());
            RegisterHandler(new IceComHandler());
        }

        public void HandleEnterMapCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            foreach (var handler in _handlers)
            {
                handler.HandleEnterCom(sender, other, pair, characterController);
            }
        }
    
        public void HandleExitMapCom(EntityCollidable sender, Collidable other, CollidablePairHandler pair, BaseCharacterController characterController)
        {
            foreach (var handler in _handlers)
            {
                handler.HandleExitCom(sender, other, pair, characterController);
            }
        }

        private void RegisterHandler(BaseMapComHandler handler)
        {
            _handlers.Add(handler);
        }

        private void UnRegisterHandler(BaseMapComHandler handler)
        {
            _handlers.Remove(handler);
        }

        public void Clear()
        {
            _handlers.Clear();
        }
    }
}