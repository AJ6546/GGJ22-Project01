using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string score = "";
    [SerializeField] float cooldown=45,resetTime;
    [SerializeField] GameObject gameOverPanel=null;
    [SerializeField] Text ip;
    [SerializeField] Image msg;
    private void Start()
    {
        resetTime = cooldown + (int)Time.time;
        if(gameOverPanel!=null)
            gameOverPanel.SetActive(false);
        if(msg!=null)
            msg.gameObject.SetActive(false);
    }
    private void Update()
    {
        if ( Time.time > resetTime)
        {
            score = "";
            resetTime = cooldown + (int)Time.time;
        }
    }
    public void onRestartButtonClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void onQuitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
    public void onNewGameButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void onEnterLevelButtonClicked()
    {
        int lvl = Convert.ToInt32(ip.text);
        if (lvl < 1 || lvl > 6)
        {
            msg.gameObject.SetActive(true);
            return;
        }
        SceneManager.LoadScene(lvl);
    }
    public void onExitButtonClicked()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}
