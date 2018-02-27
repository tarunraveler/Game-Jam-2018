using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinalSceneManager : MonoBehaviour
{
	public Text shownMessage;
	public GameObject panel;
	public float typingDelay;


	public string[] messages;
	public float[] waitTimes;
	private int actual;
	public float delay;

	private bool writing = false;

	
	[Header("Fade Effect")]
	public Texture texture;
	public float timeToFade = 1f;
	public float deadFadeTime = 1f;
    
	private float alpha;
	private bool fadingIn, fadingOut, keepBlack, deadFadeIn, deadFadeOut;
	
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
	void Start ()
	{
		shownMessage.text = "";
		panel.SetActive(true);
		StartCoroutine(ShowConsecutive());
	}

	
	private IEnumerator ShowConsecutive()
	{
		fadingIn = true;
		alpha = 1;
		yield return new WaitForSeconds(timeToFade);
		yield return new WaitForSeconds(delay);
		for (int i = 0; i < messages.Length; i++)
		{
			string text = messages[i];
			if (typingDelay > 0)
			{
				foreach (char letter in text)
				{
					shownMessage.text += letter;
					yield return new WaitForSeconds(typingDelay);
				}
			}
			else
			{
				shownMessage.text += text;
			}
			yield return new WaitForSeconds(waitTimes[i]);
			
			if(i!=messages.Length-1)
				shownMessage.text = "";

		}
		
		fadingOut = true;
		alpha = 0;
		yield return new WaitForSeconds(timeToFade);
		
		SceneManager.LoadScene("Angelo");

	}

}
