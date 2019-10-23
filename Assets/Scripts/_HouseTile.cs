using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class _HouseTile : MonoBehaviour
{
	static int houseObjIndex = 0;

	public GameObject[] houseOBJs;
	public void Generate(Vector3 lookAtPos)
	{
		GameObject parent = GameObject.FindGameObjectWithTag("DioramaParent");

		GameObject houseSlot = transform.GetChild(0).gameObject;

		GameObject houseToSpawn = houseOBJs[houseObjIndex++ % houseOBJs.Length];

		Vector3 pos = houseSlot.transform.position;

		pos.y = houseToSpawn.transform.localScale.y / 2 + (transform.localScale.y / 2);

		GameObject spawnedHouse = Instantiate(houseToSpawn, pos, houseToSpawn.transform.rotation);

		spawnedHouse.transform.parent = parent.transform;

		Vector3 dir = (lookAtPos - spawnedHouse.transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(dir);

		lookRotation.x = 0;
		lookRotation.z = 0;

		spawnedHouse.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Random.Range(0.75f, 1.25f));
	}
}
