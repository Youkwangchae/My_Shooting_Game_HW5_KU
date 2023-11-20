using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarriorScript : MonoBehaviour
{
    public enum barriorState { enemy, Mine};
    public barriorState state;

    private int barriorCount = 10;


    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;

        if(state == barriorState.enemy)
        {
            if (other.tag == "Bullet")
            {
                barriorCount--;

                if (barriorCount == 0)
                    gameObject.SetActive(false);

                //other.gameObject.SetActive(false);

                // 파티클 등장
                StartCoroutine(bullet_Particle(other.gameObject.transform));
            }
        }
        else
        {
            if (other.tag == "Enemy_Bullet")
            {
                barriorCount--;

                if (barriorCount == 0)
                    gameObject.SetActive(false);

                //other.gameObject.SetActive(false);

                // 파티클 등장
                StartCoroutine(bullet_Particle(other.gameObject.transform));
            }
        }           
    }

    IEnumerator bullet_Particle(Transform other)
    {
        Debug.Log("1");
        other.GetChild(0).GetChild(0).gameObject.SetActive(true);
        other.GetChild(0).GetChild(1).gameObject.SetActive(true);
        other.GetChild(0).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        other.GetChild(0).GetChild(0).gameObject.SetActive(false);
        other.GetChild(0).GetChild(1).gameObject.SetActive(false);
        other.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }
}
