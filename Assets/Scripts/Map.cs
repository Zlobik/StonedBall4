using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private List<Transform> _objects = new List<Transform>();

    public void Add (Transform @object)
    {
        _objects.Add(@object);
    }

    public bool IsFree (ref Vector3 position)
    {
        position = new Vector3Int(Convert.ToInt32(position.x), Convert.ToInt32(position.y), 0);

        int xPosition = Convert.ToInt32(position.x);

        foreach (var bannedTransform in _objects)
        {
            if (xPosition == bannedTransform.position.x)
                return false;
        }

        return true;
    }
}
