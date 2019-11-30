using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScript : MonoBehaviour
{
	public Vector3 offset;

	void Start()
    {
		Invoke("DoIt", 0.1f);
    }

	private void DoIt()
	{
		transform.position += offset;
	}
}
