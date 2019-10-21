using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;


public class DioramaManager : MonoBehaviour
{
	public static DioramaManager inst = null;

	[HideInInspector] public int SEED;
	[HideInInspector] public int WORLD_SIZE;
	[HideInInspector] public int TOTAL_TILES;
	[HideInInspector] public int NUM_SETTLEMENTS;

    private int _TILE_SIZE = 5;

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
        SEED = 123;// Random.Range(0, 999999);

		Random.InitState(this.SEED);

		// Constants
		WORLD_SIZE		= Random.Range(9, 24);
		TOTAL_TILES		= this.WORLD_SIZE * WORLD_SIZE;
		NUM_SETTLEMENTS = Random.Range(1, (int)Mathf.Sqrt(WORLD_SIZE));
	}

	private void Start()
	{
		allTiles = new GameObject[TOTAL_TILES];

		tileGen.Generate();
	}

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
