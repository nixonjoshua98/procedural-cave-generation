using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	public static TerrainGenerator instance = null;

	[Header("Perlin Noise")]
	[Range(0.0f, 0.25f)] public double frequency;
	[Range(0.0f, 1.0f)] public double lacunarity;
	[Range(0.0f, 2.0f)] public double persistance;
	[Range(0, 25)]  public int octaves;
	[Header("Border Attributes")]
	[Range(1.0f, 64.0f)] public float borderMultiplier;
	[Range(0, 128)] public int borderSize;
	[Header("Terrain Mesh Attributes")]
	public float heightMultiplier;
	[Space]
	public AnimationCurve heightCurve;
	[Header("Gameobjects")]
	public GameObject ravineObject;
    [Space]
	public TerrainType[] regions;

	// Hidden from inspector
	private const int worldWidth = 250;
	private const int worldHeight = 250;
	[HideInInspector] public TerrainType[] terrainMap;

	private void Start()
	{
		GenerateTerrain();
	}

	public void GenerateTerrain()
	{
		instance = this;

		Random.InitState(69);

		float[,] noiseMap		= Noise.GenerateNoiseMap(worldWidth, worldHeight, frequency, lacunarity, persistance, octaves, Random.seed);
		terrainMap				= GenerateTerrainTypeMap(noiseMap);
		Color[] colorMap		= GenerateColorMap(terrainMap);
		Texture2D meshTexture	= TextureFromColorMap(colorMap, worldWidth, worldHeight);
		MeshData meshData		= MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, borderSize, borderMultiplier, heightCurve, regions[0].height);

		DrawMesh(meshData, meshTexture);

		ObjectGenerator decorationgen = GetComponent<ObjectGenerator>();

		decorationgen.Generate(worldWidth, worldHeight, terrainMap, meshData.vertices, borderSize, ravineObject);
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

    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height)
    {
        Texture2D texture = new Texture2D(width, height)
        {
            filterMode = FilterMode.Point,
            wrapMode = TextureWrapMode.Clamp
        };

        texture.SetPixels(colorMap);

        texture.Apply();

        return texture;
    }

    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
		ravineObject.GetComponent<MeshFilter>().sharedMesh						= meshData.CreateMesh();
		ravineObject.GetComponent<MeshRenderer>().sharedMaterial.mainTexture	= texture;
    }
}