using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private Image panel;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Button adButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button helpButton;
    [SerializeField] private Text messageText;
    [SerializeField] private Text titleText;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject owl;
    [SerializeField] private GameObject bonus_Manager;
    [SerializeField] private Text score;
    [SerializeField] private GameObject stamina;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button startButton;
    [SerializeField] private Text highScore;
    [SerializeField] private int alwaysOwl = 500;
    [SerializeField] private Slider volume;
    private bool messageDestroyed = false;
    public static bool paused = false;
    private static bool started = false;
    private bool adPlayed = false;
    public void Pause()
    {
        if (!paused)
        {
            Time.timeScale = 0f;
            paused = true;
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Pause";
            if (!messageDestroyed)
            {
                messageDestroyed = true;
                Destroy(messageText.gameObject);
            }
                
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0.33f);
            restartButton.gameObject.SetActive(true);
            highScore.gameObject.SetActive(true);
            volume.gameObject.SetActive(true);
            helpButton.gameObject.SetActive(true);
            highScore.text = "High score: " + PlayerPrefs.GetInt("Score", 0);
            return;
        }
        if (paused)
        {
            Time.timeScale = 1f;
            paused = false;
            gameOverText.gameObject.SetActive(false);
            highScore.gameObject.SetActive(false);
            volume.gameObject.SetActive(false);
            helpButton.gameObject.SetActive(false);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0f);
            restartButton.gameObject.SetActive(false);
            return;
        }
    }
    public void Volume()
    {
        PlayerPrefs.SetFloat("Volume", volume.value);
        sword.GetComponent<DragMovement>().audioSourcePull.volume = PlayerPrefs.GetFloat("Volume",1f);
        sword.GetComponent<DragMovement>().audioSourceBreak.volume = PlayerPrefs.GetFloat("Volume", 1f);
        titleText.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1f);

    }
    public void Gameover()
    {
        if (!paused)
        {
            Time.timeScale = 0f;
            paused = true;
            if (!messageDestroyed)
            {
                messageDestroyed = true;
                Destroy(messageText.gameObject);
            }
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game over";
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0.33f);
            restartButton.gameObject.SetActive(true);
            highScore.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(false);
            highScore.text = "High score: " + PlayerPrefs.GetInt("Score", 0);
            if (!(Application.internetReachability == NetworkReachability.NotReachable) && DragMovement.gameOver && (PlayerPrefs.GetInt("adPlayed",0) == 0))
            {
                adButton.gameObject.SetActive(true);
            }
            return;
        }
        if (paused)
        {
            gameOverText.gameObject.SetActive(false);
            panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 0f);
            restartButton.gameObject.SetActive(false);
            highScore.gameObject.SetActive(false);
            pauseButton.gameObject.SetActive(true);
            Time.timeScale = 1f;
            paused = false;
            return;
        }
    }
    public void Restart()
    {
        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
        }
        DragMovement.gameOver = false;
        PlayerPrefs.SetInt("adPlayed", 0);
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        if (PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            SceneManager.LoadScene(1);
            return;
        }
        if (PlayerPrefs.GetInt("Started",0)==0)
        {
            
            float throwing = Random.Range(0f, 1f);
            Debug.Log(throwing);
            PlayerPrefs.SetInt("adPlayed", 0);
            DragMovement.gameOver = false;
            titleText.gameObject.SetActive(false);
            messageText.gameObject.SetActive(true);
            stamina.SetActive(true);
            sword.GetComponent<DragMovement>().enabled = true;
            score.gameObject.SetActive(true);
            bonus_Manager.gameObject.SetActive(true);
            
            pauseButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Started", 1);
        }
        
    }
    public void Tutorial()
    {
        PlayerPrefs.SetInt("HelpUsed", 1);
        PlayerPrefs.SetFloat("ScoreBeforeHelp", DragMovement.height);
        PlayerPrefs.SetFloat("PositionBeforeHelp", sword.transform.position.y);
        SceneManager.LoadScene(1);
        //while (SceneManager.GetActiveScene().buildIndex == 0) ;
        //return;
    }
    public static void RewardedVideo()
    {

        if (paused)
        {
            paused = false;
            Time.timeScale = 1f;
        }
        DragMovement.gameOver = false;
        PlayerPrefs.SetInt("adPlayed", 1);
        SceneManager.LoadScene(0);
    }
    private void Update()
    {
        if (DragMovement.gameOver)
        {
            if(!paused)
                Gameover();
        }
    }
    private void OnApplicationPause(bool pause)
    {
        if(PlayerPrefs.GetInt("adPlayed",0) == 0 && !paused)
        {
            PlayerPrefs.SetInt("Started", 0);
            PlayerPrefs.SetInt("adPlayed", 0);
        }
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Started", 0);
        PlayerPrefs.SetInt("adPlayed", 0);
    }
    private void Start()
    {
        
        volume.value = PlayerPrefs.GetFloat("Volume",1f);
        if (paused)
        {
            Pause();
        }
        Debug.Log(Time.timeScale);
        Time.timeScale = 1f;
        sword.GetComponent<DragMovement>().audioSourcePull.volume = PlayerPrefs.GetFloat("Volume", 1f);
        sword.GetComponent<DragMovement>().audioSourceBreak.volume = PlayerPrefs.GetFloat("Volume", 1f);
        titleText.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume", 1f);

        if (PlayerPrefs.GetInt("Started", 0) == 1)
        {
            
            titleText.gameObject.SetActive(false);
            messageText.gameObject.SetActive(true);
            stamina.SetActive(true);
            bonus_Manager.SetActive(true);
            sword.GetComponent<DragMovement>().enabled = true;
            score.gameObject.SetActive(true);
            pauseButton.gameObject.SetActive(true);
            startButton.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Started", 1);
        }
    }
}
