using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinsCounter;

    private int _coins = 0;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.GetComponent<Coin>())
        {
            _coins++;
            _coinsCounter.text = _coins.ToString();
        }
    }

}
