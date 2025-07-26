using UnityEngine;
using UnityEngine.UI;

public class HappinessBarUI : MonoBehaviour
{
    public float Happiness, MaxHappiness, Width, Height;
    [SerializeField] private RectTransform happinessBar;
    [SerializeField] private TMPro.TextMeshProUGUI percentageText;

    public void Start()
    {
        Happiness = MaxHappiness;
    }

    public void Update()
    {
        percentageText.text = (Happiness / MaxHappiness * 100) + "%";
    }

    public void SetMaxHappiness(float maxHappiness)
    {
        MaxHappiness = maxHappiness;
    }

    public void SetHappiness(float happiness)
    {
        Happiness = happiness;
        float newWidth = (Happiness / MaxHappiness) * Width;
        happinessBar.sizeDelta = new Vector2(newWidth, Height);
    }
}
