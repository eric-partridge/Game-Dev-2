using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public GameObject arrow;
    int arrow_state = 0;
    public MenuManager mm;

    private float changeTime = 0f;
    private bool resetStickY = true;


    // Update is called once per frame
    void Update()
    {
        //select
        if (Input.GetButtonDown("X P1") || Input.GetButtonDown("X P2") || Input.GetButtonDown("X P3") || Input.GetButtonDown("X P4") || Input.GetKeyDown(KeyCode.Return)) {
            if (arrow_state == 0) {
                StartCoroutine(menuChange("1p"));
            } else if(arrow_state == 1) {
                StartCoroutine(menuChange("2p"));
            } else if (arrow_state == 2) {
                StartCoroutine(menuChange("4p"));
            } else if (arrow_state == 3) {
               StartCoroutine(menuChange("help"));
            }
            
        }

        //move arrow
        if((Input.GetAxis("DPadY P1") > 0.3f || Input.GetAxis("Vertical P1") > 0.3f || Input.GetKeyDown(KeyCode.DownArrow)) && resetStickY)
        {
            MoveArrow(1);
        }
        if ((Input.GetAxis("DPadY P1") < -0.3f || Input.GetAxis("Vertical P1") < -0.3f || Input.GetKeyDown(KeyCode.UpArrow)) && resetStickY)
        {
            MoveArrow(-1);
        }
        //makes you have to actually flick the stick (cant hold it)
        if (Input.GetAxis("Vertical P1") > 0.3f || Input.GetAxis("Vertical P1") < -0.3f || Input.GetAxis("DPadY P1") > 0.3f || Input.GetAxis("DPadY P1") < -0.3)
        {
            resetStickY = false;
        }
        else if (Input.GetAxis("Vertical P1") < 0.3f && Input.GetAxis("Vertical P1") > -0.3f && Input.GetAxis("DPadY P1") < 0.3f && Input.GetAxis("DPadY P1") > -0.3)
        {
            resetStickY = true;
        }

    }

    public void MoveArrow(int i) {
        int new_state = arrow_state + i;
        if(new_state == 4) { new_state = 0; }
        if(new_state == -1) { new_state = 3; }  
        arrow.GetComponent<RectTransform>().localPosition = new Vector3(-525, -50 - (new_state * 125), 0);
        arrow_state = new_state;
    }

    IEnumerator menuChange(string menu)
    {
        yield return new WaitForSeconds(0.25f);
        mm.ChangeMenu(menu);
    }

  
}
