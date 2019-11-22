using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	[Header("Perlin Noise")]
	[Range(0.0f, 0.25f)] public double frequency;
	[Range(0.0f, 1.0f)] public double lacunarity;
	[Range(0.0f, 2.0f)] public double persistance;
	[Range(0, 10)]  public int octaves;

	[Header("World Attributes")]
	[Range(8, 128)] public int tileSize;
	[Space]
	[Range(16, 250)] public int worldWidth;
	[Range(16, 250)] public int worldHeight;

	[Header("Border Attributes")]
	[Range(1.0f, 16.0f)] public float borderHeightMultiplier;
	[Range(16, 250)] public int borderSize;

	[Header("Mesh Attributes")]
	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;

	[Space]

	public TerrainType[] regions;

	private void Start()
	{
		GenerateTerrain();
	}

	public void GenerateTerrain()
	{
		float[,] noiseMap = Noise.GenerateNoiseMap(worldWidth, worldHeight, frequency, lacunarity, persistance, octaves, Random.Range(-99999, 99999));

		TerrainType[] terrainMap	= GenerateTerrainTypeMap(noiseMap);
		Color[] colorMap			= GenerateColorMap(terrainMap);

		TerrainDisplay display = GetComponent<TerrainDisplay>();

		Texture2D meshTexture = TextureGenerator.TextureFromColorMap(colorMap, worldWidth, worldHeight);
		MeshData meshData = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, borderSize, borderHeightMultiplier, meshHeightCurve);

		display.DrawMesh(meshData, meshTexture);
	}

	public TerrainType[] GenerateTerrainTypeMap(float[,] noiseMap)
	{
		TerrainType[] terrainMap = new TerrainType[worldHeight * worldWidth];

		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				float height = noiseMap[x, y];

				foreach (TerrainType r in regions)
				{
					if (height <= r.height)
					{
						terrainMap[x + (y * worldWidth)] = r;

						break;
					}
				}
			}
		}

		return terrainMap;
	}

	public Color[] GenerateColorMap(TerrainType[] terrainMap)
	{
		Color[] colorMap = new Color[worldWidth * worldHeight];

		for (int i = 0; i < terrainMap.Length; i++)
			colorMap[i] = terrainMap[i].color;

		return colorMap;
	}

	public void CreateCube(Vector3 pos, Color col)
	{
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

		pos.x *= tileSize;
		pos.z *= tileSize;
		pos.y += 1.0f;

		cube.transform.localScale	= new Vector3(tileSize, 1.0f, tileSize);
		cube.transform.position		= pos;

		cube.GetComponent<MeshRenderer>().material.color = col;
	}
}

[System.Serializable]
public struct TerrainType
{
	public string name;
	public float height;
	public Color color;
}