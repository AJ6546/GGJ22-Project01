using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string score = "";
    [SerializeField] float cooldown=45,resetTime;
    [SerializeField] GameObject gameOverPanel=null,creditsPanel=null;
    [SerializeField] Text ip;
    [SerializeField] TMP_Text hint;
    [SerializeField] Image msg;
    public int killCount = 0;
    public int reqkillCount = 4;
    private void Start()
    {
        resetTime = cooldown + (int)Time.time;
        if(gameOverPanel!=null)
            gameOverPanel.SetActive(false);
        if(msg!=null)
            msg.gameObject.SetActive(false);
        if (creditsPanel != null)
            creditsPanel.SetActive(false);
    }
    private void Update()
    {
        if ( Time.time > resetTime)
        {
            score = "";
            resetTime = cooldown + (int)Time.time;
        }
        if(hint)
            GetHints();
    }

    private void GetHints()
    {
        switch(ip.text)
        {
            case "1":
                hint.text = "Collect the orbs that appear. Both the players must touch the orb within a 45 second window!";
                break;
            case "2":
                hint.text = "There is 1 in 10 chances of the orb appearing on a tile. The Black Player must walk on white tile " +
                    "&& White player must walk on black tile. There is 1 in 2 chances of either tile appearing!";
                    break;
            case "3":
                hint.text = "Now the tiles could appear both in front or back of the player!";
                break;
            case "4":
                hint.text = "Tiles could appear on all 4 sides!";
                break;
            case "5":
                hint.text = "The orbs appear high in the sky now. Push the new Buttons and figure it out!";
                break;
            case "6":
                hint.text = "Creapy Enemies ! This Must be fun!";
                break;
            case "7":
                hint.text = "More Enemies! Use the powers and work togather!";
                break;
            case "8":
                hint.text = "New Powers!";
                break;
            case "9":
                hint.text = "Kill!!!";
                break;
            case "10":
                hint.text = "Run!!! or Die, That works too";
                break;
            case "11":
                hint.text = "Beat The Boss!!";
                break;
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
        if (lvl < 1 || lvl > 11)
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
    public void ShowCredits()
    {
        creditsPanel.SetActive(true);
    }
}
