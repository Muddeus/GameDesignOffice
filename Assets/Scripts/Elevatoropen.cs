using UnityEngine;

public class Elevatoropen : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (KeycardTracker.haveKeycard == true)
            {
                anim.Play("ElevatorDoorOpen");
            }
        }
    }
}
