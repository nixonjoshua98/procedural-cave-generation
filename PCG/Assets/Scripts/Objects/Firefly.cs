using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
	private Vector3 start;
	public Vector3 target;

	private float rotateSpeed	= 0.25f;
	private float moveSpeed		= 2.0f;

	private void Start()
	{
		SetDestination();

		transform.position = target;

		SetDestination();
	}

	private void SetDestination()
	{
		float x = Random.Range(-100, 100);
		float y = Random.Range(10.0f, 30.0f);
		float z = Random.Range(-100, 100);

		target	= new Vector3(x, y, z);
		start	= transform.position;
	}

	private void FixedUpdate()
	{
		float step = Mathf.Clamp(rotateSpeed * Time.fixedDeltaTime, 0.0f, 1.0f);

		Vector3 targetDir	= target - transform.position;
		Vector3 newDir		= Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

		transform.rotation = Quaternion.LookRotation(newDir);
		transform.Translate(Vector3.forward * Time.fixedDeltaTime * moveSpeed);

		if (Vector3.Distance(transform.position, target) <= 1.0f)
			SetDestination();
	}
}
