using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour {

    public GameObject Game;
    public GameObject MainMenu;
	// Use this for initialization
	void Start () {
        Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(BeforeGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeforeGame()
    {
        Game.SetActive(true);
        MainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                         Application.Quit();
        #endif
    }
}
