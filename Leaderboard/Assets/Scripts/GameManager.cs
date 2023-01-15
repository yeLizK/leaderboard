using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
    public bool isGameOn;

    private float timer;
    [HideInInspector]
    public int timerInSec;

    [SerializeField]
    private TMP_Text timerText;
    [SerializeField]
    private Button StartButton;
    [SerializeField]
    private GameObject StartGamePanel,GameOverPanel, LeaderboardPanel;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    private void Start()
    {
        isGameOn = false;
        timer = 0.0f;
        timerText.text = "00:00";
    }
    private void Update()
    {
        if(isGameOn)
        {
            timer += Time.deltaTime;
            timerInSec = (int)(timer % 60);
            timerText.text = "Score: "+ timerInSec.ToString();
        }

    }
    public void StartGame()
    {
        timer = 0.0f;
        isGameOn = true;
        StartGamePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        LeaderboardPanel.SetActive(false);
    }

    public void GameOver()
    {
        isGameOn = false;
        timer = 0.0f;
        PlayFabManager.Instance.SendLeaderboard(timerInSec);
        GameOverPanel.SetActive(true);
    }

    public void OpenLeaderboardPanel()
    {
        LeaderboardPanel.SetActive(true);
        GameOverPanel.SetActive(false);
        PlayFabManager.Instance.GetLeaderboard();
    }
}
