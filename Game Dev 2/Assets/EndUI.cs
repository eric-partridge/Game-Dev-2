using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{

    public UnityEngine.UI.Image[] pics;
    public UnityEngine.UI.Image[] names;
    public UnityEngine.UI.Text[] scores;
    public Sprite[] pic_sprites;
    public Sprite[] name_sprites;

    public void UpdateUI(List<KeyValuePair<int,double>> l) {
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
}
