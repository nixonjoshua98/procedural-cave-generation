using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : ObjectBase
{
	private static int houseIndex = 0;
	private static HashSet<int> allCells = new HashSet<int>();

	private int cellIndex;

	private HashSet<int> cells;
	private List<PositionIndex> neighbours;

	public int GeneratePositions(int _cellIndex)
	{
		cells = new HashSet<int>();

		if (!allCells.Contains(_cellIndex))
		{
			cellIndex = _cellIndex;

			GenerateSettlement();
		}

		return cells.Count();
	}

	public void GenerateObjects(GameObject[] houses)
	{
		for (int i = 0; i < 5;)
		{
			if (neighbours.Count == 0)
				break;

			int rand = Random.Range(0, neighbours.Count);

			PositionIndex posIndex = neighbours[rand];

			neighbours.RemoveAt(rand);

			Collider[] hits = Physics.OverlapSphere(posIndex.pos, 0.5f);

			if (hits.Length > 0)
			{
				continue;
			}

			if (i == 0)
				transform.position = posIndex.pos;

			else
			{
				Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);

				Instantiate(houses[houseIndex], posIndex.pos, rot, transform);
			}

			++i;
		}

		houseIndex = (houseIndex + 1) % houses.Length;
	}

	private void GenerateSettlement()
	{
		neighbours = GetNeighboursRecursive(transform.position, cellIndex).ToList();
	}

	private HashSet<PositionIndex> GetNeighboursRecursive(Vector3 center, int index)
	{
		HashSet<PositionIndex> positionIndices = GetNeighbourPositions(center, index);

		allCells.Add(index);
		cells.Add(index);

		for (int i = 0; i < positionIndices.Count; i++)
		{
			if (cells.Contains(positionIndices.ElementAt(i).index))
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
