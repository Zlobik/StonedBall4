using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameObjectsGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _template;
    [SerializeField] private Transform _container;
    [SerializeField] private float _secondsBetweenSpawn = 1f;
    [SerializeField] private int _spawnRangeA = 1;
    [SerializeField] private int _spawnRangeB = 12;

    private float _elapsedTime = 0;

    private void Spawn ()
    {
        GameObject let = Instantiate(_template, _container);

        let.transform.position = new Vector3(transform.position.x + UnityEngine.Random.Range(_spawnRangeA, _spawnRangeB), transform.position.y);
        let.SetActive(true);
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
