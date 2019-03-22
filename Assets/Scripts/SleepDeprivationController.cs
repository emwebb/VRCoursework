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
	// Use this for initialization
	void Start () {
		
	}
	
    void updateRelevantValues()
    {
        inputController.lag = sleepDeprivationHours * (0.05f / 3.0f);
        postProccessingController.focusSpeed = Mathf.Pow(2, -(sleepDeprivationHours/10.0f)+1);
        postProccessingController.apeture = 1.0f + (31.0f / sleepDeprivationHours);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
