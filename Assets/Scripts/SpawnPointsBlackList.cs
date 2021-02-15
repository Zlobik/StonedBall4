using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnPointsBlackList : MonoBehaviour
{
    [SerializeField] private GameObjectsGenerator[] _gameObjectsGenerators;

    private List<Vector3> _blackList = new List<Vector3>();

    public event UnityAction<Vector3> AllowSpawn;

    private void Start ()
    {
        _blackList.Add(new Vector3(-5, 5, 0));
    }

    private void OnEnable ()
    {
        for (int i = 0; i < _gameObjectsGenerators.Length; i++)
        {
            _gameObjectsGenerators[i].CheckSpawnPosition += CheckPositionToSpawn;
            _gameObjectsGenerators[i].AddPositionToBlackList += AddPositionToBlackList;
        }
    }

    private void OnDisable ()
    {
        for (int i = 0; i < _gameObjectsGenerators.Length; i++)
        {
            _gameObjectsGenerators[i].CheckSpawnPosition -= CheckPositionToSpawn;
            _gameObjectsGenerators[i].AddPositionToBlackList -= AddPositionToBlackList;
        }
    }

    private void AddPositionToBlackList (Vector3 position)
    {
        _blackList.Add(position);
    }

    private void CheckPositionToSpawn (Vector3 position)
    {
        bool isBusy = false;

        for (int i = 0; i < _blackList.Count; i++)
        {
            if (position.x > _blackList[i].x - 1 && position.x > _blackList[i].x + 1 || position.x < _blackList[i].x - 2)
            {
            }
            else
            {
                isBusy = true;
            }
        }

        if (!isBusy)
        {
            AllowSpawn.Invoke(position);
        }
    }
}
