using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialSceneManager : MonoBehaviour {

	
	[Header("Fade Effect")]
	public Texture texture;
	public float timeToFade = 1f;
	public float deadFadeTime = 1f;
    
	
	
	private float alpha;
	private bool fadingIn, fadingOut, keepBlack, deadFadeIn, deadFadeOut;
	public string message;
	public float typingDelay;
	
	public Text shownMessage;
	public GameObject panel;
	
	public float showTime;

	public GameObject objectToShow;
	
	private void OnGUI()
	{
		if (fadingIn || deadFadeIn)
		{
			keepBlack = false;
			alpha -= Mathf.Clamp01(Time.deltaTime / (deadFadeIn ? deadFadeTime : timeToFade));
			GUI.color = new Color(0, 0, 0, alpha);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), texture );
			if (alpha <= 0)
			{
				fadingIn = false;
				deadFadeIn = false;
			}
		}
        
		if (fadingOut || deadFadeOut)
		{
			alpha += Mathf.Clamp01(Time.deltaTime / (deadFadeOut ? deadFadeTime : timeToFade));
			GUI.color = new Color(0, 0, 0, alpha);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), texture );
			if (alpha >= 1)
			{
				deadFadeOut = false;
				fadingOut = false;
				keepBlack = true;
			}
		}

		if (keepBlack)
		{
			GUI.color = new Color(0, 0, 0, 1);
			GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), texture );
		}
	}
	// Use this for initialization
	void Start () {
        shownMessage.text = "";
        StartCoroutine(InitialScene());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator InitialScene()
	{
		if (typingDelay > 0)
		{
			foreach (char letter in message)
			{
				shownMessage.text += letter;
				yield return new WaitForSeconds(typingDelay);
			}
		}
		else
		{
			shownMessage.text += message;
		}
		yield return new WaitForSeconds(showTime);
		
		objectToShow.SetActive(true);
		gameObject.SetActive(false);
		
	}
}
