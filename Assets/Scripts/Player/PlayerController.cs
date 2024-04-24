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

    [SerializeField] private AnimationClip[] attackAnim;

    private float direction;
    private int saveGra;
    private int attackNum;
    private float attackTime;
    private float currentAttackLate;

    private bool isTryAttack;
    private bool isAttacking;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");

        if (currentAttackLate <= attackTime)
        {
            transform.position += Vector3.right * direction * moveSpeed * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.velocity = Vector3.up * jumpPower;
            }
            if (direction > 0)
            {
                sprite.flipX = true;
            }
            else if (direction < 0)
            {
                sprite.flipX = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StopCoroutine("TryAttack");
            StartCoroutine("TryAttack");
        }

        attackTime += Time.deltaTime;

        
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
        
        Attack();
        anim.SetInteger("Gravity", gravity);
    }

    public bool IsOnGrounded()
    {
        return true;
    }

    public void Attack()
    {
        if (isTryAttack)
        {
            if (currentAttackLate <= attackTime)
            {
                // Attack
                //switch (attackNum)
                //{
                //    case 0: Attack_1(); break;
                //    case 1: Attack_2(); break;
                //    case 2: Attack_3(); break;
                //    case 3: Attack_4(); break;
                //}
                anim.Play("Attack_" +  (attackNum + 1), -1, 0);
                StopCoroutine("OnAttack");
                StartCoroutine("OnAttack");
                currentAttackLate = attackAnim[attackNum].length;
                attackTime = 0;

                if (direction > 0)
                {
                    sprite.flipX = true;
                }
                else if (direction < 0)
                {
                    sprite.flipX = false;
                }

                attackNum++;
                if(attackNum >= 4) attackNum = 0;
            }
        }
    }

    //public void Attack_1()
    //{

    //}
    //public void Attack_2()
    //{

    //}
    //public void Attack_3()
    //{

    //}
    //public void Attack_4()
    //{

    //}

    private IEnumerator TryAttack()
    {
        isTryAttack = true;
        yield return new WaitForSeconds(0.2f);
        isTryAttack = false;
    }

    private IEnumerator OnAttack()
    {
        //anim.SetBool("IsAttacking", true);
        yield return new WaitForSeconds(0.7f);
        //anim.SetBool("IsAttacking", false);
        attackNum = 0;
    }
}
