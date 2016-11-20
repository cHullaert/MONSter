using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TriggerBehaviour : NetworkBehaviour {

    private GameObject FindParentByTag(GameObject gameObject, string tag) {
        //Debug.Log(gameObject.tag);
        if (gameObject.tag.Equals(tag))
        {
            return gameObject;
        }
        else if (gameObject.transform.parent != null)
        {
            return FindParentByTag(gameObject.transform.parent.gameObject, tag);
        }
        else
            return null;
    }

    void OnTriggerEnter(Collider other)
    {
       // Debug.Log("collision: " + gameObject.name);
        GameObject tentacle =FindParentByTag(gameObject, "tentacle");
        if (tentacle == null) {
            Debug.LogError("no tentacle found");
            return;
        }
        MonsterBehaviour behaviour = tentacle.GetComponent<MonsterBehaviour>();
        if (behaviour == null) {
            Debug.LogError("no behaviour found");
            return;
        }

        behaviour.doHit();

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
