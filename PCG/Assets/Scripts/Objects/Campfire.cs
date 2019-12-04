using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : ObjectBase
{
	public GameObject plank;

	private int cellIndex;

	public void Create(int _cellIndex)
	{
		cellIndex = _cellIndex;

		GenerateCampfire();
		GenerateSettlement();
	}

	private void GenerateCampfire()
	{
		float radius	= 0.1f;
		int numPlanks	= Random.Range(3, 7);

		for (int i = 0; i < numPlanks; i++)
		{
			GameObject _plank = Instantiate(plank, Vector3.zero, Quaternion.identity, transform);

			int ang = (360 / numPlanks) * i;

			Vector3 pos = Vector3.zero;

			pos.x = radius * Mathf.Sin(ang * Mathf.Deg2Rad);
			pos.z = radius * Mathf.Cos(ang * Mathf.Deg2Rad);

			Quaternion rot = Quaternion.FromToRotation(Vector3.forward, Vector3.zero - pos);				

			_plank.transform.rotation		= rot;
			_plank.transform.localScale		= new Vector3(0.2f, 0.2f, 0.5f);
			_plank.transform.localPosition	= pos;
		}
	}

	private void GenerateSettlement()
	{
		//GetNeighboursRecursive(transform.position, cellIndex);
	}

	private List<PositionIndex> GetNeighboursRecursive(Vector3 center, int index)
	{
		List<PositionIndex> positionIndices = GetNeighbourPositions(center, index);

		int count = positionIndices.Count;

		for (int i = 0; i < count; i++)
		{
			List<PositionIndex> temp = GetNeighboursRecursive(positionIndices[i].pos, positionIndices[i].index);

			foreach (PositionIndex t in temp)
				positionIndices.Add(t);
		}

		return positionIndices;
	}

	private List<PositionIndex> GetNeighbourPositions(Vector3 center, int index)
	{
		Vector3[] vectorOffets	= new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
		int[] indexes			= new int[] { index - 250, index + 250, index - 1, index + 1 };

		List<PositionIndex> positions = new List<PositionIndex>();

		for (int i = 0; i < 4; i++)
		{
			Vector3 offset	= vectorOffets[i];
			Vector3 pos		= center + offset;
			TerrainType t	= GetTerrainAtPosition(cellIndex);

			if (indexes[i] < 0 || indexes[i] >= 62_500)
				continue;

			if (t.name == GetTerrainAtPosition(indexes[i]).name)
			{
				if (!Physics.CheckBox(pos, new Vector3(0.5f, 0.5f, 0.5f)))
				{
					GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);

					tile.name = "TEST";

					positions.Add(new PositionIndex(pos, indexes[i]));

					tile.transform.position		= pos;
					tile.transform.localScale	= new Vector3(1.0f, 0.1f, 1.0f);
				}
			}
		}

		return positions;
	}
}
