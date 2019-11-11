using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MushroomTile : _DynamicObject
{
    [Header("Objects")]
    public GameObject mushroomOBJ;

    private List<GameObject> mushroomSlots	= new List<GameObject>();

	[Header("Attributes")]
	public float yOffset;
	public float spawnMushroomInterval;

	private float spawnTimer = 0.0f;

    private void GetMushroomSlots()
    {
        for (int i = 0; i < transform.childCount; i++)
            mushroomSlots.Add(transform.GetChild(i).gameObject);
    }

    private void FixedUpdate()
    {
		spawnTimer += Time.fixedDeltaTime;

		if (spawnTimer >= spawnMushroomInterval)
		{
			spawnTimer = 0.0f;

			if (mushroomSlots.Count > 0)
				SpawnMushroom();
		}
    }

	private void SpawnMushroom()
	{
		int rand			= Random.Range(0, mushroomSlots.Count);
		GameObject slot		= mushroomSlots[rand];
		GameObject mushroom = Instantiate(mushroomOBJ, slot.transform.position, Quaternion.identity);
		Vector3 mushroomPos = mushroom.transform.position;
		mushroomPos.y		= yOffset + transform.position.y + (transform.localScale.y / 2) + (mushroom.transform.localScale.y / 2);

		mushroom.transform.position = mushroomPos;
		mushroom.transform.parent	= transform;

		mushroomSlots.RemoveAt(rand);
		Destroy(slot);
	}

    public override void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
    {
        GetMushroomSlots();

		SpawnMushroom();
	}
}