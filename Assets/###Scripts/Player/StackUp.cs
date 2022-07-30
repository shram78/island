using UnityEngine;

public class StackUp : MonoBehaviour
{
    [SerializeField] private Zone _sosZone;
    [SerializeField] private PlayrsBag _bag;

    private void OnEnable()
    {
        _sosZone.Opened += OnTryIncrease;
    }

    private void OnDisable()
    {
        _sosZone.Opened -= OnTryIncrease;
    }

    private void OnTryIncrease()
    {
        _bag.StackUp();
    }
}
