using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    
    public float moveSpeed ;
    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 _movement;
    public bool canMove;

    // Update is called once per frame
    void Update()
    {
        // input handle
        _movement.x = Input.GetAxisRaw("Horizontal");
        _movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", _movement.x);
        animator.SetFloat("Vertical", _movement.y);
        animator.SetFloat("Speed", _movement.sqrMagnitude);
        if(Input.GetAxisRaw("Horizontal")==1||
        Input.GetAxisRaw("Horizontal")==-1||
        Input.GetAxisRaw("Vertical")==1||
        Input.GetAxisRaw("Vertical")==-1)
        {
            animator.SetFloat("LastHor",Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastVer",Input.GetAxisRaw("Vertical"));
        }
    }

    private void FixedUpdate()
    {
        // movement handle
        if (canMove)
        {
            rb.MovePosition(rb.position + _movement * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}
