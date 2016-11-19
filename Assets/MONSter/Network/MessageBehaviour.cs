using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MessageBehaviour : MonoBehaviour {

    public class MsgTypes
    {
        public static short MSG_FIRE = 1000;
        public static short MSG_SCORE = 1005;
    };

    public class FireMessage: MessageBase {
        public uint playerId;

        public override void Deserialize(NetworkReader reader)
        {
            playerId = reader.ReadPackedUInt32();
        }

        // This method would be generated
        public override void Serialize(NetworkWriter writer)
        {
            writer.WritePackedUInt32(playerId);
        }
    }

    public void broadcastFire(uint playerId) {
        FireMessage fireMessage=new FireMessage();
        fireMessage.playerId = playerId;

        NetworkServer.SendToAll(MsgTypes.MSG_FIRE, fireMessage);
    }
    
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
