using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UiActivator : MonoBehaviour
{
    [SerializeField] private GameObject _viewZone;
    [SerializeField] private GameObject _textName;
    [SerializeField] private Image _uiZone;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        else if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _viewZone.gameObject.SetActive(true);

            SetNormalScale();

            _textName.transform.DOScale(0.2f, 0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Monkey monkey))
            return;

        else if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            _uiZone.transform.DOScale(0f, 0.5f);

            _textName.transform.DOScale(0f, 0.5f);
        }
    }

    private void SetNormalScale()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_uiZone.transform.DOScale(0f, 0f));
        sequence.Append(_uiZone.transform.DOScale(1f, 0.5f));
    }
}
