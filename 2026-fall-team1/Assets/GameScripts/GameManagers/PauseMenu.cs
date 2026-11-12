using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private bool isPaused;

    private void Start()
    {
        pauseScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
    }

    private void Update ()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
     
}
