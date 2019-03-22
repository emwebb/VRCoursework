using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicroSleepController : MonoBehaviour {
    public float minTimeBetweenSleeps = 10;
    public float maxTimeBetweenSleeps = 1000;
    public float maximumMicrosleep = 0.01f;
    public float minimumMicrosleep = 5;
    public bool microSleepOn = false;
    public Image blackoutImage;
	// Use this for initialization
	void Start () {

        
	}

    public void StopMicroSleeps()
    {
        CancelInvoke("StartMicroSleep");
    }

    public void startDelayedMicroSleeps()
    {
        CancelInvoke("StartMicroSleep");
        float nextMicroSleepTime = Random.Range(minTimeBetweenSleeps, maxTimeBetweenSleeps);
        Invoke("StartMicroSleep", nextMicroSleepTime);
    }
	
    void StartMicroSleep()
    {
        microSleepOn = true;
        blackoutImage.enabled = true;
        float microSleepTime = Random.Range(minimumMicrosleep, maximumMicrosleep);
        Invoke("StopMicroSleep", microSleepTime);
    }
    void StopMicroSleep()
    {
        microSleepOn = false;
        blackoutImage.enabled = false;
        float nextMicroSleepTime = Random.Range(minTimeBetweenSleeps, maxTimeBetweenSleeps);
        Invoke("StartMicroSleep", nextMicroSleepTime);
    }
}
