using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]


public class MonsterAIScript : MonoBehaviour
{
    [Tooltip("Speed value for the ai")]
    [SerializeField] private float stalkSpeed, moveSpeed, runSpeed;

    //The object the ai uses to track the player
    [SerializeField] GameObject player;

    private NavMeshAgent agent;

    //The state of the monster
    [SerializeField] private State state = State.Still;
    [Tooltip("The current ai state of the enemy")]
    private enum State
    {
        Still,
        Wandering,
        Stalking,
        Hiding,
        Chase,
        Jumpscare
    };

    [Tooltip("the centrepoint from where the calulation of where to move next will occur")]
    [SerializeField] private Transform wanderCentrePoint;

    [Tooltip("The range of distances the ai will calculate its next movements")]
    [SerializeField] private float minRange, maxRange;

    [Tooltip("The range of time the ai will take before moving again")]
    [SerializeField] private float minWaitTime, maxWaitTime;

    [Tooltip("The range the ai will react to the player")]
    [SerializeField] private float alertDistance, chaseDistance;





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NextState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NextState()
    {
        switch (state)
        {
            case State.Still:
                StartCoroutine(StillState());
                break;
            case State.Wandering:
                StartCoroutine(WanderingState());
                break;
            case State.Stalking:
                StartCoroutine(StalkingState());
                break;
            case State.Hiding:
                StartCoroutine(HidingState());
                break;
            case State.Chase:
                StartCoroutine(ChaseState());
                break;
            case State.Jumpscare:
                StartCoroutine(JumpscareState());
                break;
        }
    }
    
    IEnumerator StillState()
    {
        Debug.Log("Entering Idle State");
        bool finishedWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        while (state == State.Still)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0.0f)
            {
                Vector3 point;
                if (RandomPoint(agent.transform.position, maxRange, out point))
                {
                    //draws point the AI will navigate to
                    NextIdleDestination(point);
                    //sets the state to wandering
                    finishedWaiting = true;
                    state = State.Wandering;
                }
            }
            yield return null; //waits for a frame
        }
        Debug.Log("Exiting Idle State");
        NextState();
    }

    IEnumerator WanderingState()
    {
        float chance = Random.Range(0f, 1f);
        while (state == State.Wandering)
        {
            if (agent.remainingDistance <= agent.stoppingDistance) //if the agent is done with its current path...
            {

                if (chance <= 0.5f)
                {
                    //finds a new point and immediately heads towards it
                    Vector3 point;
                    if (RandomPoint(agent.transform.position, maxRange, out point))
                    {
                        //draws point the AI will navigate to
                        NextIdleDestination(point);
                    }
                    chance = Random.Range(0f, 1f);
                }
                else if (chance > 0.5f)
                {
                    //the ai is done wandering and will Idle
                    agent.ResetPath();
                    state = State.Still;
                }
            }
            yield return null;
        }
        Debug.Log("Exiting Wandering State");
        NextState();
    }

    IEnumerator StalkingState()
    {

        while (state == State.Stalking)
        {



            yield return null;
        }
    }

    IEnumerator HidingState()
    {

        while (state == State.Hiding)
        {



            yield return null;
        }
    }

    IEnumerator ChaseState()
    {

        while (state == State.Chase)
        {



            yield return null;
        }
    }

    IEnumerator JumpscareState()
    {

        while (state == State.Jumpscare)
        {



            yield return null;
        }
    }

    bool RandomPoint(Vector3 center, float distance, out Vector3 result)
    {
        Vector3 randompoint = center + Random.insideUnitSphere * distance; //random point in a sphere
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randompoint, out hit, Random.Range(minRange, maxRange), NavMesh.AllAreas)) //Ddocumentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

    /// <summary>
    ///  Chooses a new idle destination
    /// </summary>
    private void NextIdleDestination(Vector3 point)
    {
        //draws point the AI will navigate to
        Debug.DrawRay(point, Vector3.up, Color.magenta, 1.0f);
        agent.SetDestination(point);
    }

    private void OnDrawGizmosSelected()
    {
        if (wanderCentrePoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(wanderCentrePoint.position, maxRange);

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(wanderCentrePoint.position, minRange);
        }
    }
}
