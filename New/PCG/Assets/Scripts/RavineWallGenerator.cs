using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavineWallGenerator : MonoBehaviour
{
	public GameObject rock;

	public GameObject corners;

	[Space]

	public int rocksPerSide;

	[Space]

	public int minY;
	public int maxY;

	private void Start()
	{
		for (int i = 0; i < corners.transform.childCount; i++)
		{
			Vector3 start = corners.transform.GetChild(i).position;
			Vector3 end = corners.transform.GetChild(i + 1 == corners.transform.childCount ? 0 : i + 1).position;

			for (int j = 0; j < rocksPerSide; j++)
			{
				Vector3 pos = Vector3.Lerp(start, end, (1.0f / rocksPerSide) * (j + 1));

				pos.y = Random.Range(minY, maxY);

				GameObject rockObj = Instantiate(rock);

				rockObj.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 270.0f), 0);
				rockObj.transform.position		= pos;
				rockObj.transform.parent		= transform;
			}
		}
	}
}
