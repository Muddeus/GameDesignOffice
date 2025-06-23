using UnityEngine;

public class KeycardTracker : MonoBehaviour
{
    [SerializeField] static public bool haveKeycard;

    private void Update()
    {
        Debug.Log(haveKeycard);
    }
}
