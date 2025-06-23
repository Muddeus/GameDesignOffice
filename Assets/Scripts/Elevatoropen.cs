using UnityEngine;

public class Elevatoropen : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("elevator trigger work");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered elevator zone");

            if (KeycardTracker.haveKeycard == true)
            {
                anim.Play("ElevatorDoorOpen");
                Debug.Log("elevator opening");
            }
        }
    }
}
