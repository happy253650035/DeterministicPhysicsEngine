using System;
using System.Collections.Generic;
using BEPUphysics.BroadPhaseEntries;
using BEPUphysics.BroadPhaseEntries.MobileCollidables;
using BEPUphysics.NarrowPhaseSystems.Pairs;
using Listeners;
using UnityEngine;
using Utils;
using CharacterController = BEPUphysics.Character.CharacterController;

namespace Base
{
    [RequireComponent(typeof(CapsuleCollider))]
    public abstract class BaseCharacterController : MonoBehaviour
    {
        public float mass = 2;
        public float speed = 0.5f;
        public float gravity = 9.81f;
        public CharacterController mCharacterController;
        public event ColliderListener.CollisionEnterDetected OnEnterCharacter;
        public event ColliderListener.CollisionEnterDetectedMainThread OnEnterCharacterMainThread;
        public event ColliderListener.CollisionEnterDetected OnExitCharacter;
        public event ColliderListener.CollisionExitDetectedMainThread OnExitCharacterMainThread;
        private int _buffIndex;
        private readonly List<ColliderListener.CollisionInfo> _collisionEnterInfos = new();
        private readonly List<ColliderListener.CollisionInfo> _collisionExitInfos = new();
        private readonly Dictionary<SkillName, BaseSkill> _skillDic = new();
        private readonly Dictionary<int, BaseBuff> _buffDic = new();
        private readonly List<BaseBuff> _deActiveBuffList = new();
        protected abstract void OnAwake();
        protected abstract void OnStart();
        protected abstract void Enable();
        protected abstract void OnUpdate();
        protected abstract void OnTick();

        public bool IsActive { get; private set; }

        private void OnEnable()
        {
            Enable();
        }

        private void Awake()
        {
            _buffIndex = 0;
            var capsule = GetComponent<CapsuleCollider>();
            mCharacterController = new CharacterController();
            var pos = transform.position;
            var center = capsule.center;
            mCharacterController.Body.position = new BEPUutilities.Vector3(Convert.ToDecimal(pos.x + center.x),
                Convert.ToDecimal(pos.y + center.y), Convert.ToDecimal(pos.z + center.z));
            mCharacterController.Body.Height = Convert.ToDecimal(capsule.height);
            mCharacterController.Body.Radius = Convert.ToDecimal(capsule.radius);
            mCharacterController.Body.Mass = Convert.ToDecimal(mass);
            mCharacterController.SpeedScale = Convert.ToDecimal(speed);
            mCharacterController.Body.Gravity = new BEPUutilities.Vector3(0, -Convert.ToDecimal(gravity), 0);
            mCharacterController.Body.CollisionInformation.GameObject = gameObject;
            OnAwake();
        }
    
        private void Start()
        {
            var material = GetComponent<PhysicsMaterial>();
            if (material)
            {
                mCharacterController.Body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(material.staticFriction),
                    Convert.ToDecimal(material.kineticFriction), Convert.ToDecimal(material.bounciness));
            }
            else
            {
                mCharacterController.Body.material = new BEPUphysics.Materials.Material(Convert.ToDecimal(PhysicsWorld.Instance.staticFriction),
                    Convert.ToDecimal(PhysicsWorld.Instance.kineticFriction), Convert.ToDecimal(PhysicsWorld.Instance.bounciness));
            }
            Activate();
            mCharacterController.Body.CollisionInformation.Events.InitialCollisionDetected +=
                InitialCollisionDetected;
            mCharacterController.Body.CollisionInformation.Events.CollisionEnded +=
                CollisionEnded;
            OnStart();
        }
    
        private void InitialCollisionDetected(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
        {
            OnEnterCharacter?.Invoke(sender, other, pair);
            var info = new ColliderListener.CollisionInfo {Sender = sender, Other = other, Pair = pair};
            _collisionEnterInfos.Add(info);
        }

        private void CollisionEnded(EntityCollidable sender, Collidable other, CollidablePairHandler pair)
        {
            OnExitCharacter?.Invoke(sender, other, pair);
            var info = new ColliderListener.CollisionInfo {Sender = sender, Other = other, Pair = pair};
            _collisionExitInfos.Add(info);
        }

        public void AddSkill(BaseSkill skill)
        {
            skill.characterController = this;
            if (!_skillDic.ContainsKey(skill.name))
            {
                _skillDic.Add(skill.name, skill);
            }
        }

        public void RemoveSkill(BaseSkill skill)
        {
            _skillDic.Remove(skill.name);
        }

        public int AddBuff(BaseBuff buff)
        {
            buff.id = _buffIndex++;
            buff.state = BaseBuff.BuffState.Active;
            buff.characterController = this;
            buff.Start();
            _buffDic.Add(buff.id, buff);
            return buff.id;
        }

        public void RemoveBuff(BaseBuff buff)
        {
            buff.End();
            _buffDic.Remove(buff.id);
        }
        
        public void RemoveBuff(int id)
        {
            _buffDic[id].End();
            _buffDic.Remove(id);
        }

        public BaseBuff GetBuff(int id)
        {
            return _buffDic.ContainsKey(id) ? _buffDic[id] : null;
        }

        private void Activate()
        {
            if (IsActive) return;
            IsActive = true;
            PhysicsWorld.Instance.AddPhysicsCharacterController(this);
        }

        private void Deactivate()
        {
            if (!IsActive) return;
            IsActive = false;
            PhysicsWorld.Instance.RemovePhysicsCharacterController(this);
        }

        public void ExecuteSkill(SkillName skillName)
        {
            _skillDic[skillName].Execute();
        }

        public void SetSpeed(float s)
        {
            mCharacterController.SpeedScale = Convert.ToDecimal(speed);
            speed = s;
        }

        private void Update()
        {
            foreach (var info in _collisionEnterInfos)
            {
                OnEnterCharacterMainThread?.Invoke(info.Sender, info.Other, info.Pair);
            }
            foreach (var info in _collisionExitInfos)
            {
                OnExitCharacterMainThread?.Invoke(info.Sender, info.Other, info.Pair);
            }
            _collisionEnterInfos.Clear();
            _collisionExitInfos.Clear();
            OnUpdate();
        }

        public void Tick()
        {
            _deActiveBuffList.Clear();
            foreach (var (_, buff) in _buffDic)
            {
                buff.Tick();
                if (buff.state == BaseBuff.BuffState.DeActive) _deActiveBuffList.Add(buff);
            }
            foreach (var buff in _deActiveBuffList)
            {
                RemoveBuff(buff);
            }
            OnTick();
        }
    }
}