using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatorsButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _creatorsPanel;
    [SerializeField] private Button _backToMenu;
    [SerializeField] private float _fadeTime;

    private bool _isClickOnBackButton = false;
    private float _elapsedTime = 0;

    public void OnButtonClick ()
    {
        _creatorsPanel.gameObject.SetActive(true);
        _creatorsPanel.DOFade(1, _fadeTime);
    }

    public void OnBackButtonClick ()
    {
        _creatorsPanel.DOFade(0, _fadeTime);
        _isClickOnBackButton = true;
    }

    private void FixedUpdate ()
    {
        if (_isClickOnBackButton)
        {
            if(_creatorsPanel)

            _elapsedTime += Time.fixedDeltaTime;

            if (_elapsedTime >= _fadeTime)
            {
                _creatorsPanel.gameObject.SetActive(false);

                _elapsedTime = 0;
                _isClickOnBackButton = false;
            }
        }
    }
}
