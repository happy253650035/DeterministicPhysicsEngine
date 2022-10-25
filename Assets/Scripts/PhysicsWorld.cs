using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Space = BEPUphysics.Space;

public class PhysicsWorld : MonoBehaviour
{
    public bool Asynchronous;
    public static PhysicsWorld Instance;
    public Space PhysicsSpace;

    private bool _useThread;
    private Thread _physicThread;
    private readonly List<PhysicsObject> _physicsObjects = new();
    private readonly List<BaseCharacterController> _characterControllers = new();

    private void Awake()
    {
        Application.targetFrameRate = 30;
        if (Instance != null) return;
        Instance = this;
        Physics.autoSimulation = false;
        PhysicsSpace = new Space();
        PhysicsSpace.ForceUpdater.gravity = new BEPUutilities.Vector3(0, -9.81m, 0);
        PhysicsSpace.TimeStepSettings.TimeStepDuration = 0.02M;
        if (!Asynchronous) return;
        _physicThread = new Thread(Run);
        _physicThread.Start();
    }

    private void Run()
    {
        while (true)
        {
            PhysicsSpace.Update();
            Thread.Sleep(20);
        }
        // ReSharper disable once FunctionNeverReturns
    }

    public void AddPhysicsObject(PhysicsObject po)
    {
        Instance.PhysicsSpace.Add(po.mEntity);
        _physicsObjects.Add(po);
    }

    public void RemovePhysicsObject(PhysicsObject po)
    {
        Instance.PhysicsSpace.Remove(po.mEntity);
        _physicsObjects.Remove(po);
    }

    public void AddPhysicsCharacterController(BaseCharacterController controller)
    {
        Instance.PhysicsSpace.Add(controller.mCharacterController);
        _characterControllers.Add(controller);
    }

    public void RemovePhysicsCharacterController(BaseCharacterController controller)
    {
        Instance.PhysicsSpace.Remove(controller.mCharacterController);
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
            po.transform.position = new Vector3((float) x - po.mCenterX, (float) y - po.mCenterY, (float) z - po.mCenterZ);
            var orientation = po.mEntity.orientation;
            po.transform.rotation = new Quaternion((float)orientation.X,
                (float)orientation.Y, (float)orientation.Z,
                (float)orientation.W);
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
            PhysicsSpace.Update();
        }
        PositionPhysicsObjects();
    }

    private void OnDestroy()
    {
        _physicThread?.Abort();
    }
}
