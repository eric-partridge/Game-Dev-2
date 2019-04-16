using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuManager : MonoBehaviour
{

    public MenuManager mm;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("X P1") || Input.GetButton("X P2") /*|| Input.GetButton("X P3") || Input.GetButton("X P4")*/|| Input.GetKeyDown(KeyCode.Return)) {
            mm.ChangeMenu("main");
        }
    }
}
