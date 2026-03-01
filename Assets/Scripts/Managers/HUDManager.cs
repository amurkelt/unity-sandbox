using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI time;

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

    public void UpdateCounterText(int currentAmount)
    {
        score.text = currentAmount.ToString();
    }

    public void UpdateTimerText(float elapsedTime)
    {
        time.text = elapsedTime.ToString();
    }
}