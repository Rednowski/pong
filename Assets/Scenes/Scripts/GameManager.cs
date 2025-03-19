using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameUI gameUI;
    public GameAudio gameAudio;
    public static GameManager instance;
    public PilkaScript pilka;
    public int scorePlayer1, scorePlayer2;
    public Action onReset;
    public int maxScore = 5;
    public GameObject sciana;
    public PlayMode playMode;

    public enum PlayMode
    {
        PvP,
        PvAI,
        PvWall
    }


    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            gameUI.onStartGame += OnStartGame;
        }
    }

    private void OnDestroy()
    {
        gameUI.onStartGame -= OnStartGame;
    }

    public void addPoint(int id)
    {
        if (IsWallMode())
        {
            if (id == 1)
            {
                scorePlayer1++;
                gameUI.updateWynik(scorePlayer1, scorePlayer2);
            }
            else
            {
                gameUI.OnGameEnd(0);
            }
        }
        else
        {
            if (id == 1) scorePlayer1++;
            if (id == 2) scorePlayer2++;

            gameUI.updateWynik(scorePlayer1, scorePlayer2);
            gameUI.HighlightScore(id);
            checkIfWin();
        }

    }

    private void checkIfWin()
    {
        int winnerID = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if(winnerID != 0)
        {
            gameAudio.PlayWin();
            gameUI.OnGameEnd(winnerID);
        } else
        {
            gameAudio.PlayPoint();
            onReset?.Invoke();
                
        }
    }

    private void OnStartGame()
    {
        scorePlayer1 = 0;
        if(IsWallMode())
        {
            scorePlayer2 = -20000000;
        }
        else
        {
            scorePlayer2 = 0;
        }
        gameUI.updateWynik(scorePlayer1, scorePlayer2);
    }

    public bool IsPlayer2AI()
    {
        return playMode == PlayMode.PvAI;
    }
    public bool IsWallMode()
    {
        return playMode == PlayMode.PvWall;
    }
    public bool IsPvPMode()
    {
        return playMode == PlayMode.PvP;
    }

    public void SetPvP()
    {
        sciana.SetActive(false);
        playMode = PlayMode.PvP;
        gameAudio.StopMusic();
    }

    public void SetPvAI()
    {
        sciana.SetActive(false);
        playMode = PlayMode.PvAI;
        gameAudio.StopMusic();
    }

    public void SetPvWall()
    {
        sciana.SetActive(true);
        playMode = PlayMode.PvWall;
        gameAudio.StopMusic();
    }
}
