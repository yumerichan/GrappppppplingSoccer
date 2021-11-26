using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  ƒJƒƒ‰‚Ì‰ñ“]Šp‚É‡‚í‚¹‚é
        transform.rotation = Camera.main.transform.rotation;
    }


}
