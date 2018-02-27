using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
	public Text shownMessage;
	public GameObject panel;
	public float typingDelay;


	public string initialMessage;
	public string[] messages;
	private int actual;
	public float showTime;

	public static DialogueManager instance;

	private int writers = 0;
	
	void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (instance != this)
			Destroy(gameObject);
	}

	
	// Use this for initialization
	void Start ()
	{
		panel.SetActive(true);
		shownMessage.text = initialMessage;
		actual = -1;
	}

	public void ShowNext()
	{
		shownMessage.text = "";
		actual += 1;
		if (actual < messages.Length)
		{
			StartCoroutine(ShowText(messages[actual]));
		}
	}
	
	public void Show(int position)
	{
		panel.SetActive(true);
		shownMessage.text = "";
		if (position < messages.Length)
		{
			StartCoroutine(ShowText(messages[position]));
		}
	}
	
	private IEnumerator ShowText(string text)
	{
		writers++;
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
		yield return new WaitForSeconds(showTime);
		if(writers == 1){
			shownMessage.text = "";
			panel.SetActive(false);
		}
		writers--;
	}

}
