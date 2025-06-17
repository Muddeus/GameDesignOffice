using UnityEngine;

public class BeginInvisible : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private void Start()
    {
        obj.SetActive(false);
    }
}
