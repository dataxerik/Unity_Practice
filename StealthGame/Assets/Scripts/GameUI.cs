using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    public GameObject LoseUI;
    public GameObject WinUI;

    public Player player;

    bool isGameOver;
	// Use this for initialization
	void Start () {
        Guard.OnGuardHasSpotted += showLoseGameUI;
        player = FindObjectOfType<Player>();

        player.OnReachEndOfLevel += showWinGameUI;
	}
	
	// Update is called once per frame
	void Update () {
        if(isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
		
	}

    void showLoseGameUI()
    {
        OnGameOver(LoseUI);
    }

    void showWinGameUI()
    {
        OnGameOver(WinUI);
    }

    void OnGameOver(GameObject UI)
    {
        UI.SetActive(true);
        isGameOver = true;
        Guard.OnGuardHasSpotted -= showLoseGameUI;
        player.OnReachEndOfLevel -= showWinGameUI;
    }
}
