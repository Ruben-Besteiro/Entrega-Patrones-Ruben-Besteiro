using UnityEngine;

public class StateMachine
{
    private IState currentState;

    public SeekState seekState;
    public ChaseState chaseState;

    public StateMachine(Enemy enemy)
    {
        // Creamos una variable por cada estado
        seekState = new SeekState(enemy);
        chaseState = new ChaseState(enemy, enemy.moveController);
    }

    public void Start(IState state)
    {
        currentState = state;
        state.Enter();
    }

    public void Stop()
    {
        currentState.Exit();
        currentState = null;
    }

    public void Update(float deltaTime)
    {
        if (currentState != null)
        {
            currentState.Update(deltaTime);
        }
    }

    public void ChangeState(IState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
