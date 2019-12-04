using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PositionIndex 
{
	public int index;
	public Vector3 pos;

	public PositionIndex(Vector3 _pos, int _index)
	{
		pos = _pos;
		index = _index;
	}
}
