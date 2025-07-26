using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HappinessBarUI happinessBar;
    [SerializeField] TMPro.TextMeshProUGUI warningText;
    public float Happiness, MaxHappiness;
    private List<string> collisionMessages = new List<string>() { "HEY, WATCH IT.", "WHAT DO YOU THINK YOU'RE DOING?", "GET OUT OF MY WAY." };
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
            SetHappiness(-10f);
            StartCoroutine(ShowWarningText(collisionMessages[Random.Range(0, collisionMessages.Count)]));
        }
    }

    IEnumerator ShowWarningText(string message)
    {
        warningText.text = message;
        yield return new WaitForSeconds(1.5f);
        warningText.text = string.Empty;
    }
}
