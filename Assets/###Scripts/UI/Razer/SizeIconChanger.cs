using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class SizeIconChanger : MonoBehaviour
{
    [SerializeField] private Image _uiZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            _uiZone.transform.DOScale(1.5f, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
            _uiZone.transform.DOScale(1f, 0.5f);
    }
}
