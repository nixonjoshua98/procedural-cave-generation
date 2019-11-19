using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGenerator))]
public class TerrainGeneratorEditor : Editor
{
	public override void OnInspectorGUI()
	{
		TerrainGenerator gen = (TerrainGenerator) target;

		DrawDefaultInspector();

		if (GUILayout.Button("Generate"))
		{
			gen.GenerateTerrain();
		}
	}
}
