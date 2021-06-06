using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game : MonoBehaviour
{
    [Header("UI")] [SerializeField] private GameObject pauseViewPrefab;
    [SerializeField] private GameObject gameOverViewPrefab;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text lifesText;
    

    [Header("Base Settings")] [SerializeField]
    private int lifesTotal;

    private static int lifesLeft = 0;
    private static int totalScore;
    private static bool isPaused;
    private static bool isFinished;
    private GameObject pauseView;
    


    public static bool IsPaused
    {
        get => isPaused;
    }

    public static bool IsFinished
    {
        get => isFinished;
    }

    public static int TotalScore
    {
        get => totalScore;
    }

    #region Unity lifecycle

    private void OnEnable()
    {
        AddHandlers();
    }

    private void OnDisable()
    {
        RemoveHandlers();
    }

    private void Start()
    {
        isFinished = false;
        isPaused = false;

        if (lifesLeft == 0)
        {
            lifesLeft = lifesTotal;
            totalScore = 0;
        }

        InitUIData();
    }

    private void Update()
    {
        if (isFinished)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    #endregion

    #region Private methods

    private void AddHandlers()
    {
        PauseView.OnClosed += TogglePause;
        GameOverView.OnClosed += HandleGameOverClosed;
        UsualItem.OnCatched += HandleUsualItemCatch;
        UsualItem.OnCatchFailed += HandleUsualItemCatchFailed;
        LifesItem.OnCatched += HandleLifesItemCatched;
        ScoreItem.OnCatched += HandleScoreItemCatched;
    }

    private void RemoveHandlers()
    {
        PauseView.OnClosed -= TogglePause;
        GameOverView.OnClosed -= HandleGameOverClosed;
        UsualItem.OnCatched -= HandleUsualItemCatch;
        UsualItem.OnCatchFailed -= HandleUsualItemCatchFailed;
        LifesItem.OnCatched -= HandleLifesItemCatched;
        ScoreItem.OnCatched -= HandleScoreItemCatched;
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseView = Instantiate(pauseViewPrefab, canvasTransform);
        }
        else
        {
            Time.timeScale = 1f;
            Destroy(pauseView);
        }
    }

    private void HandleUsualItemCatch(UsualItem item)
    {
        ChangeScore(item.ScoreForCatch);
    }

    private void HandleUsualItemCatchFailed(UsualItem item)
    {
        
        ChangeLifes(-1);
    }

    private void HandleLifesItemCatched(LifesItem item)
    {
        ChangeLifes(item.LifesNumber);
    }

     private void HandleScoreItemCatched(ScoreItem item)
     {
         ChangeScore(item.Score);
     }

    private void HandleGameOverClosed()
    {
        totalScore = 0;
        lifesLeft = 0;

        SceneManager.LoadScene(SceneNames.MainMenu);
    }
    
    private void ChangeScore(int score)
    {
        totalScore += score;

        if (totalScore < 0)
        {
            totalScore = 0;
        }
        UpdateScoreText();
    }

    private void ChangeLifes(int lifes)
    {
        lifesLeft += lifes;

        if (lifesLeft > lifesTotal)
        {
            lifesLeft = lifesTotal;
        }

        if (lifesLeft <= 0)
        {
            lifesLeft = 0;
            ShowGameOverView();
        }

        UpdateLifesText();
    }

    private void ShowGameOverView()
    {
        isFinished = true;
        Instantiate(gameOverViewPrefab, canvasTransform);
    }

    private void UpdateLifesText()
    {
        lifesText.text = $"x{lifesLeft}";
    }


    private void InitUIData()
    {
        UpdateScoreText();
        UpdateLifesText();
    }
    private void UpdateScoreText()
    {
        scoreText.text = $"{totalScore}";
    }

    #endregion
}