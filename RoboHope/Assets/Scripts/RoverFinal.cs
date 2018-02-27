using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoverFinal : MonoBehaviour {

	public GameObject signal;
	public string endSceneName;
	public float loadFinalDelay;
	// Use this for initialization
	public void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player")
		{
			RobotManager.instance.Stop ();
			Debug.Log ("SIGNAL");
			signal.GetComponent<Animator> ().SetTrigger ("send");
			StartCoroutine(loadFinalScene());
		}

	}

	private IEnumerator loadFinalScene() {
		yield return new WaitForSeconds (loadFinalDelay);
		SceneManager.LoadScene (endSceneName);
	}
}
