using LibNoise.Generator;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	private Perlin perlin;

	[Header("Perlin Noise")]

	public double frequency;
	public double lacunarity;

	[Range(0.0f, 1.0f)] public double persistance;

	public int octaves;

	[Header("World Attributes")]

	[Range(16, 250)] public int worldWidth;
	[Range(16, 250)] public int worldHeight;

	public float meshHeightMultiplier;

	[Space]

	public AnimationCurve meshHeightCurve;

	[Space]

	public TerrainType[] regions;

	private void Start()
	{
		GenerateTerrain();
	}

	public void GenerateTerrain()
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(worldWidth, worldHeight, frequency, lacunarity, persistance, octaves, Random.Range(0, 9999));

		Color[] colorMap = new Color[worldHeight * worldWidth];

		// Get colours
		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				float height = noiseMap[x, y];

				foreach (TerrainType r in regions)
				{
					if (height <= r.height)
					{
						colorMap[x + (y * worldWidth)] = r.color;

						break;
					}
				}
			}
		}

		TerrainDisplay display = FindObjectOfType<TerrainDisplay>();

		//display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));

		//display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, worldSize, worldSize));

		display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(colorMap, worldWidth, worldHeight));
	}
}

[System.Serializable]
public struct TerrainType
{
	public string name;
	public float height;
	public Color color;
}