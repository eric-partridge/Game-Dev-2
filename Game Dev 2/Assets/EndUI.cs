using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{

    public UnityEngine.UI.Image[] pics;
    public UnityEngine.UI.Image[] names;
    public UnityEngine.UI.Text[] scores;
    public Sprite[] pic_sprites;
    public Sprite[] name_sprites;
    int arrow_state = 0;
    public GameObject arrow;

    private bool resetStickY = true;

    public void UpdateUI(List<KeyValuePair<int,int>> l) {
        for (int i = 0; i < pics.Length; i++) {
            pics[i].gameObject.SetActive(false);
        }
        l.Sort();
        for (int i = 0; i < l.Count; i++) {
            pics[i].gameObject.SetActive(true);
            pics[i].sprite = pic_sprites[l[i].Key];
            names[i].sprite = name_sprites[l[i].Key];
            scores[i].text = l[i].Value.ToString();
        }
    }

    void Update() {
        //select
        if (Input.GetButtonDown("X P1") || Input.GetButtonDown("X P2") || Input.GetButtonDown("X P3") || Input.GetButtonDown("X P4") || Input.GetKeyDown(KeyCode.Return)) {
            if (arrow_state == 0) {
                StartCoroutine(sceneChange("Ice_World"));
            } else if (arrow_state == 1) {
                StartCoroutine(sceneChange("Main_Menu"));
            }

        }

        //move arrow
        if ((Input.GetAxis("DPadY P1") > 0.3f || Input.GetAxis("Vertical P1") > 0.3f || Input.GetKeyDown(KeyCode.DownArrow)) && resetStickY) {
            MoveArrow(1);
        }
        if ((Input.GetAxis("DPadY P1") < -0.3f || Input.GetAxis("Vertical P1") < -0.3f || Input.GetKeyDown(KeyCode.UpArrow)) && resetStickY) {
            MoveArrow(-1);
        }
        //makes you have to actually flick the stick (cant hold it)
        if (Input.GetAxis("Vertical P1") > 0.3f || Input.GetAxis("Vertical P1") < -0.3f || Input.GetAxis("DPadY P1") > 0.3f || Input.GetAxis("DPadY P1") < -0.3) {
            resetStickY = false;
        } else if (Input.GetAxis("Vertical P1") < 0.3f && Input.GetAxis("Vertical P1") > -0.3f && Input.GetAxis("DPadY P1") < 0.3f && Input.GetAxis("DPadY P1") > -0.3) {
            resetStickY = true;
        }

    }

    public void MoveArrow(int i) {
        int new_state = arrow_state + i;
        if (new_state == 2) { new_state = 0; }
        if (new_state == -1) { new_state = 1; }
        arrow.GetComponent<RectTransform>().localPosition = new Vector3(-535, -325 - (new_state * 75), 0);
        arrow_state = new_state;
    }

    IEnumerator sceneChange(string menu) {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(menu);
    }
}
