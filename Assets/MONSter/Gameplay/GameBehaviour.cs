using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameBehaviour : NetworkBehaviour {
    public class ReservedTentacle {
        public GameObject spawnObject { get; set; } 
        public GameObject gameObject { get; set; }

        public ReservedTentacle(GameObject spawnObject, GameObject gameObject) {
            this.spawnObject = spawnObject;
            this.gameObject = gameObject;
        } 
    }

    private RandomBehaviour randomBehaviour;
    private List<ReservedTentacle> reservedTentacles;
    private GameObject[] spawns;

    private void checkList() {
        if (reservedTentacles == null)
            reservedTentacles = new List<ReservedTentacle>();
    }

	// Use this for initialization
	void Start () {
        randomBehaviour=gameObject.GetComponent<RandomBehaviour>();
        if (randomBehaviour == null)
            Debug.LogError("cannot find random behaviour on game behaviour");

        spawns = GameObject.FindGameObjectsWithTag("Respawn");
        if (spawns.Length == 0)
            Debug.LogError("cannot find respawn objects");
	}

    private void releaseTentacle(GameObject gameObject) {
        checkList();
        Debug.Log("release tentacle "+gameObject.name);
        reservedTentacles.ForEach(match => Debug.Log(match.spawnObject.name));
        var id = reservedTentacles.FindIndex(match => match.gameObject == gameObject);
        if (id > -1) {
            Debug.Log("remove id: "+id);
            reservedTentacles.RemoveAt(id);
        }
        else
            Debug.Log("cannot find object " + gameObject.name);
    }

    public ReservedTentacle reserveTentacle(GameObject gameObject) {
        Debug.Log("reserve tentacle");
        checkList();

        releaseTentacle(gameObject);

        ReservedTentacle reserved = null;
        while (reserved == null) {
            int random=this.randomBehaviour.getNextRandom(11);
            if (reservedTentacles.Find(match => match.spawnObject == spawns[random]) == null) {
                Debug.Log("reserve slot "+random);
                reserved = new ReservedTentacle(spawns[random], gameObject);
                this.reservedTentacles.Add(reserved);
                Debug.Log("change parent to " + spawns[random].name);
                gameObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                gameObject.transform.SetParent(reserved.spawnObject.transform, false);
            }
        }

        return reserved;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
