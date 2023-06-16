using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Frog : Enemy
{
    public Transform leftpotint, rightpoint;
    public float Speed, JumpForce;
    public LayerMask Ground;
    public bool isHurt;//默认是false

    private float leftx, rightx;
    private bool Faceleft = true;
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D coll;


    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //Anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        transform.DetachChildren();
        leftx = leftpotint.position.x;
        rightx = rightpoint.position.x;

    }


    void Update()
    {
        Movement();
        SwithchAnim();
    }

    void Movement()
    {
        if (Faceleft)//面向左侧
        {
            if (coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if (transform.position.x < leftx)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    Faceleft = false;
                }
            }
            else//面向右侧
            {
            if (coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
                if (transform.position.x > rightx)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    Faceleft = true;
                }
            }
        }

    void SwithchAnim()
    {
        if(Anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
            if(coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
            {
                Anim.SetBool("falling", false);
            }
        }
    }
 }



