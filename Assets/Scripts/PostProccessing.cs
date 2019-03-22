using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProccessing : MonoBehaviour {

    public PostProcessVolume postProcessVoulune;
    public float newDepth;
    public float depth;
    public float focusSpeed = 1;
    public float apeture = 0;
    public float focalLength = 0;
	// Use this for initialization
	void Start () {
        depth = newDepth;
	}
	
	// Update is called once per frame
	void Update () {
        int layerMask = ~(1 << 8);

        RaycastHit hit;
        if(Physics.Raycast(transform.position + transform.TransformDirection(Vector3.forward) , transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            newDepth = hit.distance;
        }

        DepthOfField dof;
        depth = Mathf.Lerp(depth, newDepth, focusSpeed * Time.deltaTime);
        postProcessVoulune.profile.TryGetSettings(out dof);

        dof.focusDistance.value = depth;
        dof.aperture.value = apeture;
        dof.focalLength.value = focalLength;
	}
}
