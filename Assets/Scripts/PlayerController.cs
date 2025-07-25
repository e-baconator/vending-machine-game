using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private HappinessBarUI happinessBar;
    public float Happiness, MaxHappiness;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        happinessBar.SetMaxHappiness(MaxHappiness);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            SetHappiness(-20f);
        }
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            SetHappiness(20f);
        }
    }

    public void SetHappiness(float happinessChange)
    {
        Happiness += happinessChange;
        Happiness = Mathf.Clamp(Happiness, 0, MaxHappiness);
        happinessBar.SetHappiness(Happiness);
    }
}
