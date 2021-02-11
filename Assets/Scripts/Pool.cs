using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
