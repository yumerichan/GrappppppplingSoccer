using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBall : MonoBehaviour
{
    public float r_x = 10f;
    public float r_y = 10f;
    public float r_z = 10f;
    public float speed = 50f;//”ò‚Î‚·‚Æ‚«‚Ì‰‘¬“x


    Vector3 _oldPos;

    public float maxTime = 5f;
    private float curTime;


    // Start is called before the first frame update
    void Start()
    {
        //“K“–‚É”ò‚Î‚·
        

        Vector3 vel = new Vector3(Random.Range(-r_x, r_x), Random.Range(-r_y, r_y), Random.Range(-r_z, r_z)).normalized * speed;
        GetComponent<Rigidbody>().velocity = vel;
        curTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= maxTime)
        {
            curTime = 0f;

            Vector3 vel = new Vector3(Random.Range(-r_x, r_x), Random.Range(-r_y, r_y), Random.Range(-r_z, r_z)).normalized * speed;
            GetComponent<Rigidbody>().velocity = vel;
        }




    }
}
