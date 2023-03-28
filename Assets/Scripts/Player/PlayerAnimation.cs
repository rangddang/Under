using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void WalkAnimation()
    {
        anim.SetBool("isWalk", true);
    }

	public void IdleAnimation()
	{
		anim.SetBool("isWalk", false);
	}

    public void JumpAnimation()
    {
		anim.SetBool("isFall", false);
		anim.SetBool("isJump", true);
	}

	public void FallAnimation()
	{
		anim.SetBool("isFall", true);
	}

    public void OnGround()
    {
		anim.SetBool("isJump", false);
		anim.SetBool("isFall", false);
	}
}
