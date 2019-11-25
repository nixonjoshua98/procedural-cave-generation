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
	[Range(1.0f, 64.0f)] public float borderHeightMultiplier;
	[Range(0, 128)] public int borderSize;

	[Header("Mesh Attributes")]
	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;

    [Header("Components")]
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

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

		Texture2D meshTexture   = TextureFromColorMap(colorMap, worldWidth, worldHeight);
		MeshData meshData       = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, borderSize, borderHeightMultiplier, meshHeightCurve);

		DrawMesh(meshData, meshTexture);

		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				int index = x + (y * worldWidth);

				TerrainType terrain = terrainMap[index];

				Vector3 v = meshData.vertices[index];

				if (terrain.height >= 3.0f)
				{
					v.x = (v.x * tileSize) + (tileSize / 2);
					v.z = (v.z * tileSize) - (tileSize / 2);
					v.y += 1.0f;

					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

					cube.transform.position = v;
					cube.transform.localScale = new Vector3(tileSize, 1.0f, tileSize);
					cube.transform.parent = meshRenderer.gameObject.transform;
				}
			}
		}

	SettlementGenerator settlementGenerator = GetComponent<SettlementGenerator>();

      settlementGenerator.Generate();

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
        meshFilter.sharedMesh = meshData.CreateMesh();

        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}

[System.Serializable]
public struct TerrainType
{
	public string name;
	public float height;
	public Color color;
}