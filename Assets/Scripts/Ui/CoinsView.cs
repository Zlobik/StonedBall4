using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TMP_Text))]
public class CoinsView : MonoBehaviour
{
    [SerializeField] private Ball _ball;

    private TMP_Text _coinsView;

    private void Start ()
    {
        _coinsView = GetComponent<TMP_Text>();
    }

    private void OnEnable ()
    {
        _ball.OnCoinCollected += ChangeText;
    }

    private void OnDisable ()
    {
        _ball.OnCoinCollected -= ChangeText;
    }

    private void ChangeText (int collectedCoins)
    {
        _coinsView.text = collectedCoins.ToString();
    }
}
