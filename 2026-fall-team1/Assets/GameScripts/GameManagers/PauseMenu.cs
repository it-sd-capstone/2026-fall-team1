using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;

    private bool isPaused;
    private void Update ()
    {
        if (Input.GetKey(KeyCode.Escape)) {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 1.0f;
        isPaused = true;
    }
    
    public void Resume()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
        isPaused = false;
    }
}
