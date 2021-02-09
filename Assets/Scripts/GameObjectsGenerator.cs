using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameObjectsGenerator : Pool
{
    [SerializeField] private float _secondsBetweenSpawn = 1f;
    [SerializeField] private Range _range;

    private float _elapsedTime = 0;

    private void Start ()
    {
        Initialize();
    }

    private Vector3 GetSpawnPointPosition ()
    {
        int randomDistance = UnityEngine.Random.Range(_range.Min, _range.Max);
        Vector3 spawnPoint = new Vector3(transform.position.x + randomDistance, transform.position.y);

        return spawnPoint;
    }

    private void Spawn ()
    {
        if (TryGetObject(out GameObject let))
        {
            let.transform.position = GetSpawnPointPosition();
            let.SetActive(true);
        }
    }

    private void Update ()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            _elapsedTime = 0;

            Spawn();
        }
    }
}

public class Pool : MonoBehaviour
{
    [SerializeField] private Transform _poolParent;
    [SerializeField] private GameObject _template;
    [SerializeField] private int _pullCapacity;

    private List<GameObject> _pool = new List<GameObject>();

    public void Initialize ()
    {
        for (int i = 0; i < _pullCapacity; i++)
        {
            GameObject spawned = Instantiate(_template, _poolParent);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    public bool TryGetObject (out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    public void ResetPool ()
    {
        foreach (var item in _pool)
            item.SetActive(false);
    }
}

[System.Serializable]
public class Range
{
    public int Min;
    public int Max;
}
