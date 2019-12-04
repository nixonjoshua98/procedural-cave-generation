using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBase : MonoBehaviour
{
	public TerrainType GetTerrainAtPosition(int index)
	{
		return TerrainGenerator.instance.terrainMap[index];
	}
}
