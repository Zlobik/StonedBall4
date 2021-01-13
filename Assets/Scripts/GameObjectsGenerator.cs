using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private float _secondsBetweenSpawn = 1f;
    [SerializeField] private int _spawnRangeA = 1;
    [SerializeField] private int _spawnRangeB = 12;

    [SerializeField] private GameObject _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();
    private float _elapsedTime = 0;

    private void Start ()
    {
        Initialize(_template);
    }

    private void SpawnNewObject ()
    {
        if (TryGetObject(out GameObject let))
        {
            Vector3 spawnPoint = new Vector3(transform.position.x + Random.Range(_spawnRangeA, _spawnRangeB), transform.position.y, transform.position.z);
            let.SetActive(true);
            let.transform.position = spawnPoint;
        }
    }

    private void Initialize (GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    private bool TryGetObject (out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    public void ResetPool ()
    {
        foreach (var item in _pool)
            item.SetActive(false);
    }

    private void Update ()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            _elapsedTime = 0;

            SpawnNewObject();
        }
    }
}
