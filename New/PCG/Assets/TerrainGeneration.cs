using LibNoise.Generator;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
	private Perlin perlin;

	[Header("Perlin Noise")]

	public double frequency;
	public double lacunarity;

	[Range(0.0f, 1.0f)] public double persistance;

	public int octaves;

	[Header("World Attributes")]

	[Range(16, 85)] public int worldSize;

	[Space]

	public TerrainType[] regions;


	// - Privates
	Vector3[] cubePositions;

	private void Start()
	{
		cubePositions = new Vector3[worldSize * worldSize];
	}

	private void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 150, 100), "Generate"))
		{
			GenerateTilePositions();

			MeshGenerator.DrawCubes(cubePositions, worldSize, GetComponent<MeshFilter>().mesh);
		}
	}

	private void GenerateTilePositions()
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(worldSize, 1.0f, frequency, lacunarity, persistance, octaves);

		float topLeftX = (worldSize - 1) / -2f;
		float topLeftZ = (worldSize - 1) / 2f;

		for (int _y = 0; _y < worldSize; _y++)
		{
			for (int _x = 0; _x < worldSize; _x++)
			{
				Vector3 pos = new Vector3
				{
					x = topLeftX + _x,
					y = (float)noiseMap[_x, _y],
					z = topLeftZ - _y
				};

				cubePositions[_x + (_y * worldSize)] = pos;
			}
		}
	}
}


[System.Serializable]
public struct TerrainType
{
	public string name;

	public float height;

	public Material mat;
}
