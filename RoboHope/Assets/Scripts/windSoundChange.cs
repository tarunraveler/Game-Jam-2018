using System.Collections;
using UnityEngine;

public class windSoundChange : MonoBehaviour
{
	public AudioClip windBegin;
	public AudioClip windLoop;
	public AudioClip background;
	
	public AudioSource _AudioSource;
	// Use this for initialization
	void Start ()
	{
		_AudioSource = GetComponent<AudioSource>();
		_AudioSource.PlayOneShot(windBegin);
		AudioSource s = gameObject.AddComponent<AudioSource>();
		s.loop = true;
		s.clip = background;
		s.Play();
		StartCoroutine(PlayWind());
	}

	IEnumerator PlayWind()
	{
		yield return new WaitForSeconds(4);
		_AudioSource.clip = windLoop;
		_AudioSource.loop = true;
		_AudioSource.Play();
	}
}
