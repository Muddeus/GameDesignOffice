using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public class RedEnemyBehaviour : MonoBehaviour
{

    //The player gameobject
    [SerializeField] private GameObject player;

    //The speed for the ai when wandering and running
    [SerializeField] private float wanderSpeed, runSpeed;

    [Tooltip("the max amount of time the mob will be interested in the player even when it's no longer being observed")]
    [SerializeField] private float maxAlertTime;

    //What the current ai is determining to do
    [Tooltip("Keeps track of what the current state is")]
    [SerializeField] private State currentState;

    [Tooltip("the centrepoint from where the calulation of where to move next will occur")]
    [SerializeField] private Transform wanderCentrePoint;

    [Tooltip("The range of distances the ai will calculate its next movements")]
    [SerializeField] private float minRange, maxRange;

    [Tooltip("The range of time the ai will take before moving again")]
    [SerializeField] private float minWaitTime, maxWaitTime;

    [Tooltip("The range the ai will react to the player")]
    [SerializeField] private float alertDistance, chaseDistance;

    private Vector3 originalScale;
    private enum State
    {
        Idle,
        Wandering,
        Alerted,
        Chasing,
        Attack
    };

    private Rigidbody rb;

    private NavMeshAgent agent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Getting components for rigidbody and navmesh agent
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();

        originalScale = transform.localScale;

        //starts the ai in an idle state
        currentState = State.Idle;
        NextState();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void NextState()
    {
        switch (currentState)
        {
            case State.Idle:
                StartCoroutine(IdleState());
                break;
            case State.Wandering:
                StartCoroutine(WanderingState());
                break;
            case State.Alerted:
                StartCoroutine(AlertedState());
                break;
            case State.Chasing:
                StartCoroutine(ChasingState());
                break;
            case State.Attack:
                StartCoroutine(AttackState());
                break;
        }
    }

    IEnumerator IdleState()
    {
        Debug.Log("Entering Idle State");
        bool finishedWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        while (currentState == State.Idle)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0.0f)
            {
                Vector3 point;
                if (RandomPoint(agent.transform.position, minRange, maxRange, out point)) 
                {
                    //draws point the AI will navigate to
                    NextIdleDestination(point);
                    //sets the state to wandering
                    finishedWaiting = true;
                    currentState = State.Wandering;
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
        while (currentState == State.Wandering)
        {
            if (agent.remainingDistance <= agent.stoppingDistance) //if the agent is done with its current path...
            {

                if (chance <= 0.5f)
                {
                    //finds a new point and immediately heads towards it
                    Vector3 point;
                    if (RandomPoint(agent.transform.position, minRange, maxRange, out point))
                    {
                        //draws point the AI will navigate to
                        NextIdleDestination(point);
                    }
                    chance = Random.Range(0f, 1f);
                }
                else if (chance > 0.5f)
                {
                    //the ai is done wandering and will Idle
                    currentState = State.Idle;
                }
            }
            yield return null;
        }
        Debug.Log("Exiting Wandering State");
        NextState();
    }

    IEnumerator AlertedState()
    {

        while (currentState == State.Alerted)
        {

            yield return null;
        }
    }

    IEnumerator ChasingState()
    {
        while (currentState == State.Chasing)
        {

            yield return null;
        }
    }

    IEnumerator AttackState()
    {
        while (currentState == State.Attack)
        {

            yield return null;
        }
    }

    bool RandomPoint(Vector3 center, float minRadius, float maxRadius, out Vector3 result)
    {
        float distance = Random.Range(minRadius, maxRadius);
        Vector3 randompoint = center + Random.insideUnitSphere *  distance; //random point in a sphere
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
    /// <param name="point"></param>
    /// 
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

    //Search up Capela Games on github to find the other scripts


    /// <summary>
    /// Adds two integers together.
    /// </summary>
    /// <param name="num1">The first integer.</param>
    /// <param name="num2">The second integer.</param>
    /// <returns>The sum of num1 and num2.</returns>
    /// <exception cref="System.ArgumentException">Thrown when num1 or num2 is null.</exception>
    public static int Add(int num1, int num2)
    {
        return num1 + num2;
    }
}