using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ElevatorRide : MonoBehaviour
{
    [SerializeField] GameObject elevatorBlock;
    [SerializeField] GameObject winScreen;
    [SerializeField] Animator anim;
    public bool hasClosed;
     
    private void Start()
    {
        elevatorBlock.SetActive(false);
        hasClosed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasClosed == false)
        {
            hasClosed = true;
            anim.Play("ElevatorDoorClose");

            elevatorBlock.SetActive(true);
            StartCoroutine(EndGame());

        }
    }

    IEnumerator EndGame()
    {
        winScreen.SetActive(true);
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

    }
}
