using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFeetOnGround : MonoBehaviour
{
    public bool OnGround;

    private void OnTriggerStay(Collider other)
    {
        OnGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        OnGround = false;
    }
}
