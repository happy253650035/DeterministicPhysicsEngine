using System;
using System.Collections.Generic;
using System.Threading;
using Managers;
using UnityEngine;
using Utils;
using Space = BEPUphysics.Space;

public class PhysicsWorld : MonoBehaviour
{
    public bool Asynchronous;
    public float gravity = 9.81f;
    public float kineticFriction;
    public float staticFriction;
    public float bounciness;
    public static PhysicsWorld Instance;

    private bool _useThread;
    private Thread _physicThread;
    private Space _physicsSpace;

    private void Awake()
    {
        Application.targetFrameRate = 30;
        if (Instance != null) return;
        Instance = this;
        Physics.autoSimulation = false;
        _physicsSpace = new Space();
        _physicsSpace.ForceUpdater.gravity = new BEPUutilities.Vector3(0, -Convert.ToDecimal(gravity), 0);
        _physicsSpace.TimeStepSettings.TimeStepDuration = 0.02M;
        MapComManager.Instance.Init();
        if (!Asynchronous) return;
        _physicThread = new Thread(Run);
        _physicThread.Start();
    }

    private void Run()
    {
        while (true)
        {
            CommandManager.Instance.Execute();
            _physicsSpace.Update();
            Thread.Sleep(20);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void AddPhysicsObject(PhysicsObject po)
    {
        _physicsSpace.Add(po.mEntity);
        PhysicsObjectManager.Instance.Add(po);
    }

    public void RemovePhysicsObject(PhysicsObject po)
    {
        _physicsSpace.Remove(po.mEntity);
        PhysicsObjectManager.Instance.Remove(po);
    }

    public void AddPhysicsCharacterController(BaseCharacterController controller)
    {
        _physicsSpace.Add(controller.mCharacterController);
        PlayerManager.Instance.Add(controller);
    }

    public void RemovePhysicsCharacterController(BaseCharacterController controller)
    {
        _physicsSpace.Remove(controller.mCharacterController);
        PlayerManager.Instance.Remove(controller);
    }

    private void Update()
    {
        if (!Asynchronous)
        {
            CommandManager.Instance.Execute();
            _physicsSpace.Update();
        }
        
        PhysicsObjectManager.Instance.Update();
        PlayerManager.Instance.Update();
    }

    private void OnDestroy()
    {
        _physicThread?.Abort();
    }
}