using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _DynamicTileGen : MonoBehaviour
{
    public GameObject[] dynamicTiles;

    private Quaternion[] rotations =
{
        Quaternion.Euler(0,     0,      0),
        Quaternion.Euler(0,     90,     0),
        Quaternion.Euler(0,     180,    0),
        Quaternion.Euler(0,     270,    0),
    };


    public void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
    {
        int dynamicTileIndex = 0;

        List<int> emptyTiles = GetEmptyTiles(ref tiles);

        int numDynamicTiles = (int)(emptyTiles.Count * 0.05f);

        for (int i = 0; i < numDynamicTiles; i++)
        {
            int rand = Random.Range(0, emptyTiles.Count);
            int tilesIndex = emptyTiles[rand];

            GameObject emptyTile = tiles[tilesIndex];
            GameObject dynamicTile = Instantiate(dynamicTiles[dynamicTileIndex], emptyTile.transform.position, Quaternion.identity);

            dynamicTile.transform.parent = emptyTile.transform.parent;
            dynamicTile.transform.rotation = rotations[Random.Range(0, rotations.Length)];

            Destroy(emptyTile);

            tiles[tilesIndex] = dynamicTile;

            emptyTiles.RemoveAt(rand);

            dynamicTileIndex = ++dynamicTileIndex % dynamicTiles.Length;

            dynamicTile.GetComponent<_DynamicObject>().Generate(worldSize, tileSize, ref tiles);
        }
    }

    private List<int> GetEmptyTiles(ref GameObject[] tiles)
    {
        List<int> emptyTiles = new List<int>();

        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i].CompareTag("EmptyTile"))
                emptyTiles.Add(i);
        }

        return emptyTiles;
    }
}
