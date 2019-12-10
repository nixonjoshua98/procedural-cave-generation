using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UI : MonoBehaviour
{
	private void OnGUI()
	{
		if (GUILayout.Button("Generate"))
		{
			SceneManager.LoadScene(0);
		}

		else if (GUILayout.Button("Free Camera"))
		{
			Camera.main.GetComponent<FreeCam>().enabled = true;
			Camera.main.GetComponent<FixedCam>().enabled = false;
		}

		else if (GUILayout.Button("Fixed Camera"))
		{
			Camera.main.GetComponent<FreeCam>().enabled = false;
			Camera.main.GetComponent<FixedCam>().enabled = true;
		}
	}
}
