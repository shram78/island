using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Zone : MonoBehaviour
{
    [SerializeField] private bool _isRed;
    [SerializeField] private bool _isFiol;
    [SerializeField] private bool _isGreen;
    [SerializeField] private bool _isBlue;
    [SerializeField] private Transform _pointToMove;
    [SerializeField] private TMP_Text _number;
    [SerializeField] private GameObject _object1;
    [SerializeField] private float _positionY1;
    [SerializeField] private GameObject _object2;
    [SerializeField] private float _positionY2;
    [SerializeField] private GameObject _object3;
    [SerializeField] private float _positionY3;
    [SerializeField] private GameObject _object4;
    [SerializeField] private float _positionY4;
    [SerializeField] private GameObject _object5;
    [SerializeField] private float _positionY5;
    [SerializeField] private GameObject _object6;
    [SerializeField] private float _positionY6;
    [SerializeField] private GameObject _object7;
    [SerializeField] private float _positionY7;
    [SerializeField] private GameObject _object8;
    [SerializeField] private float _positionY8;
    [SerializeField] private float _durationMove;
    [SerializeField] private float _durationMove2;
    [SerializeField] private int _countElement;
    [SerializeField] private bool _isZavod;
    [SerializeField] private bool _isBarrack;
    [SerializeField] private bool _isWeaponShop;
   // [SerializeField] private Zavod _zavod;
   // [SerializeField] private Barracks _barracks;
  //  [SerializeField] private WarehouseArmor _warehouse;
    [SerializeField] private GameObject _text;
    [SerializeField] private bool _isDeacrivate = true;
    [SerializeField] private bool _isTwoShanse;

    private bool _isStop = false;
    private PlayrsBag _currentBag;
    private int _currentNumber = 0;
    private int _maxElement;
    private bool _isOneUp = true;

    private void OnEnable()
    {
        EnableText();
    }

    private void Start()
    {
        if (_number != null)
        {
            _maxElement = _countElement;
            ChangedCounter();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isStop == false)
        {
            if (other.gameObject.TryGetComponent(out PlayrsBag bag))
            {
                _currentBag = bag;

                if (_isRed)
                    bag.DropRedELement(_countElement, _pointToMove, this);
                //if (_isFiol)
                //    bag.DropFiolELement(_countElement, _pointToMove, this);
                //if (_isGreen)
                //    bag.DropGreenELement(_countElement, _pointToMove, this);
                //if (_isBlue)
                //    bag.DropBlueELement(_countElement, _pointToMove, this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayrsBag bug))
        {
            bug.StopDropCoroutine();
        }
    }

    private void ChangedCounter()
    {
        _number.text = _currentNumber.ToString() + "/" + _maxElement.ToString();
    }

    public void RemoveNumber()
    {
        _countElement--;

        _currentNumber++;

        //if (_isZavod)
        //{
        //    _zavod.AddElement(1);
        //}
        //else if (_isBarrack)
        //{
        //    _barracks.AddElement(1);
        //}
        //else if (_isWeaponShop)
        //{
        //    _warehouse.AddShil();
        //}
        //else
        //{
        //    if (_countElement == 0)
        //    {
        //        if (_isOneUp)
        //        {
        //            MoveObject(_object1, _positionY1, _durationMove);
        //            MoveObject(_object2, _positionY2, _durationMove);
        //            MoveObject(_object3, _positionY3, _durationMove);
        //            MoveObject(_object4, _positionY4, _durationMove2);
        //            _isOneUp = false;
        //        }

        //        if (_isDeacrivate)
        //        {
        //            gameObject.SetActive(false);
        //        }
        //        else
        //        {
        //            if (_isTwoShanse)
        //            {
        //                _countElement = 9;
        //                _maxElement = _countElement;
        //                _currentNumber = 0;
        //                _isTwoShanse = false;
        //                _isStop = false;
        //            }
        //            else
        //            {
        //                _text.gameObject.SetActive(false);
        //                _isStop = true;

        //                MoveObject(_object6, _positionY6, _durationMove);
        //                MoveObject(_object5, _positionY5, _durationMove);
        //                MoveObject(_object7, _positionY7, _durationMove);
        //                MoveObject(_object8, _positionY8, _durationMove);
        //            }
        //        }
        //    }
        //}

        //if (_number != null)
        //{
        //    ChangedCounter();
        //}
    }

    //public void AddNumber()
    //{
    //    _countElement++;

    //    _currentNumber--;

    //    if (_number != null)
    //    {
    //        ChangedCounter();
    //    }
    //}

    public void MoveObject(GameObject element, float positionY, float duration)
    {
        if (element != null)
        {
            element.SetActive(true);
            element.transform.DOLocalMoveY(positionY, duration);
        }
    }

    public void ChangedWork(bool result)
    {
        _isStop = result;

        if (_isStop)
        {
            _currentBag.StopDropCoroutine();
        }
    }

    public void EnableText()
    {
        StartCoroutine(StartEnableText());
    }

    private IEnumerator StartEnableText()
    {
        yield return new WaitForSeconds(0.5f);

        if (_text != null)
        {
            _text.SetActive(true);
        }
    }
}
