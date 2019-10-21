using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

// minimum spannign tree
// gimp make tile tool


public class DioramaManager : MonoBehaviour
{
	public static DioramaManager inst = null;

	[HideInInspector] public int SEED;
	[HideInInspector] public int WORLD_SIZE;
	[HideInInspector] public int TOTAL_TILES;
	[HideInInspector] public int NUM_SETTLEMENTS;

    private const int _TILE_SIZE = 5;

    [HideInInspector] public int TILE_SIZE { get => _TILE_SIZE; }

	[HideInInspector] public GameObject[] allTiles;

	// Generators
	TileGenerator tileGen;

	private void Awake()
	{
		// Set the instance
		inst = this;

		// Grab components
		tileGen = GetComponent<TileGenerator>();

        // Seeding
        SEED = 13;// Random.Range(0, 999999);

		Random.InitState(this.SEED);

		// Constants
		WORLD_SIZE		= Random.Range(9, 24);
		TOTAL_TILES		= this.WORLD_SIZE * WORLD_SIZE;
        NUM_SETTLEMENTS = 1;// Random.Range((int)Mathf.Sqrt(WORLD_SIZE), (int)Mathf.Sqrt(WORLD_SIZE) + 2);
	}

	private void Start()
	{
		allTiles = new GameObject[TOTAL_TILES];

		tileGen.Generate();
	}

    private void FixedUpdate()
    {
        foreach (GameObject t in allTiles)
        {
            if (t.tag == "House")
                print("house");
        }
    }
}
