using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArrow : MonoBehaviour{

    SelectMenuManager smm;

    string horz;
    string vert;
    string select;
    string back;
    int player;

    public void Setup(SelectMenuManager s, int p) {
        smm = s;
        player = p;
        horz = "Horizontal P" + (player+1).ToString();
        vert = "Vertical P" + (player + 1).ToString();
        select = "X P" + (player + 1).ToString();
        back = "Square P" + (player + 1).ToString();
        Debug.Log(horz);
        Debug.Log(vert);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(horz) > 0 || Input.GetKeyDown(KeyCode.RightArrow)) {
            smm.Move(player, "right");
        }
        if (Input.GetAxis(vert) > 0 || Input.GetKeyDown(KeyCode.UpArrow)) {
            smm.Move(player, "up");
        }
        if (Input.GetAxis(horz) < 0 || Input.GetKeyDown(KeyCode.LeftArrow)) {
            smm.Move(player, "left");
        }
        if (Input.GetAxis(vert) < 0 || Input.GetKeyDown(KeyCode.DownArrow)) {
            smm.Move(player, "down");
        }
        if (Input.GetAxis(select) > 0 || Input.GetKeyDown(KeyCode.Return)) {
            smm.Select(player);
        }
        if (Input.GetAxis(back) > 0 || Input.GetKeyDown(KeyCode.Backspace)) {
            smm.DeSelect(player);
        }

    }
}
