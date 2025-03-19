using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rigidbody2d;
    public int id;
    public float moveSpeed = 2f;
    public float deadzone = 1f;

    private int direction = 0;
    private float moveSpeedRandomizer = 1f;

    private void Update()
    {
        if(id==2 && GameManager.instance.IsPlayer2AI())
        {
            MovementAI();
        } else
        {
            float movement = OnInput();
            Move(movement);
        }
            
    }

    private void MovementAI()
    {
        Vector2 ballPos = GameManager.instance.pilka.transform.position;
        
        if(Mathf.Abs(ballPos.y - transform.position.y) > deadzone)
        {
            direction = ballPos.y > transform.position.y ? 1 : -1;
        }

        if(Random.value < 0.01f)
        {
            moveSpeedRandomizer = Random.Range(0.9f,1.5f);
        }

        Move(direction);
    }

    private float OnInput()
    {
        float movement = 0f;

        switch(id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;
            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }


        return movement;
    }

    private void Move(float value)
    {
        Vector2 velo = rigidbody2d.velocity;
        velo.y = moveSpeed * moveSpeedRandomizer * value;
        rigidbody2d.velocity = velo;
    }

    public float GetPaddleHeight()
    {
        return transform.localScale.y;
    }

}
