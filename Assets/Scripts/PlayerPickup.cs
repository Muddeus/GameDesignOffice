using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    public GameObject pickupText;

    private void Start()
    {
        pickupText.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupText.SetActive(true);
            if (Input.GetKey(KeyCode.E))
            {
                this.gameObject.SetActive(false);

                KeycardTracker.haveKeycard = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickupText.SetActive(false);
        }
    }
}
