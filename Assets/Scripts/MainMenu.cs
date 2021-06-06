using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region Variables

    [Header("UI")]

    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    #endregion

    #region UnityLifecycle

    private void Start()
    {
        AddButtonsListeners();
    }

    #endregion

    #region EventHandlers

    private void HandlePlayClick()
    {
       SceneManager.LoadScene(SceneNames.Game);
    }

    private void HandleExitClick()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    #endregion

    #region Private methods
    
    private void AddButtonsListeners()
    {
        playButton.onClick.AddListener(HandlePlayClick);
        exitButton.onClick.AddListener(HandleExitClick);
    }

 
    #endregion
}
