using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScore : MonoBehaviour
{
    public Text score_text;
    private int score_num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            score_num += 1;
            score_text.text = score_num.ToString();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        score_num += 1;
        score_text.text = score_num.ToString();
    }
}
