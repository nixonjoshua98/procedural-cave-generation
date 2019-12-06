using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : ObjectBase
{
	private static HashSet<int> allCellIndexesTaken = new HashSet<int>();

	private int cellIndex;

	private HashSet<int> previousCellIndexes;

	public int GeneratePositions(int _cellIndex)
	{
		previousCellIndexes = new HashSet<int>();

		if (!allCellIndexesTaken.Contains(_cellIndex))
		{
			cellIndex = _cellIndex;

			GenerateSettlement();
		}

		return previousCellIndexes.Count();
	}

	public void GenerateObjects()
	{

	}

	private void GenerateSettlement()
	{
		var neighbours = GetNeighboursRecursive(transform.position, cellIndex);

		foreach (PositionIndex p in neighbours)
		{
			GameObject tile = GameObject.CreatePrimitive(PrimitiveType.Cube);

			tile.name = "TEST";

			tile.transform.parent = transform;

			tile.transform.position = p.pos;

			tile.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
		}
	}

	private HashSet<PositionIndex> GetNeighboursRecursive(Vector3 center, int index)
	{
		HashSet<PositionIndex> positionIndices = GetNeighbourPositions(center, index);

		allCellIndexesTaken.Add(index);
		previousCellIndexes.Add(index);

		for (int i = 0; i < positionIndices.Count(); i++)
		{
			if (previousCellIndexes.Contains(positionIndices.ElementAt(i).index))
				continue;

			HashSet<PositionIndex> temp = GetNeighboursRecursive(positionIndices.ElementAt(i).pos, positionIndices.ElementAt(i).index);

			for (int j = 0; j < temp.Count(); j++)
				positionIndices.Add(temp.ElementAt(j));
		}

		return positionIndices;
	}

	private HashSet<PositionIndex> GetNeighbourPositions(Vector3 center, int index)
	{
		Vector3[] vectorOffets = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
		int[] indexes = new int[] { index - 250, index + 250, index - 1, index + 1 };

		HashSet<PositionIndex> positions = new HashSet<PositionIndex>();

		for (int i = 0; i < 4; i++)
		{
			Vector3 offset = vectorOffets[i];
			Vector3 pos = center + offset;
			TerrainType t = GetTerrainAtPosition(cellIndex);

			if (indexes[i] < 0 || indexes[i] >= 62_500)
				continue;

			if (t.name == GetTerrainAtPosition(indexes[i]).name)
			{
				if (!Physics.CheckBox(pos, new Vector3(0.5f, 0.5f, 0.5f)))
				{
					positions.Add(new PositionIndex(pos, indexes[i]));
				}
			}
		}

		return positions;

	}
}
