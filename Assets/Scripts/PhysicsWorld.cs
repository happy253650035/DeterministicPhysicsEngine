using System.Threading;
using Base;
using BEPUphysics.Constraints;
using FixMath.NET;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private long _tick;
    private Thread _physicThread;
    private Space _physicsSpace;
    public Fix64 TimeSinceStart => (Fix64) (_tick * 0.033f);

    private void Awake()
    {
        SortObject();
        _tick = 0;
        Application.targetFrameRate = 30;
        if (Instance != null) return;
        Instance = this;
        Physics.autoSimulation = false;
        _physicsSpace = new Space();
        _physicsSpace.ForceUpdater.gravity = new BEPUutilities.Vector3(0, -gravity, 0);
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
            Tick();
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

    public void AddJoint(SolverUpdateable solver)
    {
        _physicsSpace.Add(solver);
    }

    public void RemoveJoint(SolverUpdateable solver)
    {
        _physicsSpace.Remove(solver);
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
    
    private void SortObject()
    {
        var id = 1;
        var gos = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var go in gos)
        {
            var pos = go.GetComponentsInChildren<PhysicsObject>();
            foreach (var p in pos)
            {
                if (p)
                {
                    p.id = id;
                    id++;
                }
            }
        }
        id = 1;
        gos = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var go in gos)
        {
            var bos = go.GetComponentsInChildren<BaseObject>();
            foreach (var b in bos)
            {
                if (b)
                {
                    b.id = id;
                    id++;
                }
            }
        }
    }

    private void Update()
    {
        if (!Asynchronous) Tick();
        PhysicsObjectManager.Instance.Update();
        PlayerManager.Instance.Update();
    }

    private void Tick()
    {
        CommandManager.Instance.Execute();
        PlayerManager.Instance.Tick();
        PhysicsTweenManager.Instance.Tick();
        _physicsSpace.Update();
        _tick++;
    }

    private void OnDestroy()
    {
        _physicThread?.Abort();
    }
}