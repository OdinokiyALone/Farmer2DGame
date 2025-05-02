using System;
using System.Collections;
using System.Security;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 5;
    public float dashSpeed = 20f;          
    public float GroundOffsetX = 5.53f;
    public float GroundOffsetY = 1.2f;
   

    private bool animEnd = true;
    private bool isDashing = false;
    private bool isDashAfterJump = false;
    private bool isGround = true;
    
    
     

    Animator _animation;
    Rigidbody2D body;
    SpriteRenderer _renderer;
    CapsuleCollider2D _capsuleCollider;

    public void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animation = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
       
    }

    public void Update()
    {
        RaycastHit2D hitRight = Physics2D.Raycast(new Vector2(transform.position.x + GroundOffsetX, transform.position.y - GroundOffsetY), Vector2.down, 0.2f);
        RaycastHit2D hitLeft = Physics2D.Raycast(new Vector2(transform.position.x + GroundOffsetX, transform.position.y - GroundOffsetY), Vector2.down, 0.2f);
        RaycastHit2D hitCenter = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - GroundOffsetY), Vector2.down, 0.2f);

        isGround = hitCenter.collider != null && hitCenter.transform.gameObject.CompareTag("Ground");
        if(!isGround)
        {
            if((hitRight.collider != null && hitRight.collider != _capsuleCollider ))
            {
                body.linearVelocity = new Vector2( -1 , body.linearVelocity.y);
            }
            else if((hitLeft.collider != null && hitLeft.collider != _capsuleCollider))
            {
                body.linearVelocity = new Vector2(1  , body.linearVelocity.y);
            }
        }

        if (Managers.Player.AllowMove)
        {
            Move(Input.GetAxis("Horizontal"), Input.GetButtonDown("Jump"), Input.GetButtonDown("Dash") , Input.GetButtonDown("Attack"));
        }
       
    }

    void Move(float horInput, bool isJumped, bool isDashed, bool isAttacked)
    {
        if(!isDashing && animEnd)
        {
            HorizontalMove(horInput);
            if(isJumped && isGround)
            {
                Jump();
            }
        }

        if(Managers.Player.AllowAttack && animEnd && isGround && isAttacked && body.linearVelocity.x == 0)
        {
            Attack();
        }

        if (isDashed && !isDashing && body.linearVelocity.x != 0 && !isDashAfterJump)
        {
           if(!isGround)
           {
               isDashAfterJump = true;
           } 
           Dash();
        }

        if(isDashAfterJump && isGround)
        {
            isDashAfterJump = false;
        }

        _animation.SetBool("isDash", isDashing);
        _animation.SetBool("isJump", body.linearVelocity.y != 0 && !isDashing); 
        _animation.SetBool("isGround", isGround && !isDashing);
        _animation.SetBool("isRun", body.linearVelocity.x != 0 && !isDashing);
    }
    void HorizontalMove(float horInput)
    {
            body.linearVelocity = new Vector2(horInput * speed, body.linearVelocity.y);
            if (horInput > 0)
                _renderer.flipX = false;
            else if (horInput < 0)
                _renderer.flipX = true;
    }
    void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    }
    void Attack()
    {
        animEnd = false;
        _animation.SetTrigger("attack");
    }
    void Dash()
    {
        isDashing = true;
        float dashDirection = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        body.linearVelocity = new Vector2(dashDirection * dashSpeed, 0);
        body.gravityScale = 0;
    }
    public void AnimationComplete()
    {
        Debug.Log("animation complete");
        animEnd = true;
    }
    void StartDashAttack()
    {
        isDashing = true;
        _animation.SetTrigger("dashAttack");
    }
    void EndDash()
    {
        Debug.Log("end dash");
        isDashing = false;
        AnimationComplete();
        body.linearVelocity = new Vector2(0, body.linearVelocity.y);
        body.gravityScale = 5; // ��������������� ����������
    }

   


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(new Vector2(transform.position.x - GroundOffsetX, transform.position.y), new Vector2(transform.position.x - GroundOffsetX, transform.position.y - GroundOffsetY));
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - GroundOffsetY));
        Gizmos.DrawLine(new Vector2(transform.position.x + GroundOffsetX, transform.position.y), new Vector2(transform.position.x + GroundOffsetX, transform.position.y - GroundOffsetY));

    }

     
}
