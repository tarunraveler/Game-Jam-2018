using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private float initialPositionY;
    private float initialPositionX;
    private AudioSource audio;

	// Use this for initialization
	void Start () {
        initialPositionY = transform.position.y;
        initialPositionX = transform.position.x;
        audio = gameObject.GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(transform.position.y - initialPositionY) > 50 || Mathf.Abs(transform.position.x - initialPositionX) > 50)
            Destroy(gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals("Alien"))
        {
            audio.Play();
			collision.gameObject.GetComponent<Animator> ().SetTrigger ("dead");
            //Destroy(collision.gameObject);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine( Delay(collision));
        }
    }

    public IEnumerator Delay(Collider2D collision)
    {
        Debug.Log("start");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("stop");
        Destroy(gameObject);
    }
}
