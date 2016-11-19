using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBehaviour : MonoBehaviour {
    private int[] randomArray = new int[2000];
    private int currentRandom = 0;

    private void initializeRandom(int maxValue)
    {
        for (int arrayIndex = 0; arrayIndex < randomArray.Length; arrayIndex++)
            randomArray[arrayIndex] = UnityEngine.Random.Range(0, maxValue);
    }

    public int getNextRandom(int maxValue)
    {
        return randomArray[(currentRandom++ % randomArray.Length)] % maxValue;
    }

    // Use this for initialization
    void Start () {
        initializeRandom(100);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
