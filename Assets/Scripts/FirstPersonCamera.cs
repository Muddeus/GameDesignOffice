using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [Tooltip("Defines how fast the camera will move.")]
    [SerializeField] private float sensitivity;

    [Tooltip("The transform of the player object")]
    [SerializeField] private Transform playerTransform;

    [SerializeField] private float verticalRotationMin = -30;
    [SerializeField] private float verticalRotationMax = 60;

    [Tooltip("How far up the player model the camera should be")]
    [SerializeField] private float playerEyeOffset = 0.5f;

    private float currentHorizontalRotation;
    private float currentVerticalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHorizontalRotation = transform.localEulerAngles.y;
        currentVerticalRotation = transform.localEulerAngles.x;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        currentHorizontalRotation += Input.GetAxis("Mouse X") * sensitivity;
        
        //we use - here because screen start at 0 at the top of and get bigger going down,
        //which is the opposite of Unity.
        currentVerticalRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        //constrain the camera's up/down so we dont backflip
        currentVerticalRotation = Mathf.Clamp(currentVerticalRotation, verticalRotationMin, verticalRotationMax);

        Vector3 newRotation = new Vector3();
        newRotation.x = currentVerticalRotation; 
        newRotation.y = currentHorizontalRotation;


        transform.localEulerAngles = newRotation;

        //go to the player
        transform.position = playerTransform.position;
        //go up a bit to the player's eyes
        transform.position += Vector3.up * playerEyeOffset;
    }
}
