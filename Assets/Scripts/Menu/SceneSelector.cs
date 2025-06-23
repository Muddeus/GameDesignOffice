using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
