using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _CampFire : MonoBehaviour
{
	public GameObject rock;
	public GameObject logPile;

	private void Start()
	{
		Generate();
	}


	public void Generate()
	{
		int numRocks	= Random.Range(30, 45);
		Vector3 center	= transform.position;
		float radius	= Random.Range(0.85f, 1.0f);

		Quaternion rotation;
		Vector3 scale;
		Vector3 pos;

		for (int i = 0; i < numRocks; i++)
		{
			float angle		= 360 / numRocks * i;
			pos				= GetPosition(center, radius, angle);
			GameObject r	= Instantiate(rock, pos, Quaternion.identity);

			scale		= r.transform.localScale;
			rotation	= r.transform.rotation;

			scale *= Random.Range(1.0f, 1.5f);

			rotation.z = Random.Range(0, 270.0f);

			r.transform.localScale	= scale;
			r.transform.rotation	= rotation;
			r.transform.parent		= transform;
			
		}

		GameObject l = Instantiate(logPile, center, Quaternion.identity);

		rotation	= l.transform.rotation;
		pos			= l.transform.position;

		pos.y		= transform.position.y + (transform.localScale.y / 2) + (rock.transform.localScale.y / 2) + Random.Range(0.25f, 0.3f);
		rotation.y	= Random.Range(0.0f, 270.0f);

		l.transform.parent		= transform;
		l.transform.position	= pos;
		l.transform.rotation	= rotation;
	}

	Vector3 GetPosition(Vector3 center, float radius, float angle)
	{
		Vector3 pos = new Vector3()
		{
			x = center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad),
			y = transform.position.y + (transform.localScale.y / 2) + (rock.transform.localScale.y / 2) + 0.025f,
			z = center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad)
		};

		return pos;
	}
}
