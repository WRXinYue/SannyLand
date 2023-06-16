using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemies_Eagle : Enemy
{

    public float Speed;//相当于定义X的速度
    public Transform top, bottom;

    private Rigidbody2D rb;
    //private Collider2D coll;
    private float TopY, BottomY;
    private bool isUp = true;



    protected override void Start() 
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        TopY = top.position.y;
        BottomY = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }


    void Movement()
    {
        if(isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < BottomY)
            {
                isUp = true;
            }
        }
    }
}
