using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
	public TerrainType GetTerrainAtPosition(int x, int y)
	{
		int index = (int)(x + (y * 250));

		return TerrainGenerator.instance.terrainMap[index];
	}
}
