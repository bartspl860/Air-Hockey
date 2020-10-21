using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private float vel;

    [SerializeField]
    private float helper; 
    public BallBehavior ball;
    private Rigidbody2D rb2dball;

    private Vector2 actual_position;
    private Vector2 last_position;
    void Start()
    {
        actual_position = ball.transform.position;
        rb2dball = ball.transform.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        last_position = actual_position;
        actual_position = ball.transform.position;
        float blockade_check = Vector2.Distance(actual_position, last_position);        

        Debug.Log(blockade_check);
        
        if (ball.transform.position.y > 0)
        {
            if (blockade_check < 0.007f)
            {
                StartCoroutine(cheatBall());
            }
            else
            {
                chaseBall();
            }            
        }        
        else
        {
            defaultPosition();
        }
    }
    private void defaultPosition()
    {
        float distance = Vector2.Distance(transform.position, new Vector2(0f, 4f));
        float speed = Mathf.Clamp(distance / helper, 0f, 1f) * vel;
        rb2d.velocity = (new Vector2(0f, 4f) - (Vector2)transform.position).normalized * speed;
    }
    private void chaseBall()
    {
        float distance = Vector2.Distance(transform.position, ball.transform.position);
        float speed = Mathf.Clamp(distance / helper, 0f, 1f) * vel;
        rb2d.velocity = ((Vector2)ball.transform.position - (Vector2)transform.position).normalized * speed;
    }
    public IEnumerator cheatBall()
    {
        defaultPosition();
        yield return new WaitForSeconds(0.1f);
        rb2dball.AddForce(ball.transform.up * -(ball.thrust)/10, ForceMode2D.Impulse);
    }
}
