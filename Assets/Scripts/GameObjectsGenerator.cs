using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private Range _spawnRange;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Pool _pool;
    [SerializeField] private int _distanceBetweenSpawn;
    [SerializeField] private SpawnPositionsBlackList _positionsBlackList;

    private void Start ()
    {
        _pool.Initialize();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn ()
    {
        var waitForSeconds = new WaitForSeconds(_secondsBetweenSpawn);
        GameObject spawned = new GameObject();

        while (true)
        {
            if (_pool.TryGetObject(out spawned))
            {
                Vector3 spawnPosition = GetSpawnPosition();

                if (_positionsBlackList.CheckIsFreePosition(spawnPosition, _distanceBetweenSpawn))
                {
                    _positionsBlackList.AddPositionToBlackList(spawnPosition);

                    spawned.transform.position = spawnPosition;
                    spawned.SetActive(true);
                }
                else
                {
                    Debug.Log("Position is busy");
                }
            }

            yield return waitForSeconds;
        }
    }

    private Vector3 GetSpawnPosition ()
    {
        int spawnRange = Random.Range(_spawnRange.Min, _spawnRange.Max);

        Vector3 spawnPosition = new Vector3(Convert.ToInt32(transform.position.x) + spawnRange, transform.position.y);

        return spawnPosition;
    }
}

[System.Serializable]
public class Range
{
    public int Min;
    public int Max;
}
