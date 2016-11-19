using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameBehaviour : NetworkBehaviour {
    private RandomBehaviour randomBehaviour;
    private GameObject[] spawns;

	// Use this for initialization
	void Start () {
        randomBehaviour=gameObject.GetComponent<RandomBehaviour>();
        if (randomBehaviour == null)
            Debug.LogError("cannot find random behaviour on game behaviour");

        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        if (spawns.Length == 0)
            Debug.LogError("cannot find respawn objects");
        else {
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
