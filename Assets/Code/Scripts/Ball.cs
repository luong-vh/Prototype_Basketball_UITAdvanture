using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{

    public float p;
    private bool isThrow;
    private bool isLose;
    private Rigidbody2D rb;
    private Vector2 defaultPosition;
    public Vector2 TopLeftPosition;
    public Vector2 BottomRightPosition;
    private float ballScorePosition;

    public UnityEvent Scored;
   
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Start()
    {
        Down();
    }
    public void StartLevel()
    {
        RandomPosition();
        isLose = true;
        SetUp();
    }
    
    public void Throw(Vector2Variable variable)
    {
        if (isThrow)
        {
            rb.isKinematic = false;
            rb.AddForce(variable.Value * p, ForceMode2D.Force);
            isThrow = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals("out")) { isLose = true; SetUp(); }
        else if (!isLose) if (collision.transform.tag.Equals("ground")) StartCoroutine(Reset());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("ring")) ballScorePosition = this.transform.position.y;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("ring")) if(this.transform.position.y <ballScorePosition) { Scored.Invoke(); RandomPosition(); }
    }
    IEnumerator Reset()
    {
        isLose = true;
        yield return new WaitForSeconds(1.5f);
        if (isLose) SetUp();
    }
    public void SetUp()
    {
        if(isLose)
        {

            isLose = false;
            this.transform.position = defaultPosition;
            rb.isKinematic = true;
            isThrow = true;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
  
    public void RandomPosition()
    {
       System.Random random = new System.Random();
        float x = (float)random.NextDouble() * (TopLeftPosition.x - BottomRightPosition.x) + BottomRightPosition.x;
        float y = (float)random.NextDouble() * (TopLeftPosition.y - BottomRightPosition.y) + BottomRightPosition.y;
        defaultPosition = new Vector2(x,y);
    }
    public void Down()
    {
        this.transform.position = new Vector2(0,-100);
        isThrow = false;
        rb.isKinematic = true;
    }
}
