using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Packet : MonoBehaviour {

    public AudioClip sendMessage;
    public AudioClip receiveMessage;
	public AudioClip error;
    private AudioSource audio;

    public RectTransform startPos;
    public RectTransform endPos;
    
    private float startTime;
    private float journeyLength = 1;

    private Image image;
    private RectTransform _rectTransform;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        image.enabled = false;
        audio = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (image.enabled)
        {
            if (Mathf.Abs(_rectTransform.position.x - endPos.position.x) < 1)
            {
                audio.PlayOneShot(receiveMessage);
                image.enabled = false;
                return;
            }
            float distCovered = Time.time - startTime;
            float fracJourney = distCovered / journeyLength;
            _rectTransform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
        }

        
    }

	public void SendPacket(float delay, string command)
    {
		if (command == "110" || command == "011") {
			audio.PlayOneShot (error);
			Debug.Log ("ERROR");
		} else {
			journeyLength = delay;
			startTime = Time.time;
			image.enabled = true;
			_rectTransform.position = startPos.position;
			audio.PlayOneShot (sendMessage);
		}
    }
}
