using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RotateArms(float angle)
    {
        transform.RotateAround(transform.GetChild(0).position, Vector3.forward, angle);
    }
}
