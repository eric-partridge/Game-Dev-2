using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuck : MonoBehaviour {

    public AudioSource aud;
    public static float[] samples = new float[512];
    public static float[] bands = new float[8];
    public static float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];
    private float[] highest = new float[8];
    public static float[] intensity = new float[8];
    public static float amplitude;
    public static float maxAmp;

    // Use this for initialization
    void Start() {
        for (int i = 0;  i < 8; i++)
        {
            intensity[i] = 0;
            highest[i] = 0;
            bandBuffer[i] = 0;
        }
    }

    // Update is called once per frame
    void Update() {
        aud.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        makeBands();
        Buffer();
        GetIntensity();
        GetAmp();
    }

    void GetAmp()
    {
        amplitude = 0;
        for (int i = 0; i < 8; i++)
        {
            amplitude += bandBuffer[i];
        }
        if(amplitude > maxAmp)
        {
            maxAmp = amplitude;
        }
        amplitude = amplitude / maxAmp;
    }
    void makeBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average /= count;
            average = (float)(Math.Truncate((double)average * 10.0) / 10.0);
            bands[i] = average * 10;
        }
    }

    void Buffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (bands[i] > bandBuffer[i])
            {
                bandBuffer[i] = bands[i];
                bufferDecrease[i] = 0.01f;
            }

            else if (bands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.4f;
                if (bandBuffer[i] < 0)
                {
                    bandBuffer[i] = 0;
                    bufferDecrease[i] = 0.01f;
                }
            }
        }
    }

    void GetIntensity()
    {
        for (int i = 0; i < 8; i++)
        {
            if(bandBuffer[i] > highest[i])
            {
                highest[i] = bandBuffer[i];
            }
            if (highest[i] != 0)
            {
                intensity[i] = bandBuffer[i] / highest[i];
            }
        }
    }
}
