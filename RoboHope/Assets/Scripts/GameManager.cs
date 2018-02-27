using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public bool GamePaused { get; set; }

    [Header("Fade Effect")]
    public Texture texture;
    public float timeToFade = 1f;
    public float deadFadeTime = 1f;
    
    private float alpha;
    private bool fadingIn, fadingOut, keepBlack, deadFadeIn, deadFadeOut;

    private Vector3 spawnPoint;
    
	public GameObject player;
    public int fullLife;

    public GameObject[] hearts;
    
    public int life;
    public int Life
    {
        get { return life; }
        set
        {
            life = value;
            for (int i = 0; i < hearts.Length; i++)
            {
                if(i<value)
                    hearts[i].SetActive(true);
                else
                    hearts[i].SetActive(false);
            }
            if(value<=0)
                StartCoroutine(Respawn());
        }
    }

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

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        fadingIn = true;
        alpha = 1;
    }

    public void TakeDamage(int damage)
    {
        Life -= damage;
        if (Life <= 0)
            StartCoroutine(Respawn());
    }
    
    public void ChangeSpawnPoint(Vector3 spawn)
    {
        spawnPoint = spawn;
    }

    IEnumerator Respawn()
    {
        GamePaused = true;
        fadingOut = true;
        alpha = 0;
        yield return new WaitForSeconds(timeToFade);
        Life = fullLife;
        player.transform.position = spawnPoint;
        ResumeGame();
        fadingIn = true;
        alpha = 1;
        

    }

    
    public GameObject GetPlayer()
    {
        return player;
    }

    public void PauseGame() { 
    	GamePaused = true;
    	Time.timeScale = 0;
    }

    public void ResumeGame() {
        GamePaused = false;
        Time.timeScale = 1;
    }

    public void GameEnd()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }


    public void NewGame()
    { 
		//TODO        
    }

    IEnumerator LoadNext()
    {
        GamePaused = true;
        fadingOut = true;
        alpha = 0;
        yield return new WaitForSeconds(timeToFade);

        	// TODO: do stuff

        ResumeGame();
        //player.SetActive(true);
        fadingIn = true;
        alpha = 1;
        
    }
}
