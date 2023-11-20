using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriorScript : MonoBehaviour
{
    private int barriorCount = 10;


    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.tag == "Enemy_Bullet")
        {
            barriorCount--;

            if(barriorCount == 0)
            gameObject.SetActive(false);

            other.gameObject.SetActive(false);
        }              
    }
}
