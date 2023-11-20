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

        if (state == baseState.Win)
        {
            if (other.tag == "Bullet")
            {
                SceneManager.LoadScene("WinScene");
            }
        }
        else
        {
            if (other.tag == "Enemy_Bullet")
            {
                SceneManager.LoadScene("LoseScene");
            }
        }        
    }
}
