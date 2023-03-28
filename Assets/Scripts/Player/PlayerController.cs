using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Device;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private PlayerMove playerMove;
    private PlayerAnimation anim;
    private PlayerHook playerHook;
    private CameraController camera;

    private float horizontal;
    private int jumpCount;
    private bool isjumping;

    [SerializeField] private int maxJumpCount = 1;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode hookKey = KeyCode.Mouse0;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
        anim = GetComponent<PlayerAnimation>();
        playerHook = GetComponent<PlayerHook>();
        camera = Camera.main.GetComponent<CameraController>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        InputKey();
        JumpDownAnimation();
        if (playerMove.IsGrounded() && !isjumping)
        {
            ResetJumpCount();
        }
    }

    private void InputKey()
    {
		if (Input.GetKeyDown(hookKey) && !playerHook.IsHook)
		{
            playerHook.OnHook();
		}
        if (Input.GetKeyDown(jumpKey) && !playerHook.IsHook && jumpCount < maxJumpCount)
        {
            Jump();
            if (playerMove.IsGrounded())
            {

            }
		}
        if (Input.GetKeyDown(KeyCode.F))
        {
			camera.ShakeCamera();
		}
	}


    private void JumpDownAnimation()
    {
		if (rigidbody.velocity.y < 0)
		{
			anim.FallAnimation();
		}
		if (rigidbody.velocity.y == 0)
		{
			anim.OnGround();
		}
	}

    

    private void Jump()
    {
        jumpCount++;
        StartCoroutine("Jumping");
		playerMove.Jump(jumpPower);
		anim.JumpAnimation();
	}

    public void ResetJumpCount()
    {
        jumpCount = 0;
    }

    private IEnumerator Jumping()
    {
		isjumping = true;
        yield return new WaitForSeconds(0.1f);
		isjumping = false;
	}

    private void FixedUpdate()
    {
        if (horizontal != 0 && !playerHook.IsHook)
        {
			playerMove.Move(Vector3.right * horizontal, speed);
			//playerMove.RopeMove(Vector3.right * horizontal, speed);
		}
        if(horizontal != 0)
        {
            anim.WalkAnimation();
        }
        else
        {
            anim.IdleAnimation();
        }
    }
}
