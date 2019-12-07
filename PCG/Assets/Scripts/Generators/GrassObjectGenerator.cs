using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassObjectGenerator : MonoBehaviour
{
	public GameObject[] lowerGrassObjects;
	public GameObject[] higherGrassObjects;

	[Space]

	public GameObject lamp;

	[Header("Settlement Gameobjects")]
	public GameObject[] settlementHouses;

	public void Generate(int worldWidth, int worldHeight, TerrainType[] regionArray, Vector3[] vertices, int borderSize, GameObject parent)
	{
		int numSettlements = 0;

		const int MAX_SETTLEMENTS = 10;
		const int MIN_SETTLEMENT_SIZE = 100;

		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				bool isBorder		= x < borderSize || x > worldWidth - borderSize || y < borderSize || y > worldHeight - borderSize;
				int index			= x + (y * worldWidth);
				TerrainType region	= regionArray[index];
				Vector3 v			= vertices[index];

				float rand = Random.value;

				v.x += 0.5f;
				v.z -= 0.5f;

				Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360.0f), 0.0f);

				if (isBorder)
					continue;

				if (region.name == "Grass 0")
				{
					GameObject _object = null;

					if (rand <= 0.0025f)
						_object = Instantiate(lamp, v, rot, parent.transform);

					else if (rand <= 0.25f)
						_object = Instantiate(lowerGrassObjects[Random.Range(0, lowerGrassObjects.Length)], v, rot, parent.transform);
				}

				else if (region.name == "Grass 1")
				{
					GameObject _object = null;

					if (rand <= 0.01f && numSettlements < MAX_SETTLEMENTS)
					{
						_object = Instantiate(higherGrassObjects[Random.Range(0, higherGrassObjects.Length)], v, Quaternion.identity, parent.transform);

						if (_object.CompareTag("Campfire"))
						{
							Settlement settlement = _object.GetComponent<Settlement>();

							int size = settlement.GeneratePositions(index);

							if (size < MIN_SETTLEMENT_SIZE)
								Destroy(_object);

							else
							{
								numSettlements++;

								settlement.GenerateObjects(settlementHouses);
							}
						}
					}
				}
			}
		}
	}
}