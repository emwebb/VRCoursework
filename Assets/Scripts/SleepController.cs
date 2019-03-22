using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepController : MonoBehaviour {
    [SerializeField]
    public Text text;
    [SerializeField]
    private float _hoursSinceLastSleep = 0;
    public float hourseSinceLastSleep
    {
        get { return _hoursSinceLastSleep; }
        set {
            this._hoursSinceLastSleep = value;
            text.text = "Hours since last sleep : " + Mathf.Floor(value);
        }
    }
	// Use this for initialization
	void Start () {
        text.text = "Hours since last sleep : " + Mathf.Floor(hourseSinceLastSleep).ToString();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
