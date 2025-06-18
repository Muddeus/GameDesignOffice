using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class StateMachineMovement : MonoBehaviour
{
    public enum State
    {
        Walk,
        Crouch,
        Sprint
    }

    [SerializeField] private State stateCurrent;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float gravityUp, gravityDown;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] Transform camTrans;
    [SerializeField] Transform playerTrans;
    [SerializeField] CapsuleCollider playerCol;
    [SerializeField] GameObject SprintImage;
    [SerializeField] GameObject CrouchImage;
    [SerializeField] GameObject WalkImage;

    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateCurrent = State.Walk;
        //get our components
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        //make sure our rigidbody is set up correctly
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;

    }

    // Update is called once per frame
    void Update()
    {
        //depending on the current state, choose from a set of behcaour to follow
        switch (stateCurrent)
        {
            case State.Walk:
                WalkState();
                break;
            case State.Crouch:
                CrouchState();
                break;
            case State.Sprint:
                SprintState();
                break;
        }

    }

    private void WalkState()
    {
        Vector3 inputMovement = GetMovementFromInput();
        inputMovement *= walkSpeed;
        inputMovement.y = rb.linearVelocity.y - gravityDown * Time.deltaTime;

        rb.linearVelocity = inputMovement;

        walkSpeed = 5;

        playerCol.height = 2;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stateCurrent = State.Crouch;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateCurrent = State.Sprint;
        }

        if (WalkImage.active == false)
        {
            WalkImage.active = true;
            CrouchImage.active = false;
            SprintImage.active = false;
        }

    }

    private void CrouchState()
    {
        Vector3 inputMovement = GetMovementFromInput();
        inputMovement *= walkSpeed;
        inputMovement.y = rb.linearVelocity.y - gravityDown * Time.deltaTime;

        rb.linearVelocity = inputMovement;

        //playerTrans.transform.position.y - 0.5f;

        walkSpeed = 2.5f;

        playerCol.height = 0.8f;

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            stateCurrent = State.Walk;
        }

        if (CrouchImage.active == false)
        {
            WalkImage.active = false;
            CrouchImage.active = true;
            SprintImage.active = false;
        }
    }


    private void SprintState()
    {
        Vector3 inputMovement = GetMovementFromInput();
        inputMovement *= walkSpeed;
        inputMovement.y = rb.linearVelocity.y - gravityDown * Time.deltaTime;

        rb.linearVelocity = inputMovement;

        walkSpeed = 9f;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            stateCurrent = State.Walk;
        }

        if (SprintImage.active == false)
        {
            WalkImage.active = false;
            CrouchImage.active = false;
            SprintImage.active = true;
        }
    }

    private Vector3 GetMovementFromInput()
    {
        Vector2 inputThisFrame = new Vector2();
        inputThisFrame.x = Input.GetAxis("Horizontal");
        inputThisFrame.y = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(inputThisFrame.x, 0, inputThisFrame.y);

        transform.localEulerAngles = new Vector3(0, camTrans.localEulerAngles.y);
        //translate direction from world ot local
        moveDirection = transform.TransformDirection(moveDirection);

        return moveDirection;
    }
}