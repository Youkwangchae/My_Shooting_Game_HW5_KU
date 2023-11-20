using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBase : MonoBehaviour
{
    public enum baseState { Win, Lose};

    public baseState state;

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        
        if(other.tag == "Enemy_Bullet")
        {
            if(state == baseState.Win)
            {
                SceneManager.LoadScene("WinScene");
            }
            else
            {
                SceneManager.LoadScene("LoseScene");
            }
        }
    }
}
