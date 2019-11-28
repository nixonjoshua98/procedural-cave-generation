using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LibNoise;
using LibNoise.Generator;

public static class Noise
{
	public static float[,] GenerateNoiseMap(int worldWidth, int worldHeight, double frequency, double lacunarity, double persistance, int octaves, int seed)
	{
		Perlin perlin = new Perlin(frequency, lacunarity, persistance, octaves, seed, QualityMode.High);

		//var noise = new Noise2D(worldWidth, worldHeight, perlin);

		//var data = noise.GetData();

		//return data;

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
