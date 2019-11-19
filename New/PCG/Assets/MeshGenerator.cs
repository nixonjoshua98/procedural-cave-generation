﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
	public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
	{
		int width = heightMap.GetLength(0);
		int height = heightMap.GetLength(1);

		float topLeftX = (width - 1) / -2.0f;
		float topLeftZ = (height - 1) / 2.0f;

		MeshData meshData = new MeshData(width, height);

		int vertexIndex = 0;

		float maxDistanceFromCenter = Vector2.Distance(new Vector2(width, height), Vector2.zero);

		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				float distanceFromCenterPercent = Vector2.Distance(new Vector2(x, y), Vector2.zero);

				float heightValue = heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;

				meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightValue, topLeftZ - y);

				meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

				if (x < width - 1 && y < height - 1)
				{
					meshData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
					meshData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
				}

				++vertexIndex;
			}
		}

		return meshData;
	}
}

public class MeshData
{
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;

	private int triangleIndex = 0;

	public MeshData(int width, int height)
	{
		vertices	= new Vector3[width * height];
		uvs			= new Vector2[width * height];
		triangles	= new int[(width - 1) * (height - 1) * 6];
	}

	public void AddTriangle(int a, int b, int c)
	{
		triangles[triangleIndex] = a;
		triangles[triangleIndex + 1] = b;
		triangles[triangleIndex + 2] = c;

		triangleIndex += 3;
	}

	public Mesh CreateMesh()
	{
		Mesh mesh = new Mesh
		{
			vertices = vertices,
			triangles = triangles,
			uv = uvs
		};

		mesh.RecalculateNormals();

		mesh.Optimize();

		return mesh;
	}
}
