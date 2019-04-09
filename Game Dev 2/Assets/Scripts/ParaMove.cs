using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParaMove : MonoBehaviour
{
    Vector3 start;
    bool every_other = true;
    bool trig = false;
        
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(trig == true && BPM_Clock.trigger == false && every_other)
        {
            every_other = false;
            trig = false;
        }
        else if(trig == true && BPM_Clock.trigger == false && !every_other)
        {
            every_other = true;
            trig = false;
        }
        if (BPM_Clock.trigger && every_other)
        {
            transform.position = Vector3.Lerp(start, start + transform.forward * 90, 0.1f);
            trig = true;
        }
        else if(BPM_Clock.trigger && !every_other)
        {
            trig = true;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, start, 0.1f);
        }
    }
}
