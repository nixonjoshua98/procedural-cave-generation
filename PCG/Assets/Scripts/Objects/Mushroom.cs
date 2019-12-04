using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : ObjectBase
{
	private int cellIndex;

	public void Create(int _cellIndex)
	{
		cellIndex = _cellIndex;

		gameObject.name = "Mushroom";

		InvokeRepeating("SpawnNewMushroom", Random.Range(5.0f, 6.0f), Random.Range(5.0f, 10.0f));
	}

	private void SpawnNewMushroom()
	{
        Vector3[] vectorOffets	= new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
		int[] indexes			= new int[] { cellIndex - 250, cellIndex + 250, cellIndex - 1, cellIndex + 1 };

		TerrainType t = GetTerrainAtPosition(cellIndex);

		for (int i = 0; i < 4; i++)
		{
			Vector3 offset	= vectorOffets[i];
			Vector3 pos		= transform.position + offset;

			if (indexes[i] < 0 || indexes[i] >= 62_500)
				continue;

			if (t.name == GetTerrainAtPosition(indexes[i]).name)
			{
				if (!Physics.CheckBox(pos, new Vector3(0.5f, 0.5f, 0.5f)))
				{
					GameObject newMushroom = Instantiate(gameObject, pos,  Quaternion.Euler(0, Random.Range(0, 360.0f), 0.0f), transform.parent);

					newMushroom.GetComponent<Mushroom>().Create(indexes[i]);

					break;
				}
			}
		}
    }
}

