using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
	static Vector3[] startingVertices = {
			new Vector3 (-0.5f,     -0.5f,   -0.5f),
			new Vector3 (+0.5f,     -0.5f,   -0.5f),
			new Vector3 (+0.5f,     +0.5f,   -0.5f),
			new Vector3 (-0.5f,     +0.5f,   -0.5f),
			new Vector3 (-0.5f,     +0.5f,   +0.5f),
			new Vector3 (+0.5f,     +0.5f,   +0.5f),
			new Vector3 (+0.5f,     -0.5f,   +0.5f),
			new Vector3 (-0.5f,     -0.5f,   +0.5f),
		};

	static int[] startingTriangles = {
			0, 2, 1, 0, 3, 2,	// Front
			2, 3, 4, 2, 4, 5,	// Top
			1, 2, 5, 1, 5, 6,	// Right
			0, 7, 4, 0, 4, 3,	// Left
			5, 4, 7, 5, 7, 6,	// Back
			0, 6, 7, 0, 1, 6	// Bottom
		};

	public static void DrawCubes(Vector3[] cubePositions, int worldSize, Mesh mesh)
	{

		int[] triangles = new int[startingTriangles.Length * (worldSize * worldSize)];
		Vector3[] vertices = new Vector3[startingVertices.Length * (worldSize * worldSize)];

		mesh.Clear();

		for (int i = 0; i < cubePositions.Length; i++)
		{
			Vector3 pos = cubePositions[i];

			// Add vertices
			for (int j = 0; j < startingVertices.Length; j++)
			{
				int index = (i * startingVertices.Length) + j;
				Vector3 v = startingVertices[j] + pos;

				vertices[index] = v;
			}

			// Add triangles
			for (int j = 0; j < startingTriangles.Length; j++)
			{
				int index = (i * startingTriangles.Length) + j;
				int value = startingTriangles[j] + (i * startingVertices.Length);

				triangles[index] = value;
			}
		}

		mesh.vertices = vertices;

		mesh.triangles = triangles;

		mesh.Optimize();

		mesh.RecalculateNormals();
	}
}
