using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public GameObject arrow;
    int arrow_state = 0;
    public MenuManager mm;

    private float changeTime = 0f;


    // Update is called once per frame
    void Update()
    {
        //select
        if (Input.GetButton("X P1") || Input.GetButton("X P2") || Input.GetButton("X P3") || Input.GetButton("X P4") || Input.GetKeyDown(KeyCode.Return)) {
            if (arrow_state == 0) {
                mm.ChangeMenu("1p");
            } else if(arrow_state == 1) {
                mm.ChangeMenu("2p");
            } else if (arrow_state == 2) {
                mm.ChangeMenu("4p");
            } else if (arrow_state == 3) {
                mm.ChangeMenu("help");
            }
            
        }

        //move arrow
        if((Input.GetAxis("DPadY P1") > 0 || Input.GetAxis("Vertical P1") > 0 || Input.GetKeyDown(KeyCode.DownArrow)) && Time.fixedTime > changeTime + .25f){
            changeTime = Time.fixedTime;
            MoveArrow(1);
        }
        if ((Input.GetAxis("DPadY P1") < 0 || Input.GetAxis("Vertical P1") < 0 || Input.GetKeyDown(KeyCode.UpArrow)) && Time.fixedTime > changeTime + .5f) {
            changeTime = Time.fixedTime;
            MoveArrow(-1);
        }

    }

    public void MoveArrow(int i) {
        int new_state = arrow_state + i;
        if(new_state == 4) { new_state = 0; }
        if(new_state == -1) { new_state = 3; }  
        arrow.GetComponent<RectTransform>().localPosition = new Vector3(-525, -50 - (new_state * 125), 0);
        arrow_state = new_state;
    }

  
}
