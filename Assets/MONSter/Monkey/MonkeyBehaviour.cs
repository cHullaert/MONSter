using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using XInputDotNetPure;

public class MonkeyBehaviour : NetworkBehaviour
{
    bool playerIndexSet = false;
    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;
    Animator animator;
    private List<float> timestamps = new List<float>();

    // Use this for initialization
    void Start () {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        // Find a PlayerIndex, for a single player game
        // Will find the first controller that is connected ans use it
        if (!playerIndexSet || !prevState.IsConnected)
        {
            for (int i = 0; i < 4; ++i)
            {
                PlayerIndex testPlayerIndex = (PlayerIndex)i;
                GamePadState testState = GamePad.GetState(testPlayerIndex);
                if (testState.IsConnected)
                {
                    Debug.Log(string.Format("Monkey Game found {0}", testPlayerIndex));
                    playerIndex = testPlayerIndex;
                    playerIndexSet = true;
                }
            }
        }

        prevState = state;
        state = GamePad.GetState(playerIndex);

        var angle = state.ThumbSticks.Right.X * 50.0f * Time.deltaTime;
        transform.localRotation *= Quaternion.Euler(0.0f, angle, 0.0f);
        var x = Time.deltaTime * 40 * state.ThumbSticks.Left.X + gameObject.transform.position.x;
        var z = Time.deltaTime * 40 * state.ThumbSticks.Left.Y + gameObject.transform.position.z;

        //var tmp = x;
        //var sinVal = Mathf.Sin(angle);
        //var cosVal = Mathf.Cos(angle);

        //x = tmp * cosVal + z * sinVal;
        //z = -tmp * sinVal + z * cosVal;
        if ((prevState.Buttons.A == ButtonState.Pressed) && (state.Buttons.A == ButtonState.Released)) {
            this.animator.SetTrigger("Hit");
            this.gameObject.GetComponent<AudioSource>().PlayDelayed(0.5f);
        }

        transform.position = new Vector3(x, 0.3f, z);
        //gameObject.transform.Rotate(0, state.ThumbSticks.Right.X * 40.0f * Time.deltaTime, 0.0f, Space.World); 


        /* prevState = state;
        state = GamePad.GetState(playerIndex);

        // Detect if a button was pressed this frame
        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
        }
        // Detect if a button was released this frame
        if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
        {
            GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        // Set vibration according to triggers
        GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

        // Make the current object turn
        transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);*/
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, Screen.width, Screen.height), string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y));
        /* string text = "Use left stick to turn the cube, hold A to change color\n";
         text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
         text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
         text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
         text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
         text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
         text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
         text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
         GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);*/
    }
}
