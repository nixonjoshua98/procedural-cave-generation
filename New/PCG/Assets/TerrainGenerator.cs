using UnityEngine;

using System.Linq;

public class TerrainGenerator : MonoBehaviour
{
	[Header("Perlin Noise")]

	[Range(0.0f, 0.25f)] public double frequency;
	[Range(0.0f, 1.0f)] public double lacunarity;
	[Range(0.0f, 2.0f)] public double persistance;
	[Range(0, 10)]  public int octaves;

	[Header("Gameobjects")]
	public GameObject tileParent;

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
		float[,] noiseMap = Noise.GenerateNoiseMap(worldWidth, worldHeight, frequency, lacunarity, persistance, octaves, Random.Range(0, 9999));

		TerrainType[] terrainMap = GenerateTerrainTypeMap(noiseMap);

		// Generate color map
		Color[] colorMap = new Color[worldWidth * worldHeight];
		for (int i = 0; i < terrainMap.Length; i++)
			colorMap[i] = terrainMap[i].color;


		TerrainDisplay display = GetComponent<TerrainDisplay>();

		Texture2D t = TextureGenerator.TextureFromColorMap(colorMap, worldWidth, worldHeight);
		MeshData m = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, borderSize, borderHeightMultiplier, meshHeightCurve);

		foreach (Transform child in tileParent.transform)
			DestroyImmediate(child.gameObject);

		display.DrawMesh(m, t);

		Debug.Log("Mesh Vertices Count:" + m.vertices.Length);


		for (int y = 0; y < worldHeight; y++)
		{
			for (int x = 0; x < worldWidth; x++)
			{
				int index = x + (y * worldWidth);

				TerrainType terrain = terrainMap[index];

				Vector3 v = m.vertices[index];

				if (terrain.name == "Deep Water")
				{
					v.x = (v.x * tileSize) + (tileSize / 2);
					v.z = (v.z * tileSize) - (tileSize / 2);

					//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

					//cube.transform.position = v;
					//cube.transform.localScale = new Vector3(tileSize, 1.0f, tileSize);
					//cube.transform.parent = tileParent.transform;
				}
			}
		}
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
}

[System.Serializable]
public struct TerrainType
{
	public string name;
	public float height;
	public Color color;
}