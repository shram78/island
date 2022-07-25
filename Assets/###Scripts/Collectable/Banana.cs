using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : Branch
{

    protected override IEnumerator StartTimer()
    {
          yield return new WaitForSeconds(0.6f);

        SetRigidbody();
    }
}
