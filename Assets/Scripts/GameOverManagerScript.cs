using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Canvas gameOverCanvas;
    public bool isGameOver = false;

    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    void Update()
    {
        if (isGameOver)
        {
            StartCoroutine(Restart());
        }
    }

    public IEnumerator Restart()
    {
        gameOverCanvas.enabled = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
    }
}
