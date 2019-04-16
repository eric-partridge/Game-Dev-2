using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Countdown : MonoBehaviour
{
    public static List<GameObject> ships;
    public Image countdown;
    public List<Sprite> images;
    private float timer;
    public GameObject line;
    int counter = 0;
    private float speed = 0;
    public static bool start = false;

    void Awake()
    {
        ships = new List<GameObject>();
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].GetComponent<playerController>().enabled = false;
        }
        speed = line.GetComponent<NavMeshAgent>().speed;
        line.GetComponent<NavMeshAgent>().speed = 0;
        countdown.sprite = images[0];
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer += Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counter < images.Count - 1)
        {
            timer += Time.fixedDeltaTime;
            //countdown.CrossFadeAlpha(1, BPM_Clock.SPB, false);
            if (timer >= BPM_Clock.SPB * 4)
            {
                timer = 0;
                counter += 1;
                //countdown.CrossFadeAlpha(0, 0, false);
                if (counter < images.Count)
                {
                    countdown.sprite = images[counter];
                }
            }
        }
        else if (counter == images.Count - 1)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                ships[i].GetComponent<playerController>().enabled = true;
            }
            line.GetComponent<NavMeshAgent>().speed = speed;
            countdown.sprite = images[counter];
            timer = 0;
            counter += 1;
            start = true;
            countdown.CrossFadeAlpha(0, BPM_Clock.SPB * 4, false);
        }
    }
}
