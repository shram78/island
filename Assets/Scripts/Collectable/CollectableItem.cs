using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private bool _isRed;
    [SerializeField] private bool _isFiol;
    [SerializeField] private bool _isGreen;
    [SerializeField] private bool _isBlue;

    private bool _isSelect = false;

    public bool IsRed => _isRed;
    public bool IsFiol => _isFiol;
    public bool IsGreen => _isGreen;
    public bool IsBlue => _isBlue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag playrsBag))
        {
            if (_isSelect == false)
                playrsBag.AddCoollectableItem(this);
        }
    }

    public void Select()
    {
        _isSelect = true;
    }
}
