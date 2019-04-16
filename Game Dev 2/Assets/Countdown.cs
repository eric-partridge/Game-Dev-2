using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public static List<GameObject> ships;
    public Image countdown;
    public List<Sprite> images;
    private float timer;
    int counter = 0;

    void Awake()
    {
        ships = new List<GameObject>();
        images = new List<Sprite>();
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].GetComponent<playerController>().enabled = false;
        }
        countdown.sprite = images[0];
        timer = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (counter < images.Count)
        {
            countdown.CrossFadeAlpha(255, BPM_Clock.SPB, false);
            timer += Time.deltaTime;
            if (timer >= BPM_Clock.SPB * 2)
            {
                timer = 0;
                counter += 1;
                countdown.CrossFadeAlpha(0, 0, false);
                if (counter < images.Count)
                {
                    countdown.sprite = images[counter];
                }
            }
        }
        else if(counter == images.Count)
        {
            for (int i = 0; i < ships.Count; i++)
            {
                ships[i].GetComponent<playerController>().enabled = true;
            }
            counter += 1;
        }
    }
}
