using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWallGenerator : MonoBehaviour
{
	public GameObject cube;

	public GameObject terrainObject;

	private Vector2[] corners = new Vector2[]
	{
		new Vector2( 125,	 125),
		new Vector2(-125,	 125),
		new Vector2(-125,	-125),
		new Vector2( 125,	-125),
	};

	private void Start()
	{
		int side = 0;

		for (int i = 0; i < corners.Length; i++)
		{
			Vector2 start = corners[i];
			Vector2 end = corners[i + 1 == corners.Length ? 0 : i + 1];

			Vector3 startV3 = new Vector3(start.x, 25.0f, start.y);
			Vector3 endV3	= new Vector3(end.x, 25.0f, end.y);

			const int TOTAL_STEPS = 125;

			for (int j = 0; j < TOTAL_STEPS; j++)
			{
				Vector3 pos = Vector3.Lerp(startV3, endV3, j / (float)TOTAL_STEPS);

				GameObject c = Instantiate(cube, terrainObject.transform);

				c.transform.LookAt(Vector3.zero);

				c.transform.position = pos;

				c.transform.localScale = new Vector3(c.transform.localScale.x, Random.Range(0, 2) == 0 ? 0.125f : -0.125f, c.transform.localScale.z);

				c.transform.rotation = Quaternion.Euler(0, Random.Range(-45.0f, 45.0f) + c.transform.rotation.y, 0.0f);
			}

			++side;
		}
	}
}
