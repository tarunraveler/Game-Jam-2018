using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotManager : MonoBehaviour {

    public static RobotManager instance;

    public GameObject Laser;
    public GameObject startLaser;
    public int velocityLaser=15;
    public Animator gunAnimator;

    public float speed = 0f;
	public float rotationValue = 90f;
	public float rotationSpeed = 1f;

    public Transform collisionChecker;

	public ParticleSystem fire;

	private Rigidbody2D rb;
	private AudioSource[] source;

	private bool moving = false;
	private bool wasMoving = false;
	private bool rotating = false;
	public bool fly = false;

	private int rotationCounter = 0;
	private float actualRotationSpeed = 0;

    private int directionRobot;

	void Awake() {
		instance = this;
        directionRobot = 0;
	}

	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody2D> ();
		// source 1 for moving, source 2 for jump, such that both can be played overlapping
		source = gameObject.GetComponents<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
        
		if (moving) {
            RaycastHit2D raycast = Physics2D.Linecast(transform.position, collisionChecker.position, layerMask: 1 << LayerMask.NameToLayer("Obstacles"));
            if (raycast)
            {
                Stop();
                return;
            }
            rb.velocity = new Vector2 (transform.up.x, transform.up.y) * speed;
		}

		if (rotating) {
			if (rotationCounter * rotationSpeed >= rotationValue) {
				rotating = false;
				rotationCounter = 0;
				moving = wasMoving;
				gameObject.GetComponent<Animator>().SetBool("move", moving);
			} else {
				transform.Rotate (0, 0, actualRotationSpeed);
				rotationCounter += 1;
			}
		}
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Enter=" + collision);
        if (collision.tag.Equals("One") && rotating==false)
        {
            Stop();
            GameManager.instance.TakeDamage(1);
        }
        if (collision.tag.Equals("Three") && !fly)
        {
            Stop();
            GameManager.instance.TakeDamage(3);
        }
        if (collision.tag.Equals("margin"))
            Stop();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Stay="+collision);
        if (collision.tag.Equals("Three") && !fly)
        {
            Stop();
            GameManager.instance.TakeDamage(3);
        }
        if (collision.tag.Equals("margin"))
            Stop();
    }


    public void Go() {
        RaycastHit2D raycast = Physics2D.Linecast(transform.position, collisionChecker.position, layerMask: 1 << LayerMask.NameToLayer("Obstacles"));
        if (raycast) return;
        source [1].Play ();
        gameObject.GetComponent<Animator>().SetBool("move", true);
		moving = true;
	}

	public void Stop(bool stopAudio=true) {
		if(stopAudio) source [1].Stop ();
        gameObject.GetComponent<Animator>().SetBool("move", false);
        rb.velocity = Vector2.zero;
		moving = false;
	}

	public void RotateLeft() {
		if (!rotating) {
			wasMoving = moving;
			Stop (stopAudio:!moving);
			rotating = true;
			actualRotationSpeed = rotationSpeed;
            directionRobot--;
            if (directionRobot < 0)
                directionRobot = 3;
		}

	}

	public void RotateRigth() {
		if (!rotating) {
			wasMoving = moving;
			Stop (stopAudio: !moving);
			rotating = true;
			actualRotationSpeed = -rotationSpeed;
            directionRobot++;
            if (directionRobot > 3)
                directionRobot = 0;
		}
	}

    public void Fly()
    {
        if (fly == false)
        {
            source[0].Play();
            gameObject.GetComponent<Animator>().SetTrigger("fly");
        }
    }

    public void Shot()
    {
        gunAnimator.SetTrigger("shoot");
    }
		
    
    public void SpawnShot()
    {
         GameObject newLaser = (GameObject)Instantiate(Laser);
                newLaser.transform.position = new Vector2(startLaser.transform.position.x, startLaser.transform.position.y);
        switch (directionRobot)
        {
            case 0:
                newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, velocityLaser);
                break;
            case 1:
                newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityLaser, 0);
                break;
            case 2:
                newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocityLaser);
                break;
            case 3:
                newLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(-velocityLaser, 0);
                break;

        }
    }

	public void StartFire() {
		fire.Play ();
	}

	public void StopFire() {
		fire.Stop ();
	}
}
