using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private SaveManager saveManager;

    private bool isPaused;

    private void Start()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void TogglePause() { if (isPaused) Resume(); else Pause(); }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    
    public void Resume()
    {
        if (!isPaused) return;
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    public void SaveGame() { if (saveManager != null) saveManager.SaveGame(); }
    public void LoadGame() { if (saveManager != null) saveManager.LoadGame(); }
     
}
