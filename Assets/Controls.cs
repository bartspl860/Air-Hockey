using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public static class CoroutineUtil
{
    public static IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
    }
}
public class Controls : MonoBehaviour
{  
    public Rigidbody2D rb2d;    
    private Vector3 offset;
    private bool menu = true;

    [SerializeField]
    private TMP_Text counter;
    [SerializeField]
    private Canvas counterCanvas;

    [SerializeField]
    private GameObject gmmenu;
    [SerializeField]
    private GameObject score;    

    public IEnumerator CountBeforeStart()
    {
        counterCanvas.GetComponent<Canvas>().enabled = true;
        counter.text = "3";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
        counter.text = "2";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
        counter.text = "1";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(1));
        counterCanvas.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1f;
        menu = false;

    }
    void Start()
    {
        Time.timeScale = 0f;
        score.GetComponent<Canvas>().enabled = true;
        gmmenu.GetComponent<Canvas>().enabled = true;        
        menu = true;
    }
    void FixedUpdate()
    {
        if (Input.GetKeyDown("escape"))
        {
            toggleMenu();            
        }
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5f);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

        Vector3 dir = cursorPosition - (Vector3)transform.position;
        dir.Normalize();

        float distance = Vector3.Distance(cursorPosition, transform.position);
        float speed = distance * 1000;

        //rb2d.AddForce(dir * speed * Time.fixedDeltaTime);                

        if(transform.position.y < 0 || cursorPosition.y < 0)
        {
            rb2d.velocity = dir * speed * Time.deltaTime;
        }
        else
        {
            transform.position -= new Vector3(0f, transform.position.y, 0f);
            rb2d.velocity = Vector2.zero;
        }        
    }
    public void toggleMenu()
    {
        if (menu)
        {
            gmmenu.GetComponent<Canvas>().enabled = false;
            StartCoroutine(CountBeforeStart());
        }
        else
        {
            gmmenu.GetComponent<Canvas>().enabled = true;
            Time.timeScale = 0f;
            menu = true;
        }
    }

    public void exit()
    {
        Application.Quit();
    }

    public void again()
    {
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
