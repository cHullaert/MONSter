using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABehaviour : MonoBehaviour {
    public class PNJ
    {
        public RandomBehaviour randomBehaviour;
        public float startTime { get; set; }
        public float length { get; set; }

        public void next() {
            if (randomBehaviour == null)
                return;

            startTime = randomBehaviour.getNextRandom(30) / 10.0f;
            length = randomBehaviour.getNextRandom(80) / 10.0f;

            Debug.Log("startTime" + startTime);
            Debug.Log("length" + length);
        }

        public PNJ(RandomBehaviour randomBehaviour) {
            this.randomBehaviour = randomBehaviour;
            this.next();
        }

    }

    public RandomBehaviour randomBehaviour;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
    }
}
