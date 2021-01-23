using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private Transform _pullFather;
    [SerializeField] private int _pullCapacity;
    [SerializeField] private float _secondsBetweenSpawn = 1f;
    [SerializeField] private int _spawnRangeA = 1;
    [SerializeField] private int _spawnRangeB = 12;

    private float _elapsedTime = 0;
    private Pull _pull;

    private void Start ()
    {
        _pull = new Pull();
        _pull.SetContainer(_pullFather, _pullCapacity);
        _pull.Initialize(_template);
    }

    private Vector3 GetSpawnPointPosition ()
    {
        Vector3 spawnPoint = new Vector3(transform.position.x + UnityEngine.Random.Range(_spawnRangeA, _spawnRangeB), transform.position.y, transform.position.z);

        return spawnPoint;
    }

    private void Spawn ()
    {
        if (_pull.TryGetObject(out GameObject let))
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

public class Pull : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();
    private Transform _container;
    private int _containerCapacity;

    public void SetContainer (Transform container, int capacity)
    {
        _container = container;
        _containerCapacity = capacity;
    }

    public void Initialize (GameObject prefab)
    {
        for (int i = 0; i < _containerCapacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container);
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
