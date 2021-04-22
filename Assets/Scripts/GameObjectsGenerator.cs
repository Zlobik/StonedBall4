using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private Range _spawnRange;
    [SerializeField] private float _secondsBetweenSpawn;
    [SerializeField] private Pool _pool;
    [SerializeField] private Map _map;

    private void Start ()
    {
        _pool.Initialize();
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn ()
    {
        var timeBeforeRespawn = new WaitForSeconds(_secondsBetweenSpawn);

        while (true)
        {
            if (_pool.TryGetObject(out GameObject spawned))
            {
                var spawnPosition = transform.position + new Vector3(_spawnRange.GetRandom(), transform.position.y, 0);

                if (_map.IsFree(ref spawnPosition))
                {
                    spawned.transform.position = spawnPosition;
                    _map.Add(spawned.transform);
                    spawned.SetActive(true);
                }
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

    public int GetRandom ()
    {
        int spawnRange = Random.Range(Min, Max);

        return spawnRange;
    }
}
