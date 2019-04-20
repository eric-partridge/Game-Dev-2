using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenuManager : MonoBehaviour
{

    public GameObject[] tracks;
    public GameObject[] overlay;
    public Sprite[] overlay_sprites;

    public List<GameObject> arrows;
    List<int> arrow_states;
    List<int> arrow_lock;

    public Sprite[] arrow_sprites;
    public GameObject arrow_prefab;
    public MenuManager mm;

    public GameObject loadScreen;
    public AudioSource aud;

    void Awake() {
        arrows = new List<GameObject>();
        arrow_states = new List<int>();
        arrow_lock = new List<int>();
    }

    public void InitArrows(int a) {
        CleanUp();
        for (int i = 0; i < a; i++) {
            arrows.Add(Instantiate(arrow_prefab, new Vector3(0, 0, 0), Quaternion.identity));
            arrows[i].transform.parent = gameObject.transform;
            arrows[i].GetComponent<SelectionArrow>().Setup(this, i);
            arrows[i].GetComponent<UnityEngine.UI.Image>().sprite = arrow_sprites[i];
            arrow_states.Add(i);
            ChangeState(i, i);
        }
    }

    public void ChangeState(int a, int s) {
        if (s == -1) return;
        arrow_states[a] = s;
        if(s == 0) {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(-630, 225 - (a * 50), 0);
        }else if (s == 1) {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(-630, -125 - (a * 50), 0);
        }else if (s == 2) {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(-30, 225 - (a * 50), 0);
        } else if (s == 3) {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(-30, -125 - (a * 50), 0);
        } else if (s == 4) {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(570, 225 - (a * 50), 0);
        } else if (s == 5) {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(570, -125 - (a * 50), 0);
        } else {
            arrows[a].GetComponent<RectTransform>().localPosition = new Vector3(-540, -350 - (a * 50), 0);
        }
    }

    public void Move(int a, string m) {
        if (arrow_lock.Contains(a)) return;
        int t = -1;
        if(arrow_states[a] == 0) {
            if (m.Equals("right")) {
                t = 2;
            } else if (m.Equals("down")) {
                t = 1;
            }
        } else if(arrow_states[a] == 1) {
            if (m.Equals("right")) {
                t = 3;
            } else if (m.Equals("up")) {
                t = 0;
            } else if (m.Equals("down")) {
                t = 6;
            }
        } else if(arrow_states[a] == 2) {
            if (m.Equals("right")) {
                t = 4;
            } else if (m.Equals("left")) {
                t = 0;
            } else if (m.Equals("down")) {
                t = 3;
            }
        } else if(arrow_states[a] == 3) {
            if (m.Equals("right")) {
                t = 5;
            } else if (m.Equals("up")) {
                t = 2;
            } else if (m.Equals("left")) {
                t = 1;
            } else if (m.Equals("down")) {
                t = 6;
            }
        } else if (arrow_states[a] == 4) {
            if (m.Equals("left")) {
                t = 2;
            } else if (m.Equals("down")) {
                t = 5;
            }
        } else if (arrow_states[a] == 5) {
            if (m.Equals("up")) {
                t = 4;
            } else if (m.Equals("left")) {
                t = 3;
            } else if (m.Equals("down")) {
                t = 6;
            }
        } else if (arrow_states[a] == 6) {
            if (m.Equals("up")) {
                t = 1;
            }
        }
        ChangeState(a, t);
    }

    public void Select(int p) {
        if (arrow_states[p] == 6) {
            mm.ChangeMenu("main");
            return;
        }
        if (arrow_lock.Contains(p)) return;
        //NO DUPS
        foreach(int q in arrow_lock) {
            if (arrow_states[q] == arrow_states[p]) return;
        }
        PlayerPrefs.SetInt("p" + p, arrow_states[p]);
        arrow_lock.Add(p);
        //ANIMATION
        overlay[arrow_states[p]].SetActive(true);
        overlay[arrow_states[p]].GetComponent<UnityEngine.UI.Image>().sprite = overlay_sprites[p];
        arrows[p].GetComponent<UnityEngine.UI.Image>().enabled = false;
        if (arrow_lock.Count == arrows.Count) {
            loadScreen.SetActive(true);

            StartCoroutine(FadeOut(aud, 1f));
            StartCoroutine(LoadNewScene());
        }
    }

    public void DeSelect(int p) {
        if (arrow_lock.Contains(p)) {
            arrow_lock.Remove(p);
            overlay[arrow_states[p]].SetActive(false);
            arrows[p].GetComponent<UnityEngine.UI.Image>().enabled = true;
        }
    }
    void CleanUp() {
        for(int i = 0; i < arrows.Count; i++) {
            Destroy(arrows[i]);
        }
        arrows = new List<GameObject>();
        arrow_states = new List<int>();
        arrow_lock = new List<int>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 v;
        for(int i = 0; i < tracks.Length; i++) {
            v = tracks[i].transform.localPosition;
            v.z -= 250 * Time.deltaTime;
            if (v.z <= -725) {
                v.z += (375 * 3);
            }
            tracks[i].transform.localPosition = v;
        }
        
    }

    IEnumerator LoadNewScene() {
        yield return new WaitForSeconds(1);
        // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.

        AsyncOperation async = Application.LoadLevelAsync("Ice_World");

        // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
        while (!async.isDone) {
            yield return null;
        }

    }

    IEnumerator FadeOut(AudioSource Aud, float FadeTime)
    {
        float startVolume = Aud.volume;
        while (Aud.volume > 0)
        {
            Aud.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
    }
}
