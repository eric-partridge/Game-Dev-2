using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArrow : MonoBehaviour{

    SelectMenuManager smm;

    string horz;
    string vert;
    string DPadX;
    string DPadY;
    string select;
    string back;
    int player;

    private float changeTime = 0f;
    private float loadTime = 0f;

    public void Setup(SelectMenuManager s, int p) {
        smm = s;
        player = p;
        horz = "Horizontal P" + (player+1).ToString();
        vert = "Vertical P" + (player + 1).ToString();
        DPadX = "DPadX P" + (player + 1).ToString();
        DPadY = "DPadY P" + (player + 1).ToString();
        select = "X P" + (player + 1).ToString();
        back = "Square P" + (player + 1).ToString();
        loadTime = Time.fixedTime;
        Debug.Log(horz);
        Debug.Log(vert);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime > changeTime + .25f)
        {
            if (Input.GetAxis(horz) > 0 || Input.GetAxis(DPadX) > 0 ||Input.GetKeyDown(KeyCode.RightArrow))
            {
                smm.Move(player, "right");
                changeTime = Time.fixedTime;
            }
            if (Input.GetAxis(vert) < 0 || Input.GetAxis(DPadY) < 0 || Input.GetKeyDown(KeyCode.UpArrow))
            {
                smm.Move(player, "up");
                changeTime = Time.fixedTime;
            }
            if (Input.GetAxis(horz) < 0 || Input.GetAxis(DPadX) < 0 || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                smm.Move(player, "left");
                changeTime = Time.fixedTime;
            }
            if (Input.GetAxis(vert) > 0 || Input.GetAxis(DPadY) > 0 || Input.GetKeyDown(KeyCode.DownArrow))
            {
                smm.Move(player, "down");
                changeTime = Time.fixedTime;
            }
            if ((Input.GetAxis(select) > 0 || Input.GetKeyDown(KeyCode.Return)) && Time.fixedTime > loadTime + .25f)
            {   
                smm.Select(player);
            }
            if (Input.GetAxis(back) > 0 || Input.GetKeyDown(KeyCode.Backspace))
            {
                smm.DeSelect(player);
            }
        }

    }
}
