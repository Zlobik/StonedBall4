using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
    private int _coins = 0;

    public event UnityAction<int> OnCoinCollected;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.GetComponent<Coin>())
        {
            _coins++;
            OnCoinCollected?.Invoke(_coins);
        }
    }
}
