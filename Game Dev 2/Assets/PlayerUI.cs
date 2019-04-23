using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{

    public UnityEngine.UI.Text score_text;
    public UnityEngine.UI.Text speed_text;
    public UnityEngine.UI.Image battery;
    public Sprite[] battery_sprites;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        score_text.text = player.GetComponent<CheckPoint>().GetScore().ToString();
        speed_text.text = ((int)player.GetComponent<Rigidbody>().velocity.magnitude).ToString();
        if(player.GetComponent<weaponScript>().currEnergy > 0) {
            battery.sprite = battery_sprites[1];
        } else {
            battery.sprite = battery_sprites[0];
        }
    }
}
