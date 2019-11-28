using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : ObjectBase
{
	int x;
	int y;

	public void Create(int x, int y)
	{
		this.x = x;
		this.y = y;

		gameObject.name = "Mushroom";

		Invoke("SpawnNewMushroom", Random.Range(5.0f, 10.0f));
	}

	private void SpawnNewMushroom()
	{
		const int scale = 32;

		Vector3[] vectorOffets = new Vector3[] { Vector3.left, Vector3.right, Vector3.back, Vector3.forward };

		foreach (Vector3 p in vectorOffets)
		{
			Vector3 pos = transform.position + (p * scale);
			TerrainType t = GetTerrainAtPosition(x + (int)p.x, y + (int)p.z);

			if (t.name == GetTerrainAtPosition(x, y).name)
			{
				if (!Physics.CheckBox(pos, new Vector3(0.5f, 0.5f, 0.5f)))
				{
					GameObject newMushroom = Instantiate(gameObject, transform.parent);

					newMushroom.transform.position = pos;
					newMushroom.transform.localScale = new Vector3(1.0f, scale, 1.0f);

					newMushroom.GetComponent<Mushroom>().Create(x + (int)p.x, y + (int)p.z);

					break;
				}
			}
		}
	}
}

