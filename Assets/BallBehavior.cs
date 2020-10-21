using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallBehavior : MonoBehaviour
{    
    public Rigidbody2D rb2d_ball;        

    public float thrust = 0f;
    [SerializeField]
    private TMP_Text score;

    public Transform t_ball;
    public Controls c;    

    private int score_player = 0;
    private int score_computer = 0;

    [SerializeField]
    private GameObject endScoreW;
    [SerializeField]
    private GameObject endScoreL;
    [SerializeField]
    private GameObject menuWin;
    [SerializeField]
    private GameObject MenuLose;

    void Start()
    {        
        rb2d_ball.AddForce(t_ball.up * -thrust, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {            

        if(transform.position.y <= -4.7f)
        {
            score_computer++;
            rb2d_ball.velocity = transform.position * 0;
            transform.position = new Vector3(0f, 0f, -5f);

            c.transform.position = new Vector3(0f, -3f, -5f);

            rb2d_ball.AddForce(t_ball.up * -thrust, ForceMode2D.Impulse);
        }
        if(transform.position.y >= 4.7f)
        {
            score_player++;
            rb2d_ball.velocity = transform.position * 0;
            transform.position = new Vector3(0f, 0f, -5f);

            c.transform.position = new Vector3(0f, -3f, -5f);

            rb2d_ball.AddForce(t_ball.up * thrust, ForceMode2D.Impulse);
        }

        score.text = score_computer + " : " + score_player;
        endScoreW.GetComponent<TMP_Text>().text = score_player + " : " + score_computer;
        endScoreL.GetComponent<TMP_Text>().text = score_player + " : " + score_computer;

        if (score_computer >= 10)
        {
            Time.timeScale = 0f;
            MenuLose.GetComponent<Canvas>().enabled = true;
        }
        if (score_player >= 10)
        {
            Time.timeScale = 0f;
            menuWin.GetComponent<Canvas>().enabled = true;
        }
    }
}
