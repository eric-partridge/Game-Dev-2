using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{


    public GameObject helpCanvas;
    public GameObject helpCamera;

    public GameObject selectCanvas;
    public GameObject selectCamera;

    public GameObject mainCanvas;
    public GameObject mainCamera;

    public void ChangeMenu(string s) {
        if (s.Equals("help")) {
            //open help view
            mainCamera.SetActive(false);
            mainCanvas.SetActive(false);
            helpCamera.SetActive(true);
            helpCanvas.SetActive(true);
            selectCamera.SetActive(false);
            selectCanvas.SetActive(false);
        } else if (s.Equals("1p")) {
            PlayerPrefs.SetInt("num_p",1);
            //open character select
            mainCamera.SetActive(false);
            mainCanvas.SetActive(false);
            helpCamera.SetActive(false);
            helpCanvas.SetActive(false);
            selectCamera.SetActive(true);
            selectCanvas.SetActive(true);
            selectCanvas.GetComponent<SelectMenuManager>().InitArrows(1);
        } else if (s.Equals("2p")) {
            PlayerPrefs.SetInt("num_p", 2);
            //open character select
            mainCamera.SetActive(false);
            mainCanvas.SetActive(false);
            helpCamera.SetActive(false);
            helpCanvas.SetActive(false);
            selectCamera.SetActive(true);
            selectCanvas.SetActive(true);
            selectCanvas.GetComponent<SelectMenuManager>().InitArrows(2);
        } else if (s.Equals("4p")) {
            PlayerPrefs.SetInt("num_p", 4);
            //open character select
            mainCamera.SetActive(false);
            mainCanvas.SetActive(false);
            helpCamera.SetActive(false);
            helpCanvas.SetActive(false);
            selectCamera.SetActive(true);
            selectCanvas.SetActive(true);
            selectCanvas.GetComponent<SelectMenuManager>().InitArrows(4);
        } else if (s.Equals("main")) {
            //open main view
            mainCamera.SetActive(true);
            mainCanvas.SetActive(true);
            helpCamera.SetActive(false);
            helpCanvas.SetActive(false);
            selectCamera.SetActive(false);
            selectCanvas.SetActive(false);
        }
    }
}
