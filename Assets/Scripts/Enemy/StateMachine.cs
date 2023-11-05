using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    public BaseState activeState;
 

    public void Initialise()
    {
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }
 
    public void ChangeState(BaseState newState)
    {
        if (activeState != null) // null check
        {
            activeState.Exit();  //cleans the active state
        }

        activeState = newState;

        if (activeState != null)
        {
            activeState.stateMachine = this; //new state
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter(); //assign state enemy class
        }
    }
}
