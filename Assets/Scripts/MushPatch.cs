using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushPatch : MonoBehaviour
{
	private List<GameObject> mushroomSlots = new List<GameObject>();

	[Header("Gameobjects")]
	public GameObject mushroom;

	private float spawnDelay;
	private float timer;

	private void Start()
	{
		spawnDelay = Random.Range(4.0f, 12.0f);
	}

	public void Generate()
	{
		GetMushroomSlots();
	}

	void GetMushroomSlots()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject child = transform.GetChild(i).gameObject;

			for (int j = 0; j < child.transform.childCount; j++)
			{
				mushroomSlots.Add(child.transform.GetChild(j).gameObject);
			}
		}
	}

	private void FixedUpdate()
	{
		timer += Time.fixedDeltaTime;

		if (timer >= spawnDelay)
		{
			timer = 0.0f;

			SpawnMushroom();
		}
	}

	void SpawnMushroom()
	{
		if (mushroomSlots.Count == 0) return;

		int rand		= Random.Range(0, mushroomSlots.Count);
		GameObject slot = mushroomSlots[rand];

		GameObject spawnedMush = Instantiate(mushroom, slot.transform.position, Quaternion.identity);

		spawnedMush.transform.parent = slot.transform.parent;

		mushroomSlots.RemoveAt(rand);

		Destroy(slot);
	}
}
