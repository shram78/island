using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShowView : MonoBehaviour
{
    [SerializeField] private GameObject _viewZone;
    [SerializeField] private Image _uiZone;
    [SerializeField] private ShakeTreeZone _shakeTreeZone;

    private void OnEnable()
    {
        _shakeTreeZone.Enter += OnEntered;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _viewZone.gameObject.SetActive(true);

            SetNormalScale();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            // _viewZone.gameObject.SetActive(false);
            _uiZone.transform.DOScale(0f, 0.5f);
    }

    private void OnDisable()
    {
        _shakeTreeZone.Enter -= OnEntered;
    }

    private void OnEntered(bool isEnter)
    {
        if (isEnter)
            _uiZone.transform.DOScale(1.5f, 0.5f);
        if(isEnter == false)
            _uiZone.transform.DOScale(1f, 0.5f);
    }

    private void SetNormalScale()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_uiZone.transform.DOScale(0f, 0f));
        sequence.Append(_uiZone.transform.DOScale(1f, 0.5f));
    }
}
