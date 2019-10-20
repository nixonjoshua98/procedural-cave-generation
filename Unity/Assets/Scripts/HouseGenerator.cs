using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{

	static int houseObjIndex = 0;

	[Header("Objects")]
	public GameObject[] houseObjects;

	public void Generate(Vector3 centerPoint)
	{
		GameObject houseSlot	= transform.GetChild(0).gameObject;
		GameObject houseToSpawn = houseObjects[houseObjIndex++ % houseObjects.Length];

		Vector3 pos = houseSlot.transform.position;

		pos.y = houseToSpawn.transform.localScale.y / 2 + (transform.localScale.y / 2);

		GameObject spawnedHouse = Instantiate(houseToSpawn, pos, houseToSpawn.transform.rotation);

		spawnedHouse.transform.parent = transform;

		Vector3 dir = (centerPoint - spawnedHouse.transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(dir);

		lookRotation.x = 0;
		lookRotation.z = 0;

		spawnedHouse.transform.rotation = Quaternion.Slerp( transform.rotation, lookRotation, Random.Range(0.75f, 1.25f) );

		Destroy(houseSlot);
	}
}
