using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HappinessBarUI happinessBar;
    [SerializeField] TMPro.TextMeshProUGUI warningText;
    [SerializeField] VMStatus vmStatus1;
    [SerializeField] VMStatus vmStatus2;
    [SerializeField] VMStatus vmStatus3;
    [SerializeField] VMStatus vmStatus4;
    [SerializeField] VMStatus vmStatus5;
    public float Happiness, MaxHappiness;
    private List<string> collisionMessages = new List<string>() { "HEY, WATCH IT.", "WHAT DO YOU THINK YOU'RE DOING?", "GET OUT OF MY WAY." , "UGH, WHY ARE YOU HERE?"};
    void Start()
    {
        happinessBar.SetMaxHappiness(MaxHappiness);
        warningText.text = string.Empty;
    }

    void Update()
    {
        if (Time.frameCount % 500 == 0)
        {
            SetHappiness(-1f);
        }
        if (vmStatus1.isComplete && vmStatus2.isComplete && vmStatus3.isComplete && vmStatus4.isComplete && vmStatus5.isComplete)
        {
            FindFirstObjectByType<GameWonManagerScript>().TriggerGameWon();
        }
        if (Happiness == 0)
        {
            FindFirstObjectByType<GameOverManager>().TriggerGameOver();
        }
    }

    public void SetHappiness(float happinessChange)
    {
        Happiness += happinessChange;
        Happiness = Mathf.Clamp(Happiness, 0, MaxHappiness);
        happinessBar.SetHappiness(Happiness);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            //Debug.Log("Player touched NPC!");
            SetHappiness(-5f);
            StartCoroutine(ShowText(collisionMessages[Random.Range(0, collisionMessages.Count)], Color.red));
        }
    }

    public IEnumerator ShowText(string message, Color color)
    {
        warningText.color = color;
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }
}
