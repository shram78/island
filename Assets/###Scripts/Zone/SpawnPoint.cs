using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private bool _isClose = false;

    public bool IsClose => _isClose;

    public void ChangeFreeStatus(bool result)
    {
        _isClose = result;
    }
}
