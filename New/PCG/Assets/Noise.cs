using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LibNoise;
using LibNoise.Generator;
using LibNoise.Operator;

public static class Noise
{
	public static float[,] GenerateNoiseMap(int worldSize, float scale, double frequency, double lacunarity, double persistance, int octaves)
	{
		Perlin perlin = new Perlin(frequency, lacunarity, persistance, octaves, Random.Range(0, worldSize * worldSize), QualityMode.High);

		float[,] map = new float[worldSize, worldSize];

		for (int y = 0; y < worldSize; y++)
		{
			for (int x = 0; x < worldSize; x++)
			{
				float _x = x / scale;
				float _y = y / scale;

				map[x, y] = (float) perlin.GetValue(_x, 0.0f, _y);
			}
		}

		return map;
	}
}
