using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenerator : MonoBehaviour
{
	public GameObject beacon;
	public GameObject mushroom;
	public GameObject house;
	public GameObject submergedRock;

	private Color[] colors = new Color[] { Color.red, Color.green, Color.yellow, Color.blue };

	public void Generate(int worldWidth, int worldHeight, TerrainType[] terrainMap, Vector3[] vertices, int tileSize, int borderSize, GameObject parent)
	{
		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				bool isBorder		= x < borderSize || x > worldWidth - borderSize || y < borderSize || y > worldHeight - borderSize;
				int index			= x + (y * worldWidth);
				TerrainType region	= terrainMap[index];
				Vector3 v			= vertices[index];

				v.x = (v.x * tileSize) + (tileSize / 2);
				v.z = (v.z * tileSize) - (tileSize / 2);
				v.y += 16.0f;

				if (isBorder)
				{
					switch (region.name)
					{
						case "Grass 1":
							SpawnBeacon(v, parent.transform);
							break;
					}
				}

				else if (!isBorder)
				{
					switch (region.name)
					{

						case "Dirt 0":
							SpawnMushroom(v, x, y, parent.transform);
							break;

						case "Grass 1":
							SpawnHouse(v, parent.transform);
							break;

						case "Water":
							SpawnRockInWater(v, parent.transform);
							break;
					}
				}
			}
		}
	}

	private void SpawnBeacon(Vector3 pos, Transform parent)
	{
		float rand = Random.value;

		if (rand <= 0.005f)
		{
			GameObject _beacon = Instantiate(beacon, parent);

			_beacon.transform.position		= pos;
			_beacon.transform.localScale	= new Vector3(1.0f, 32.0f, 1.0f);
		}
	}

	private void SpawnMushroom(Vector3 pos, int x, int y, Transform parent)
	{
		float rand = Random.value;

		if (rand <= 0.01f)
		{
			GameObject _mushroom = Instantiate(mushroom, parent);

			_mushroom.transform.position = pos;
			_mushroom.transform.localScale = new Vector3(1.0f, 32.0f, 1.0f);

			GameObject head = _mushroom.transform.GetChild(1).gameObject;

			head.GetComponent<MeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];

			_mushroom.GetComponent<Mushroom>().Create(x, y);
		}

	}

	private void SpawnHouse(Vector3 pos, Transform parent)
	{
		float rand = Random.value;

		if (rand <= 0.02f)
		{
			GameObject _house = Instantiate(house, parent);

			pos.y += 24.0f;

			_house.transform.position = pos;
			_house.transform.localScale = new Vector3(1.0f, 32.0f, 1.0f);
		}
	}

	private void SpawnRockInWater(Vector3 pos, Transform parent)
	{
		float rand = Random.value;

		if (rand <= 0.0025f)
		{
			GameObject _rock = Instantiate(submergedRock, parent);

			pos.y -= 75.0f;

			_rock.transform.position = pos;
			_rock.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360.0f), 0);
			_rock.transform.localScale = new Vector3(1.0f, 32.0f, 1.0f);
		}
	}
}