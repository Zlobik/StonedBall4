using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private Range _spawnRange;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Pool _pool;
    [SerializeField] private Map _positionsBlackList;

    private bool _spawnerWork = true;

    private void Start ()
    {
        _pool.Initialize();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn ()
    {
        var timeBeforeRespawn = new WaitForSeconds(_secondsBetweenSpawn);

        while (_spawnerWork)
        {
            if (_pool.TryGetObject(out GameObject spawned))
            {
                Vector3Int spawnPosition = _spawnRange.GenerateSpawnPosition(transform.position);

                if (_positionsBlackList.IsFree(spawnPosition))
                {
                    spawned.transform.position = spawnPosition;
                    _positionsBlackList.Add(spawned.transform);
                    spawned.SetActive(true);
                }
            }
            else
            {
                _spawnerWork = false;
            }

            yield return timeBeforeRespawn;
        }
    }
}

[System.Serializable]
public class Range
{
    public int Min;
    public int Max;

    public Vector3Int GenerateSpawnPosition (Vector3 currentPosition)
    {
        int spawnRange = Random.Range(Min, Max);

        return new Vector3Int(Convert.ToInt32(currentPosition.x) + spawnRange, Convert.ToInt32(currentPosition.y), 0);
    }
}
