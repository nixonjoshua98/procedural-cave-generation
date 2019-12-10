using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCam : MonoBehaviour
{
	private Vector3[] corners = new Vector3[]
	{
		new Vector3( 80,   25.0f,  80),
		new Vector3(-80,   25.0f,  80),
		new Vector3(  0,   25.0f,   0),
		new Vector3(-80,   25.0f, -80),
		new Vector3( 80,   25.0f, -80),
	};

	private Vector3 start;
	private Vector3 target;

	private int index;
	private float step;

	private float speed = 0.25f;

	private bool isLooking = false;

	private void Start()
	{
		start = corners[0];
		target = corners[1];

		transform.position = start;

		transform.LookAt(new Vector3(0.0f, 25.0f, 0.0f));
	}

	private void FixedUpdate()
	{
		step += Time.fixedDeltaTime * speed;

		if (transform.position == target)
		{
			step = 0.0f;

			start = target;

			index = index + 1 == corners.Length ? 0 : index + 1;

			target = corners[index];
		}

		transform.position = Vector3.Lerp(start, target, step);

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			Cursor.visible = false;
			isLooking = true;
			Cursor.lockState = CursorLockMode.Locked;
		}

		else if (Input.GetKeyUp(KeyCode.Mouse1))
		{
			Cursor.visible = true;
			isLooking = false;
			Cursor.lockState = CursorLockMode.None;
		}

		if (isLooking)
		{
			float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 2.5f;
			float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * 2.5f;

			transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);
		}
	}
}
