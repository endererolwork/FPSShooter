using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;

    public float bulletSpeed = 40;

    private float shotTimer;
    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0; //lock the lose player timer and increment the move and shot timers.
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (shotTimer > enemy.fireRate)
            {
                Shoot();
            }
            if (moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 8)
            {
                //change to search state for enemy.
                stateMachine.ChangeState(new PatrolState());
            }

        }

        
    }

    public void Shoot()
    {
        //store reference to the gun barrel and instantiate bullet and direction to the player. Adding force to the bullet
        Transform gunbarrel = enemy.gunBarrel;

        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
        
        Vector3 shootDriection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDriection * bulletSpeed;
        Debug.Log(("Shoot"));
        shotTimer = 0;
    }

    public override void Exit()
    {
        ;
    }
}
