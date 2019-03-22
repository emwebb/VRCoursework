using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepDeprivationController : MonoBehaviour {

    public int _sleepDeprivationHours = 0;
    public int sleepDeprivationHours
    {
        get { return _sleepDeprivationHours; }
        set {
            _sleepDeprivationHours = value;
            updateRelevantValues();

        }
    }

    public InputController inputController;
    public PostProccessing postProccessingController;
    public MicroSleepController microSleepController;
    public Camera mainCamera;
	// Use this for initialization
	void Start () {
        updateRelevantValues();
	}
	
    void updateRelevantValues()
    {
        inputController.lag = sleepDeprivationHours * (0.05f / 3.0f);
        postProccessingController.focusSpeed = Mathf.Pow(2, -(sleepDeprivationHours/10.0f)+1);
        postProccessingController.apeture = 1.0f + (31.0f / sleepDeprivationHours);
        if(sleepDeprivationHours > 0)
        {
            microSleepController.maxTimeBetweenSleeps = Mathf.Sqrt( 1000 / sleepDeprivationHours);
            microSleepController.minTimeBetweenSleeps = Mathf.Sqrt(100 / sleepDeprivationHours);
            microSleepController.startDelayedMicroSleeps();
        } else
        {
            microSleepController.StopMicroSleeps();
        }
        mainCamera.fieldOfView = 40 + 20 / (Mathf.Pow(sleepDeprivationHours/10.0f,0.5f) + 1);
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
