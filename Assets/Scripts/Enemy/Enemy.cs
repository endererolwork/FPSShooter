using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;

    private NavMeshAgent agent;

    private LevelSystem ls;
    public NavMeshAgent Agent
    {
        get => agent;
    }

    [SerializeField] private string currentState;

    public PathAI path;
    private Vector3 lastKnownPos;
    public Vector3 LastKnownPos
    {
        get => lastKnownPos; set => lastKnownPos = value;
    }
    public GameObject Player
    {
        get => player;
    }

    private GameObject player;
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    [Header("Weapon Values")] 
    public Transform gunBarrel;
    [Range(0.1f, 10f)]
    public float fireRate;
    
    
    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
        ls = GetComponent<LevelSystem>();
      
    }

    // Update is called once per frame
    void Update()
    {
        CanSeePlayer();
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer()
    {
        if (player != null)
        {
            // is the player close ?
            if (Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward); // angle to player
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray((transform.position + Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit(); // raycast if its blocked by the object
                    if (Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    public void EnemyDie()
    {
        ls.GainExp(10);
        Destroy(this.gameObject);
    }
    
}