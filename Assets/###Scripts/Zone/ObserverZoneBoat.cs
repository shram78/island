using UnityEngine;

public class ObserverZoneBoat : ObserverZoneRaft
{
    [SerializeField] private Zone _zoneBanana;
    [SerializeField] private GameObject[] _bananaInRaft;


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
        _zoneBanana.DropPrefabInStock += ShowBananaInRaft;

    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _zoneBanana.Opened -= OnSetBanana;
        _zoneBanana.DropPrefabInStock -= ShowBananaInRaft;

    }

    private void OnSetBanana()
    {
        _isBananaOpen = true;
        Delivery();
    }

     private void ShowBananaInRaft(int currentNumber)
    {
        for (int i = 0; i < currentNumber; i++)
            _bananaInRaft[i].gameObject.SetActive(true);
    }
}
