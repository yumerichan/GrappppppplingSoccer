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
        //  �J�����̉�]�p�ɍ��킹��
        transform.rotation = Camera.main.transform.rotation;
    }


}
