using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MushroomTile : _DynamicObject
{
    public enum GrowthStatus {CHILD, PARENT};

    [Header("Status")]
    public GrowthStatus status = GrowthStatus.CHILD;

    [Header("Objects")]
    public GameObject mushroom;
    private List<GameObject> mushroomSlots;

    [Header("Attributes")]
    private float growTime = 10.0f;

    private void GetMushroomSlots()
    {
        mushroomSlots = new List<GameObject>();

        for (int i = 0; i < transform.childCount; i++)
        {
            mushroomSlots[i] = transform.GetChild(i).gameObject;
        }
    }

    private void FixedUpdate()
    {

    }

    public override void Generate(int worldSize, int tileSize, ref GameObject[] tiles)
    {
        GetMushroomSlots();
    }
}