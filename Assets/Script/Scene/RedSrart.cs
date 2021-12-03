using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedSrart : MonoBehaviour
{
    // Start is called before the first frame update
    private bool IsCollision;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            IsCollision = true;
        }
    }

    public bool GetOnRedCollision()
    {
        return IsCollision;
    }
}
