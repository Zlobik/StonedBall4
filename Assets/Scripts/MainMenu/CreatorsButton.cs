using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CreatorsButton : MonoBehaviour
{
    [SerializeField] private CanvasGroup _creatorsPanel;
    [SerializeField] private Button _backToMenu;
    [SerializeField] private float _fadeTime;

    public void OnButtonClick ()
    {
        _creatorsPanel.gameObject.SetActive(true);
        _creatorsPanel.DOFade(1, _fadeTime);
    }

    public void OnBackButtonClick ()
    {
        _creatorsPanel.DOFade(0, _fadeTime);

        StartCoroutine(DisablePanel());
    }

    private IEnumerator DisablePanel ()
    {
        var waitForSeconds = new WaitForSeconds(_fadeTime);

        yield return waitForSeconds;

        _creatorsPanel.gameObject.SetActive(false);
    }
}
