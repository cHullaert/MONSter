﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MonsterBehaviour : NetworkBehaviour {
    public uint playerId; 
    private float sumDeltaTime;
    private Animator animator;
    private List<float> timestamps = new List<float>();
    public IABehaviour.PNJ pnj = null;

    static float maxWaitingTime = 8.0f;
    static float maxIdleTime = 8.0f;
    static int idleState = Animator.StringToHash("Base Layer.Idle");
    static int waitingState = Animator.StringToHash("Base Layer.Waiting");
    static int hitState = Animator.StringToHash("Base Layer.Hit");
    private int ObjectState = waitingState;

    private void onServerFire(NetworkMessage netMsg) {
        var fm = netMsg.ReadMessage<MessageBehaviour.FireMessage>();
        Debug.Log("firemessage for " + fm.playerId);
        if (fm.playerId == playerId) {
            this.animator.ResetTrigger("Hit");
            this.animator.SetTrigger("Fire");
        }
    }

    // Use this for initialization
    void Start () {
        if (gameObject.tag == "pnj") {
            //Debug.Log("pnj detected");
            gameObject.GetComponent<MonsterBehaviour>().pnj = new IABehaviour.PNJ(this.GetComponent<RandomBehaviour>());
        }

        animator = gameObject.GetComponent<Animator>();

        if(!isServer)
            ClientScene.readyConnection.RegisterHandler(MessageBehaviour.MsgTypes.MSG_FIRE, onServerFire);

    }

    public void doHit() {
        this.gameObject.GetComponent<AudioSource>().Play();
        this.animator.SetTrigger("Hit");
    }

    private void doFire() {
        if (!isServer)
            return;

        //Debug.Log("inside");
        this.animator.ResetTrigger("Hit");
        this.animator.SetTrigger("Fire");

        MessageBehaviour behaviour =GameObject.FindObjectOfType<MessageBehaviour>();
        behaviour.broadcastFire(this.playerId);    
    }

    private void onFire() {
    }

    private void onIdle() {
        sumDeltaTime += Time.deltaTime;
        if (sumDeltaTime > maxIdleTime) {
            this.sumDeltaTime = maxIdleTime;
            doFire();
        }
    }

    private void onNewState(int state) {
        //Debug.Log("on new state");
        if (state == hitState) {
            this.sumDeltaTime = 0.0f;
        }
        else if (state == waitingState) {
            GameBehaviour behaviour=GameObject.FindObjectOfType<GameBehaviour>();
            behaviour.reserveTentacle(gameObject);

            if(this.sumDeltaTime>0.0)
                timestamps.Add(this.sumDeltaTime);
            this.sumDeltaTime = 0.0f;
        }
        else if (state == idleState) {
            this.sumDeltaTime = 0.0f;
        }

        ObjectState = state;
    }

    private void onWaiting() {
        sumDeltaTime += Time.deltaTime;
        if (sumDeltaTime > maxWaitingTime)
        {
            this.sumDeltaTime = 0.0f;
            doFire();
        }
    }

    private void dump(int state) {
        /* if (state == waitingState)
            Debug.Log("waiting state");
        else if (state == idleState)
            Debug.Log("idle state");
        else if (state == hitState)
            Debug.Log("hit state");
        else
            Debug.Log("other stsate");*/
    }

    // Update is called once per frame
    void Update() {
        if (!isServer)
            return;

        var currentState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        if (ObjectState != currentState) {
            onNewState(currentState);
        }

        if (currentState == idleState) {
            this.onIdle();
        }
        else if (currentState == waitingState) {
            this.onWaiting();
        }

        if (this.pnj != null)
        {
            if (currentState == waitingState)
            {
                if (this.sumDeltaTime >= pnj.startTime)
                {
                    doFire();
                }
            }
            else if (currentState == idleState)
            {
                if (this.sumDeltaTime >= pnj.length)
                {
                    doFire();
                    this.pnj.next();
                }
            }
        }
        else {
           /* if (animator != null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    doFire();
                }
            }*/
        }

    }
}
