using UnityEngine;

public class DetachmentBridge : MonoBehaviour
{
    [SerializeField] private LevelComlete _levelComplete;
    private void OnEnable()
    {
        _levelComplete.Comleted += OnArrivedShip;
    }

    private void OnDisable()
    {
        _levelComplete.Comleted -= OnArrivedShip;
    }

    private void OnArrivedShip()
    {
        transform.SetParent(null);
    }
}
