using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTriggerBehaviour : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
