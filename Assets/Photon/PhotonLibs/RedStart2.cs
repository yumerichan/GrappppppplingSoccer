using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSrart2 : MonoBehaviour
{
    // Start is called before the first frame update
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
}
