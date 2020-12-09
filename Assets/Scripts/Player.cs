using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isGrounded;
    private float timeAttack; //contator
    public float startTimeAttack; //tempo de animação.


    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //logica do player para movimento
        if(Input.GetKey(KeyCode.LeftArrow)){
            rigidbody.velocity = new Vector2(-Speed, rigidbody.velocity.y);

            //incluindo variaiveisanimator
            animator.SetBool("isWalking", true);

            //invertendo o rosto
            sprite.flipX = true;

        }else
        {
            //assim que solta as setas
            rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            //n tem tecla pressionada
            animator.SetBool("isWalking", false);

        }

        if(Input.GetKey(KeyCode.RightArrow)){
           rigidbody.velocity = new Vector2(Speed, rigidbody.velocity.y);

             animator.SetBool("isWalking", true);

            //invertendo o rosto
            sprite.flipX = false;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow)){
            rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);

        }

        if(timeAttack <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                animator.SetTrigger("isAtacking");//inicio da animação
                timeAttack = startTimeAttack; // o tempo é colocado no unity.
            }
        }
        else
        {
            timeAttack -= Time.deltaTime; //decrescer em tempo real
            animator.SetTrigger("isAtacking");//termino da animação.
        }
    }

    

    void OnCollisionEnter2D(Collision2D coll){
        //personagem tocando chão?
        if(coll.gameObject.layer == 8){
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}