using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MInimap : MonoBehaviour
{
    RawImage miniMap_;

    // Start is called before the first frame update
    void Start()
    {
        //  最初はミニマップは非表示の状態
        miniMap_ = this.GetComponent<RawImage>();
        miniMap_.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        OnMap();
    }

    private void OnMap()
    {
        if (Input.GetButtonDown("Map") ||
            Input.GetKeyDown(KeyCode.M))
        {
            if (!miniMap_.enabled)
                miniMap_.enabled = true;
            else
                miniMap_.enabled = false;
        }
        
    }
}
