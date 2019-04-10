using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Resample : MonoBehaviour
{
    private AudioClip file;
    public AudioSource BGM;
    public AudioSource Sampler;
    private float[] sample;
    public int samplerate = 44100;
    public float frequency = 440;
    public int position = 0;
    public int currentPos;
    private float timeLength;
    private float SPB;
    public float BPM = 175;
    private float waitTime;


    // Start is called before the first frame update
    void Start()
    {
        SPB = 60 / BPM;
        file = BGM.clip;
        sample = new float[file.samples * file.channels];
        file.GetData(sample, 0);
        AudioClip myClip = AudioClip.Create("resample", sample.Length, 2, samplerate, false, OnAudioRead);
        myClip.SetData(sample, 0);
        Sampler.clip = myClip;
        Sampler.volume = 1;
    }

    void OnAudioRead(float[] data)
    {
        int count = 0;
        while (count < data.Length)
        {
            data[count] = Mathf.Sin(2 * Mathf.PI * frequency * position / samplerate);
            position++;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ResampleLoop();
        }
    }
    public float ResampleLoop()
    {
        if (waitTime > LapTimer.timer)
        {
            waitTime = LapTimer.timer + SPB + SPB / 4;
            Sampler.Play();
            return waitTime;
        }
        else
        {
            waitTime = LapTimer.timer + SPB + SPB / 4;
            Sampler.clip = MakeSubclip(file, LapTimer.timer, LapTimer.timer + SPB);
            Sampler.Play();
            return waitTime;
        }
    }

    /*
    IEnumerator FadeOut(AudioSource Aud, float FadeTime)
    {
        float startVolume = Aud.volume;
        while (Aud.volume > 0)
        {
            Aud.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    IEnumerator FadeIn(AudioSource Aud, float FadeTime)
    {
        while (Aud.volume < 1)
        {
            Aud.volume += Time.deltaTime / FadeTime;
            print(Aud.volume);

            yield return null;
        }
    }*/


    public AudioClip MakeSubclip(AudioClip clip, float start, float stop)
    {
        timeLength = stop - start;
        int samplesLength = (int)(samplerate * timeLength);
        AudioClip newClip = AudioClip.Create("resample", samplesLength, 2, samplerate, false);
        float[] data = new float[samplesLength * 2];
        clip.GetData(data, (int)(samplerate * start));
        newClip.SetData(data, 0);
        return newClip;
    }
}
