using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform Target;
    public float Smooth;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Target)
        {
            Vector3 target = new Vector3(Target.position.x, Target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * Smooth);
        }
	}

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}
