using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PNJNetworkManager : MonoBehaviour {
    public bool FirstClient = true;

    // Use this for initialization
    void Start () {
        NetworkManager networkManager = GameObject.FindObjectOfType<NetworkManager>();
        NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
    }

    void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Client connected");
        if (FirstClient) {
            NetworkManager networkManager = GameObject.FindObjectOfType<NetworkManager>();
            GameObject trainer = null;      
            trainer = (GameObject)GameObject.Instantiate(networkManager.playerPrefab, transform.position, Quaternion.identity);
            trainer.name = "pnj";
            NetworkServer.Spawn(trainer);
            FirstClient = false;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void afterServerCreate() {
    }
}
