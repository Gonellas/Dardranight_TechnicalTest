using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Score")]
    [SerializeField] private int _score = 0;
    [SerializeField] TextMeshProUGUI _scoreTxt;
    [SerializeField] TextMeshProUGUI _finalScoreTxt;

    [Header("UI Components")]
    [SerializeField] GameObject _deleteConfirmationPanel;
    [SerializeField] GameObject _canvasMainMenu;
    [SerializeField] GameObject _canvasScore;
    [SerializeField] GameObject _canvasLife;
    [SerializeField] GameObject _canvasLose;
    [SerializeField] GameObject _pauseCanvas;
    [SerializeField] TextMeshProUGUI _livesTxt;

    [Header("Player Components")]
    private int _lives;

    [Header("Pause Game")]
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;

        LoadGame();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void LoadGame()
    {
        _score = PlayerPrefs.GetInt("Data_Score", 0);
        _lives = PlayerPrefs.GetInt("Player_Lives", 3);
        Debug.Log("Game Loaded");
    }

    private void SaveGame()
    {
        PlayerPrefs.SetInt("Data_Score", _score);
        PlayerPrefs.SetInt("Player_Lives", _lives);

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }
    #region Score
    public void AddScore(int score)
    {
        _score += score;
        SaveGame();
    }

    public void ResetScore()
    {
        _score = 0;
        SaveGame();
    }

    private void UpdateUI()
    {
        if (_scoreTxt != null) _scoreTxt.text = _score.ToString();

        if (_livesTxt != null) _livesTxt.text = _lives.ToString(); 
    }
    #endregion

    #region Delete Values
    public void DeleteValues()
    {
        //AudioManager
        _deleteConfirmationPanel.SetActive(true);
    }

    public void ConfirmDeleteValues()
    {
        //AudioManager
        PlayerPrefs.DeleteAll();

        Debug.Log("Values Deleted");

        SceneManager.LoadScene(0);

        LoadGame();
        
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    #endregion

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        _pauseCanvas.SetActive(false);
    }

    public void PauseGame()
    {
        TogglePause();
        isPaused = true;

        _pauseCanvas.SetActive(true);
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
    }

    public bool IsPaused()
    {
        return isPaused;
    }
    public void LoseGame()
    {
        TogglePause();
        _canvasLife.SetActive(false);
        _canvasScore.SetActive(false);
        _canvasLose.SetActive(true);

        if(_finalScoreTxt != null) _finalScoreTxt.text = _score.ToString();

        PlayerPrefs.SetInt("Player_Lives", 3);
        PlayerPrefs.Save();

        ResetScore();
    }
}
