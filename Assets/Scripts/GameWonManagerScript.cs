using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonManagerScript : MonoBehaviour
{
    public Canvas gameWonCanvas;
    public bool isGameWon = false;

    void Start()
    {
        gameWonCanvas.enabled = false;
    }

    void Update()
    {
        if (isGameWon)
        {
            StartCoroutine(Restart());
        }
    }

    public IEnumerator Restart()
    {
        gameWonCanvas.enabled = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Menu");
    }

    public void TriggerGameWon()
    {
        isGameWon = true;
    }
}
