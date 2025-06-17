using UnityEngine;
using UnityEngine.SceneManagement;


//This script was written by Daniel
public class MainMenu : MonoBehaviour
{
    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
