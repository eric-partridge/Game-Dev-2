using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class MusicReader : MonoBehaviour {

    public TextAsset textFile;
    private float BPM;
    public static float BPS;
    public static float measureTime;
    public static int beats;
    public static int combo = 1;
    //private List<float> notes;
    private List<float> kicks;
    private string[] lines;
    private List<string> lineList;
    private string[] command;
    private float currentTime;
    private float changeTime;
    //private int currentNote = 0;
    private int currentKick = 0;
    private bool noteOn = true;
    private bool animationOn = true;
    private bool hit = false;
    private GameObject player;
    private GameObject tankObj;
    private GameObject roadObj;
    private Animator car;
    private Animator tank;
    private AudioSource shot;
    private Texture road;
    public List<Texture> roadList;
    private int currentRoad = 1;

	// Use this for initialization
	void Start () {
        lines = textFile.text.Split('\n');
        lineList = new List<string>(lines);
        CollectData();
        player = GameObject.Find("Player");
        tankObj = GameObject.Find("Enemy");
        roadObj = GameObject.Find("Quad");
        car = player.GetComponent<Animator>();
        tank = tankObj.GetComponent<Animator>();
        shot = player.GetComponent<AudioSource>();
        roadObj.GetComponent<Renderer>().material.mainTexture = roadList[0];
        gameObject.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
        /*
        print(BPM);
        print(BPS);
        print(beats);
        print(measureTime);
        print(kicks[1]);
        */
        currentTime += Time.deltaTime;
        if (Time.time >= changeTime)
        {
            CollectData();
        }
        if (currentTime >= measureTime)
        {
            currentTime -= measureTime;
            //currentNote = 0;
            currentKick = 0;
            noteOn = true;
            animationOn = true;
            if (hit == false)
            {
                combo = 1;
            }
            hit = false;
        }
        /*
        if (currentTime > notes[currentNote] + 0.15)
        {
            currentNote++;
            noteOn = true;
            if (currentNote >= notes.Count)
            {
                currentNote = 0;
            }
            print(notes[currentNote]);
        }*/
        else if (currentTime > kicks[currentKick + 1])
        {
            noteOn = true;
            animationOn = true;
            currentKick++;
            if (currentKick >= kicks.Count)
            {
                currentKick = 0;
            }
            if (hit == false)
            {
                combo = 1;
            }
            hit = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (currentTime >= (kicks[currentKick] - 0.25) && currentTime <= (kicks[currentKick] + 0.35) && noteOn)
            {
                print(currentTime);
                noteOn = false;
                hit = true;
                player.SendMessage("Attack");
                shot.Play();
                combo++;
            }
            else
            {
                combo = 1;
            }
        }
        if (currentTime >= kicks[currentKick] - 0.016f)
        {
            if (animationOn)
            {
                car.SetTrigger("BeatPress");
                if (Time.time >= 32)
                {
                    tank.SetInteger("HitNum", tank.GetInteger("HitNum") + 1);
                    if (tank.GetInteger("HitNum") == 5)
                    {
                        tank.SetInteger("HitNum", 1);
                    }
                    roadObj.GetComponent<Renderer>().material.mainTexture = roadList[currentRoad];
                    currentRoad++;
                    if(currentRoad >= roadList.Count)
                    {
                        currentRoad = 0;
                    }
                }
                animationOn = false;
            }
        }
    }

    private void CollectData()
    {
        for (int line = 0; line < lineList.Count; line++)
        {
            command = lineList[line].Split(' ');
            if (command[0] == "BPM")
            {
                BPM = float.Parse(command[1], CultureInfo.InvariantCulture.NumberFormat);
                lineList[line] = "Done";
            }
            if (command[0] == "BPS")
            {
                BPS = float.Parse(command[1], CultureInfo.InvariantCulture.NumberFormat);
                lineList[line] = "Done";
            }
            if (command[0] == "Beats")
            {
                beats = int.Parse(command[1]);
                lineList[line] = "Done";
            }
            if (command[0] == "Measure_Time")
            {
                measureTime = float.Parse(command[1], CultureInfo.InvariantCulture.NumberFormat);
                lineList[line] = "Done";
            }
            if (command[0] == "Kick")
            {
                kicks = new List<float>();
                for (int i = 1; i < command.Length; i++)
                {
                    kicks.Add(float.Parse(command[i], CultureInfo.InvariantCulture.NumberFormat));
                }
                lineList[line] = "Done";
            }
            /*
            if (command[0] == "Bass")
            {
                notes = new List<float>();
                for (int i = 1; i < command.Length; i++)
                {
                    notes.Add(float.Parse(command[i], CultureInfo.InvariantCulture.NumberFormat));
                }
                lineList[line] = "Done";
            }*/
            if (command[0] == "Change")
            {
                changeTime = float.Parse(command[1], CultureInfo.InvariantCulture.NumberFormat);
                lineList[line] = "Done";
                break;
            }
        }
    }
}
