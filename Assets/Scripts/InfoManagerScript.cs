using UnityEngine;
using UnityEngine.SceneManagement;

public class Info : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main"); // Replace with your actual game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}