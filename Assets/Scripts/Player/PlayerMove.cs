using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	private Rigidbody2D rigidbody;

	[SerializeField] private BoxCollider2D collider;
	[SerializeField] private LayerMask jumpableGround;

	private void Awake()
	{
		rigidbody = GetComponent<Rigidbody2D>();
	}

	public void Move(Vector3 dir, float speed)
	{
		rigidbody.AddForce(dir * speed, ForceMode2D.Impulse);
		if(Mathf.Abs(rigidbody.velocity.x) > speed)
		{
			rigidbody.velocity = (dir * speed) + (Vector3.up * rigidbody.velocity.y);
		}
	}

	public void Jump(float jumpPower)
	{
		rigidbody.velocity = new Vector3(rigidbody.velocity.x, jumpPower, 0);
	}

	public bool IsGrounded()
	{
		return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround);
	}
}
