using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SaveManager saveManager;
    [SerializeField] private GameObject noSavePopup;
    public void StartGame()
    { SceneManager.LoadScene("SampleScene"); }

    public void LoadGame()
    {
        if (saveManager == null)
        { Debug.LogError("SaveManager reference is missing."); return; }

        if (saveManager.HasSaveFile())
        {
            SceneManager.LoadScene("SampleScene");
        }
        else { noSavePopup.SetActive(true); }
    }

    public void ClosePopup() { noSavePopup.SetActive(false); }
   

}
