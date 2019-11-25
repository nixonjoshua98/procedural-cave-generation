using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public static class Noise
{
	public static float[,] GenerateNoiseMap(int worldWidth, int worldHeight, double frequency, double lacunarity, double persistance, int octaves, int seed)
    {
        using (Perlin perlin = new Perlin(frequency, lacunarity, persistance, octaves, seed, QualityMode.High))
        {
            float[,] map = new float[worldWidth, worldHeight];

            for (int y = 0; y < worldHeight; y++)
            {
                for (int x = 0; x < worldWidth; x++)
                {
                    float _x = x;
                    float _y = y;

                    map[x, y] = (float)perlin.GetValue(_x, 0.0f, _y);
                }
            }

            return map;
        }
    }
}
