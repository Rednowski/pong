using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PilkaScript : MonoBehaviour
{
    public Rigidbody2D cialo;
    public float maxStartAngle = 0.66f;
    public float predkosc = 1f;
    public float maxStartPosY = 4.1f;
    public float speedup = 1.1f;

    public float startPosX = 0f;

    private void Start()
    {
        GameManager.instance.onReset += ResetPilka;
        GameManager.instance.gameUI.onStartGame += ResetPilka;
    }

    private void OnDestroy()
    {
        GameManager.instance.onReset -= ResetPilka;
    }

    private void ResetPilka()
    {
        ResetPilkaPosition();
        Poczatek();
    }

    private void Poczatek()
    {
        Vector2 kierunek = Vector2.left;

        if (UnityEngine.Random.value < 0.5f)
        {
            kierunek = Vector2.right;
        }


        kierunek.y = Random.Range(-maxStartAngle, maxStartAngle);
        if(kierunek.y < 0.1f && kierunek.y > -0.1f)
        {
            kierunek.y = 0.4f;
        }
        cialo.velocity = kierunek * predkosc;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ObszarPunktuScript obszarPunktu = collision.GetComponent<ObszarPunktuScript>();
        if(obszarPunktu != null)
        {
            GameManager.instance.addPoint(obszarPunktu.id);
        }
    }

    private void ResetPilkaPosition()
    {
        float posY = Random.Range(-maxStartPosY, maxStartPosY);
        Vector2 position = new Vector2(startPosX, posY);
        transform.position = position;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Paddle paddle = collision.collider.GetComponent<Paddle>();
        Debug.Log("velocity: " + cialo.velocity.magnitude);
        if(paddle)
        {
            ChangeAngle(paddle, collision);
            if (GameManager.instance.IsWallMode())
            {
                GameManager.instance.addPoint(1);
            }
            GameManager.instance.gameAudio.PlayPong();
            if (cialo.velocity.magnitude < 25f)
            {
                cialo.velocity *= speedup;
            }
            else
            {
                cialo.velocity = cialo.velocity.normalized * 25f;
            }
        } else
        {
            GameManager.instance.gameAudio.PlayWall();
        }
        
    }

    private void ChangeAngle(Paddle paddle, Collision2D collision)
    {
        float dy = paddle.transform.position.y - transform.position.y;
        float f = dy / paddle.GetPaddleHeight() *2;

        Vector2 v = cialo.velocity;
        v.y = v.y - 1f * f;
        cialo.velocity = v.normalized * cialo.velocity.magnitude;
    }

}
