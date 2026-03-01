using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [SerializeField] private TextMeshProUGUI progress;
    [SerializeField] private TextMeshProUGUI timer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateCounterText(int currentAmount, int totalAmount)
    {
        progress.text = $"{currentAmount}/{totalAmount}";
    }

    public void UpdateTimerText(float elapsedTime)
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);

        timer.text = $"{minutes:00}:{seconds:00}";
    }
}