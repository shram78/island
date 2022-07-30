using UnityEngine;

public class ObserverZoneBoat : ObserverZoneRaft
{
    [SerializeField] private Zone _zoneBanana;

    private bool _isBananaOpen = false;

    protected override void Delivery()
    {
        if (_isBarellOpen && _isBananaOpen)
            _levelComplete.SetFoodCollected();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _zoneBanana.Opened += OnSetBanana;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _zoneBanana.Opened -= OnSetBanana;
    }

    private void OnSetBanana()
    {
        _isBananaOpen = true;
        Delivery();
    }
}
