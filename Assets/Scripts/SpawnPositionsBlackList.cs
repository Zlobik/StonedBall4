using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositionsBlackList : MonoBehaviour
{
    private List<Vector3> _positionsBlackList = new List<Vector3>();

    public void AddPositionToBlackList (Vector3 position)
    {
        _positionsBlackList.Add(position);
    }

    public bool CheckIsFreePosition (Vector3 spawnPosition, int distanceBetweenSpawn)
    {
        bool canSpawn = true;

        foreach (var bannedPosition in _positionsBlackList)
        {
            for (int i = 0; i < distanceBetweenSpawn; i++)
            {
                if (spawnPosition.x == bannedPosition.x + i || spawnPosition.x == bannedPosition.x - i)
                    canSpawn = false;
            }
        }

        return canSpawn;
    }
}
