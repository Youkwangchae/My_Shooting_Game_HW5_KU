using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void GoToWin()
    {
        SceneManager.LoadScene("WinScene");
    }

    public void GoToLose()
    {
        SceneManager.LoadScene("LoseScene");
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("GameManger ม฿บน.");
        }

        var colliderCheck = FindObjectOfType<CharacterColliderCheck>();
        colliderCheck.onWin.AddListener(GoToWin);
        colliderCheck.onLose.AddListener(GoToLose);
    }
}
