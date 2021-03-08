using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private int _additionalBanPoints;

    private List<Transform> _objects = new List<Transform>();

    public void Add (Transform @object)
    {
        _objects.Add(@object);
    }

    public bool IsFree (Vector3 position)
    {
        int xPosition = Convert.ToInt32(position.x);

        foreach (var bannedTransform in _objects)
        {
            for (int i = 0; i < _additionalBanPoints; i++)
            {
                if (xPosition == bannedTransform.position.x + i || xPosition == bannedTransform.position.x - i)
                    return false;
            }
        }

        return true;
    }
}
