using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource audioSound;
    public AudioSource audioMusic;

    public AudioClip pong1;
    public AudioClip pong2;
    public AudioClip wall;
    public AudioClip win;
    public AudioClip point;

    int counterSound = 0;
    public void PlayPong()
    {
        if (counterSound % 2 == 0)
        {
            audioSound.PlayOneShot(pong1);
            counterSound = 1;
        }
        else
        {
            audioSound.PlayOneShot(pong2);
            counterSound = 0;
        }
    }


    public void PlayWall()
    {
        audioSound.PlayOneShot(wall);
    }

    public void PlayWin()
    {
        audioSound.PlayOneShot(win);
    }
    public void PlayPoint()
    {
        audioSound.PlayOneShot(point);
    }

    public void StopMusic()
    {
        if(audioMusic.isPlaying)
            audioMusic.Stop();
    }

    public void PlayMusic()
    {
        if (!audioMusic.isPlaying)
            audioMusic.Play();
    }
}
