using System.Collections;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private float _secondsBetweenSpawn = 1f;
    [SerializeField] private Range _range;
    [SerializeField] private Pool _pool;
    [SerializeField] private SpawnPointsBlackList _spawnPointBlackList;

    private float _elapsedTime = 0;

    public event UnityAction<Vector3> AddPositionToBlackList;
    public event UnityAction<Vector3> CheckSpawnPosition;

    private void Start ()
    {
        _pool.Initialize();
    }

    private void OnEnable ()
    {
        _spawnPointBlackList.AllowSpawn += Spawn;
    }

    private void OnDisable ()
    {
        _spawnPointBlackList.AllowSpawn -= Spawn;
    }

    private Vector3 GetSpawnPointPosition ()
    {
        int randomDistance = Random.Range(_range.Min, _range.Max);
        Vector3 spawnPoint = new Vector3(transform.position.x + randomDistance, transform.position.y);

        CheckSpawnPosition.Invoke(spawnPoint);

        return spawnPoint;
    }

    private void Spawn (Vector3 spawnPosition)
    {
        AddPositionToBlackList.Invoke(spawnPosition);

        if (_pool.TryGetObject(out GameObject spawnObject))
        {
            spawnObject.transform.position = spawnPosition;
        }

        spawnObject.gameObject.SetActive(true);
    }

    private void Update ()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn)
        {
            _elapsedTime = 0;
            GetSpawnPointPosition();
        }
    }
}

[System.Serializable]
public class Range
{
    public int Min;
    public int Max;
}
