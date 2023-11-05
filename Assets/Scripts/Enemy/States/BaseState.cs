public abstract class BaseState
{
    public Enemy enemy;
    //instance of enemy class and state machine class
    public StateMachine stateMachine;

    public abstract void Enter(); //start

    public abstract void Perform(); // update

    public abstract void Exit(); //delete
    
    
}