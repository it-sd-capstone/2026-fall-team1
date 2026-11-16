using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInputManager : MonoBehaviour
{

    [SerializeField] private PauseMenu pauseMenu;

    public void OnPause() {  if (pauseMenu != null) { pauseMenu.TogglePause(); } }
    
}
