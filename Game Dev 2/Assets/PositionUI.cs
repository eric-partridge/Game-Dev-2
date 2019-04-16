using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUI : MonoBehaviour
{

    public GameObject[] positions;
    public Sprite[] sprites;

    public void UpdateUI(int[] order) {
        foreach(GameObject g in positions){
            g.SetActive(false);
        }
        for(int i = 0; i < order.Length; i++) {
            positions[i].SetActive(true);
            positions[i].GetComponent<UnityEngine.UI.Image>().sprite = sprites[order[i]];
        }
    }
}
