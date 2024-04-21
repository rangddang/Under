using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sprite;

    public float moveSpeed;
    public float jumpPower;

    private float direction;
    private int saveGra;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rigid.velocity = Vector3.up * jumpPower;
        }

        sprite.flipX = direction > 0;
        anim.SetBool("IsRunning", direction != 0);
        int gravity = 0;
        if(rigid.velocity.y > 0)
        {
            gravity = 1;
        }
        else if(rigid.velocity.y < 0)
        {
            gravity = -1;
        }
        if(saveGra!= 0 && gravity == 0)
        {
            anim.Play("Player_Onground", -1, 0);
        }
        saveGra = gravity;
        anim.SetInteger("Gravity", gravity);
    }

    public bool IsOnGrounded()
    {
        return true;
    }
}
