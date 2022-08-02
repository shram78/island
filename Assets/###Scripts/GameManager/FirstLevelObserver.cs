using System.Collections;
using UnityEngine;

public class FirstLevelObserver : MonoBehaviour
{
    [SerializeField] private Zone _firstPalm;
    [SerializeField] private Zone _tent;
    [SerializeField] private Zone _razer;
    [SerializeField] private Zone _water;
    [SerializeField] private Zone _raft;
    [SerializeField] private GameObject _tentObserved;
    [SerializeField] private GameObject _razerObserver;
    [SerializeField] private GameObject _waterObserver;
    [SerializeField] private GameObject _joistick;
    [SerializeField] private GameObject _raftObserver;
    [SerializeField] private PointerController _pointerController;
    [SerializeField] private GameObject _pointerUI;

    private void OnEnable()
    {
        _firstPalm.Opened += OnOpenedPalm;
        _tent.Opened += OnOpenedTent;
        _razer.Opened += OnOpenedRazer;
        _water.Opened += OnOpenedWater;
        _raft.Opened += OnOpenedRaft;
    }

    private void Start()
    {
        StartCoroutine(SwitchJoistickTimer());
    }

    private void OnDisable()
    {
        _firstPalm.Opened -= OnOpenedPalm;
        _tent.Opened -= OnOpenedTent;
        _razer.Opened -= OnOpenedRazer;
        _water.Opened -= OnOpenedWater;
        _raft.Opened -= OnOpenedRaft;

    }

    private void OnOpenedPalm()
    {
        _tentObserved.gameObject.SetActive(true);

        _pointerController.SetNewTarget(_tent.Pointer.transform);
    }

    private void OnOpenedTent()
    {
        _razerObserver.gameObject.SetActive(true);
        _pointerController.SetNewTarget(_razer.Pointer.transform);
    }

    private void OnOpenedRazer()
    {
        _waterObserver.gameObject.SetActive(true);
        _raftObserver.gameObject.SetActive(true);
        _pointerController.SetNewTarget(_water.Pointer.transform);
    }

    private void OnOpenedWater()
    {
        _pointerController.SetNewTarget(_raft.Pointer.transform);
    }

    private void OnOpenedRaft()
    {
        _pointerUI.gameObject.SetActive(false);
    }

    private IEnumerator SwitchJoistickTimer()
    {
        yield return new WaitForSeconds(2f);
        _joistick.gameObject.SetActive(true);
    }
}
