using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuCanvasGO;
    [SerializeField] private GameObject winCanvasGO;

    private void Start()
    {
        mainMenuCanvasGO.SetActive(false);

        // Optional - call win panel on level enter
        winCanvasGO.SetActive(GameManager.Instance.IsLevelCompleted);
    }

    void Update()
    {
        if (InputManager.Instance.WasMenuOpenPressed)
        {
            if (!PauseManager.Instance.IsPaused)
                Pause();
        }

        // Comment from here if you want to disable unpause on same button
        else if (InputManager.Instance.WasMenuClosePressed)
        {
            if (PauseManager.Instance.IsPaused)
            {
                Unpause();
            }
        }
    }

    private void Pause()
    {
        if (winCanvasGO.activeSelf) return;

        PauseManager.Instance.PauseGame();
        mainMenuCanvasGO.SetActive(true);
    }

    private void Unpause()
    {
        PauseManager.Instance.UnpauseGame();
        mainMenuCanvasGO.SetActive(false);
    }

    public void OnResumePress()
    {
        Unpause();
    }

    public void OnResetPress()
    {
        GameManager.Instance.ResetScore();
        Unpause();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowLevelComplete()
    {
        // Win condition
        winCanvasGO.SetActive(true);
    }

    public void OnMainMenuPress()
    {
        // Main Menu Return
        GameManager.Instance.SaveElapsedTime();
        Unpause();
        SceneManager.LoadScene("MainMenu");
    }
}
