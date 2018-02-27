using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour {
    public float distanceSound = 4;
    public GameObject player;
    public GameObject A;
    public GameObject B;
    public float velocity = 2;

    public int direction = 1;
    private AudioSource audio;
    private bool isPlaying;

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity,0);
        audio = gameObject.GetComponent<AudioSource>();
        audio.Stop();
        isPlaying = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.x > B.transform.position.x && direction==1)
        {
            direction *= -1;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * velocity,0);
        }
        if (gameObject.transform.position.x < A.transform.position.x && direction ==-1)
        {
            direction *= -1;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction * velocity,0);
        }
        if (isPlaying == false && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) < distanceSound)
        {
            isPlaying = true;
            audio.Play();
        }
        if (isPlaying == true && Mathf.Abs(gameObject.transform.position.y - player.transform.position.y) > distanceSound)
        {
            isPlaying = false;
            audio.Stop();
        }
    }

	public void AnimDestroy() {
		Destroy(gameObject);
	}
}
