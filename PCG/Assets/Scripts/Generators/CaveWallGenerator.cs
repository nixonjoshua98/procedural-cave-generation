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

			Vector3 startV3 = new Vector3(start.x, 2.0f, start.y);
			Vector3 endV3	= new Vector3(end.x, 2.0f, end.y);

			const int TOTAL_STEPS = 75;

			for (int j = 0; j < TOTAL_STEPS; j++)
			{
				Vector3 pos = Vector3.Lerp(startV3, endV3, j / (float)TOTAL_STEPS);

				GameObject c = Instantiate(cube, terrainObject.transform);

				c.transform.position	= pos;
				c.transform.localScale	= new Vector3(5.0f, 100.0f, 1.0f);

				c.transform.LookAt(Vector3.zero);
			}

			++side;
		}
	}
}
