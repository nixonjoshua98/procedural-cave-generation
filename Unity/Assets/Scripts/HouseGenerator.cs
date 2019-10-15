using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{

	static int houseObjIndex = 0;

	[Header("House Objects")]
	public GameObject[] houseObjects;

	public void Start()
	{
		Generate();
	}

	public void Generate()
	{
		GameObject slot = transform.GetChild(0).gameObject;

		GameObject houseToSpawn = houseObjects[HouseGenerator.houseObjIndex++ % houseObjects.Length];

		Vector3 pos = slot.transform.position;

		pos.y = houseToSpawn.transform.localScale.y / 2 + (transform.localScale.y / 2);

		GameObject spawnedHouse = Instantiate(houseToSpawn, pos, houseToSpawn.transform.rotation); 

		spawnedHouse.transform.parent = transform;

		Destroy(slot);
	}
}
