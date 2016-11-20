using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNJNetworkManager : NetworkManager {
    private int count = 0;
    public GameObject tentaclePrefab;
    public GameObject monkeyPrefab;

    public override void OnServerAddPlayer(NetworkConnection conn, short controllerID) {
        GameObject respawn = tentaclePrefab;
        Debug.Log("on server add player..." + controllerID);
        Debug.Log("player cout..." + controllerID);
        if (this.count == 0) {
            respawn = monkeyPrefab;
        }

        this.count++;

        GameObject trainer = null;
        trainer = (GameObject)GameObject.Instantiate(respawn, transform.position, Quaternion.identity);
        NetworkServer.Spawn(trainer);
    }
}
