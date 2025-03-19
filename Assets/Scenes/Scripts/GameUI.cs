using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Wynik scoreTextPlayer1, scoreTextPlayer2;
    public GameObject menuObject;
    public TextMeshProUGUI winText;

    public Action onStartGame;

    public void updateWynik(int scorePlayer1, int scorePlayer2)
    {
        scoreTextPlayer1.setWynik(scorePlayer1);
        scoreTextPlayer2.setWynik(scorePlayer2);
    }

    public void HighlightScore(int id)
    {
        if (id == 1)
        {
            scoreTextPlayer1.Highlight();
        }
        else
        {
            scoreTextPlayer2.Highlight();
        }
    }

    public void OnStartGameButtonClick()
    {
        GameManager.instance.gameAudio.StopMusic();
        GameManager.instance.SetPvP();
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnStartAiGameButtonClick()
    {
        GameManager.instance.gameAudio.StopMusic();
        GameManager.instance.SetPvAI();
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }

    public void OnStartWallGameButtonClick()
    {
        GameManager.instance.gameAudio.StopMusic();
        GameManager.instance.SetPvWall();
        menuObject.SetActive(false);
        onStartGame?.Invoke();
    }
    public void OnGameEnd(int winnerID)
    {
        GameManager.instance.gameAudio.PlayMusic();
        menuObject.SetActive(true);
        if (winnerID  > 0)
        {
            winText.text = $"Player {winnerID} wins!";
        }
        if (winnerID == 0)
        {
            winText.text = $"You got {scoreTextPlayer1.text.text} points!";
        }
    }

}
