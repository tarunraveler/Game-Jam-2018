using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public Packet packet;
	public Text text;
    public AudioClip AudioButton0;
    public AudioClip AudioButton1;
    public float delay = 1f;

    private AudioSource audio;
	private int counter;
	private string command;

    private bool wait;

	// Use this for initialization
	void Start () {
		counter = 0;
		command = "";
        audio = gameObject.GetComponent<AudioSource>();
		text.text = "";
        wait = false;
	}
    
	
	// Update is called once per frame
	void Update () {
        if (wait==false)
        {
            if (Input.GetButtonDown("One"))
            {
                audio.PlayOneShot(AudioButton1);
                command += "1";
                counter += 1;
                text.text = command;
                Debug.Log(command);
            }
            else if (Input.GetButtonDown("Zero"))
            {
                audio.PlayOneShot(AudioButton0);
                command += "0";
                counter += 1;
                text.text = command;
                Debug.Log(command);
            }

            if (counter == 3)
            {
                StartCoroutine(Transmit(command));
				packet.SendPacket(delay, command);
                counter = 0;
                command = "";
                wait = true;
                text.text = command;
                StartCoroutine(NotWait());
            }
        }
	}

    private IEnumerator NotWait()
    {
        yield return new WaitForSeconds(delay);
        wait = false;
    }

	private IEnumerator Transmit(string command) {
		yield return new WaitForSeconds (delay);
		Debug.Log ("Transmitted " + command);
		switch (command) {
		case "000":
			RobotManager.instance.Stop ();
			Debug.Log ("Transmitted Stop");
			break;
		case "111":
			RobotManager.instance.Go();
			Debug.Log ("Transmitted Go");
			break;
		case "010":
			RobotManager.instance.RotateLeft ();
			Debug.Log ("Transmitted Rotate Left");
			break;
		case "101":
			RobotManager.instance.RotateRigth ();
			Debug.Log ("Transmitted Rotare Right");
			break;
        case "100":
            RobotManager.instance.Fly();
            Debug.Log("Transmitted Fly");
            break;
        case "001":
            RobotManager.instance.Shot();
            Debug.Log("Transmitted Shot");
            break;
        default:
			Debug.Log ("Error!");
			break;
		}
	}
}
