using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IObserver
{
    public StateMachine stateMachine;
    public NavMeshMoveController moveController;
    public Transform playerPos;
    [SerializeField] public GameObject pared;

    public void OnNotify(bool seeking)
    {
        stateMachine.seekState.OnNotify(seeking);
    }

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        stateMachine.Start(stateMachine.seekState);
    }

    void Start()
    {
        GameManager.Instance.Subscribe(this);
    }

    void OnDestroy()
    {
        GameManager.Instance.Unsubscribe(this);
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);
    }
}