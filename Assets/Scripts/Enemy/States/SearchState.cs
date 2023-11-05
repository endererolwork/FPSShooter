using UnityEngine;

public class SearchState : BaseState
{
    private float moveTimer;
    private float searchTimer; //how long searching enemy
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.LastKnownPos); //setting destination
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }

        if (enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(3,5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10)); // Search for player.
                moveTimer = 0;
            }
            
            if (searchTimer > 3)
            {
               stateMachine.ChangeState(new PatrolState());  // turns back to the Patrol State.
            }
        }
    }

    public override void Exit()
    {
        
    }
}



