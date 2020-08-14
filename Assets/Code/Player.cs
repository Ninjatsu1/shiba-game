using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    private Rigidbody2D myRigidbody;

    [SerializeField]
    private float MovementSpeed = 30;

    public bool facingRight; //Kertoo mihin suuntaan pelaaja menee
    private Animator myAnimator;

    public float jumpForce = 30;
    public bool OnAir = false;
    void Start()
    {

        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        this.myRigidbody.isKinematic = false;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //Kutsutaan funktiot
        HandleMovement(horizontal);
        flip(horizontal);



        if (Input.GetKeyDown(KeyCode.Space) && (!this.OnAir))
        {
            this.myRigidbody.AddForce(Vector2.up * this.jumpForce);
            this.OnAir = true;
        }

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        this.OnAir = false;

    }


    void HandleMovement(float horizontal)
    {

        myRigidbody.velocity = new Vector2(horizontal * MovementSpeed * Time.deltaTime * 10, myRigidbody.velocity.y); //x = -1, y = 0
        if (horizontal == 0)
        {
            myAnimator.SetBool("IsWalking", false);
        }
        else
        {
            myAnimator.SetBool("IsWalking", true);

        }
    }

    void flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1; //Kerrotaan miinus luvulla. Skaala saa negatiivisen arvon ja flippaa pelaaja ukkelin.
            transform.localScale = theScale;
        }
    }







}