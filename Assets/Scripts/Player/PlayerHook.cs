using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHook : MonoBehaviour
{
	private PlayerController player;
	private Rigidbody2D rigid;
	private Hook hook;

	private bool isHook;
	public bool IsHook => isHook;
	private Vector3 mousePos;
	private Vector3 targetPos;

	private Transform hookTrans;
	private Transform ropeTrans;
	[SerializeField] private float hookSpeed;
	[SerializeField] private float hookMoveSpeed = 20;
	[SerializeField] private float hookSize = 5f;

	private void Awake()
	{
		player = GetComponent<PlayerController>();
		rigid = GetComponent<Rigidbody2D>();
		hook = FindObjectOfType<Hook>();
		hookTrans = hook.gameObject.GetComponent<Transform>();
		ropeTrans = FindObjectOfType<Rope>().GetComponent<Transform>();
	}

	public void OnHook()
	{
		mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
		targetPos = (((mousePos - player.transform.position).normalized) * hookSize) + transform.position;
		hookTrans.position = transform.position;
		isHook = true;
		hookTrans.gameObject.SetActive(true);
		ropeTrans.gameObject.SetActive(true);

		StartCoroutine("MoveToTargetPos");
	}

	public void OffHook()
	{
		hook.RemoveTarget();
		isHook = false;
		hookTrans.gameObject.SetActive(false);
		ropeTrans.gameObject.SetActive(false);
	}

	public void StopHook()
	{
		StopCoroutine("MoveToTargetPos");
		OffHook();
	}

	private IEnumerator MoveToTargetPos()
	{
		bool hasEnemy = false;
		while (Vector3.Distance(hookTrans.position, targetPos) > 0.1f)
		{
			hookTrans.position = Vector3.MoveTowards(hookTrans.position, targetPos, Time.fixedDeltaTime * hookSpeed);
			if(hook.Target != null)
			{
				hookTrans.position = hook.Target.position;
				hasEnemy = true;
				StartCoroutine("MoveToHook");
				break;
			}
			yield return new WaitForFixedUpdate();
		}
		if(!hasEnemy)
			OffHook();
	}

	private IEnumerator MoveToHook()
	{
		rigid.gravityScale = 0;
		rigid.velocity = Vector3.zero;
		yield return new WaitForSeconds(0.1f);
		while (Vector3.Distance(transform.position, hookTrans.position) > 0.1f)
		{
			transform.position = Vector3.MoveTowards(transform.position, hookTrans.position, Time.fixedDeltaTime * hookMoveSpeed);
			yield return new WaitForFixedUpdate();
		}
		yield return new WaitForSeconds(0.1f);
		player.ResetJumpCount();
		rigid.gravityScale = 3;
		OffHook();
	}

}
