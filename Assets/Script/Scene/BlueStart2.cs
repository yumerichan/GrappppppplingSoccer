using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueStart2 : MonoBehaviour
{

    private bool IsCollision;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            IsCollision = true;

    }

    public bool GetOnRedCollision()
    {
        return IsCollision;
    }

    public void SetIsCollision()
    {
        IsCollision = false;
    }
}
