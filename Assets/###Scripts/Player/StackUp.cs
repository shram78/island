using System.Collections;
using TMPro;
using UnityEngine;

public class StackUp : MonoBehaviour
{
    [SerializeField] private Zone _sosZone;
    [SerializeField] private PlayrsBag _bag;
    [SerializeField] private TMP_Text _messageStackUp;

    private float _timeToShowMessage = 7f;

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

        _messageStackUp.gameObject.SetActive(true);

        _messageStackUp.text = "stack capacity = " + _bag.CurrentStack;

        StartCoroutine(SetMessageTimer());
    }

    private IEnumerator SetMessageTimer()
    {
        yield return new WaitForSeconds(_timeToShowMessage);
        _messageStackUp.gameObject.SetActive(false);

    }
}
