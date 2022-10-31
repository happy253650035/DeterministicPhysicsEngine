using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
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
    private readonly List<PhysicsObject> _physicsObjects = new();
    private readonly List<BaseCharacterController> _characterControllers = new();

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
            _physicsSpace.Update();
            Thread.Sleep(20);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void AddPhysicsObject(PhysicsObject po)
    {
        Instance._physicsSpace.Add(po.mEntity);
        _physicsObjects.Add(po);
    }

    public void RemovePhysicsObject(PhysicsObject po)
    {
        Instance._physicsSpace.Remove(po.mEntity);
        _physicsObjects.Remove(po);
    }

    public void AddPhysicsCharacterController(BaseCharacterController controller)
    {
        Instance._physicsSpace.Add(controller.mCharacterController);
        _characterControllers.Add(controller);
    }

    public void RemovePhysicsCharacterController(BaseCharacterController controller)
    {
        Instance._physicsSpace.Remove(controller.mCharacterController);
        _characterControllers.Remove(controller);
    }

    public void PositionPhysicsObjects()
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

        foreach (var character in _characterControllers)
        {
            var worldPos = character.mCharacterController.Body.position;
            var x = (float) worldPos.X;
            var y = (float) worldPos.Y;
            var z = (float) worldPos.Z;
            character.transform.position = new Vector3(x, y, z);
        }
    }

    private void Update()
    {
        if (!Asynchronous)
        {
            _physicsSpace.Update();
        }

        PositionPhysicsObjects();
    }

    private void OnDestroy()
    {
        _physicThread?.Abort();
    }
}