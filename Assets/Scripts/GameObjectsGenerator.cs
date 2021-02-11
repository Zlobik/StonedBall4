using System.Collections;
using UnityEngine;
using System.Linq;
using System;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenSpawn = 1f;
    [SerializeField] private Range _range;
    [SerializeField] private Pool _pool;

    private float _elapsedTime = 0;
    private bool _canObjectSpawn = false;

    private void Start ()
    {
        _pool.Initialize();
    }

    private Vector3 GetSpawnPointPosition ()
    {
        int randomDistance = UnityEngine.Random.Range(_range.Min, _range.Max);
        Vector3 spawnPoint = new Vector3(transform.position.x + randomDistance, transform.position.y);

        return spawnPoint;
    }

    private void Spawn (Transform spawnObject)
    {
        spawnObject.gameObject.SetActive(true);
    }

    private void CheckSpawnPlace ()
    {
        if (_pool.TryGetObject(out GameObject spawnObject))
        {
            spawnObject.transform.position = GetSpawnPointPosition();
            Vector2 raycastLenght = new Vector2(spawnObject.transform.position.x + 5, 0);

            RaycastHit2D hit = Physics2D.Raycast(spawnObject.transform.position, raycastLenght);
            Physics2D.Raycast(spawnObject.transform.position, raycastLenght);

            if (hit.collider == null || hit.collider.GetComponent<Textures>())
            {
                _canObjectSpawn = true;
            }
            if (_canObjectSpawn)
            {
                _canObjectSpawn = false;
                Spawn(spawnObject.transform);
            }
        }
    }

    private void Update ()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            _elapsedTime = 0;
            CheckSpawnPlace();
        }
    }
}

[System.Serializable]
public class Range
{
    public int Min;
    public int Max;
}
