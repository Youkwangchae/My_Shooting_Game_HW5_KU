using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    private CapsuleCollider _collider;

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.tag == "Bullet")
        {
            // ��ƼŬ ����
            StartCoroutine(bullet_Particle(other.gameObject.transform));

            // ĳ���Ϳ��� �ǰ� ����.
            Destroy(gameObject);
        }
    }

    IEnumerator bullet_Particle(Transform other)
    {
        other.GetChild(0).GetChild(0).gameObject.SetActive(true);
        other.GetChild(0).GetChild(1).gameObject.SetActive(true);
        other.GetChild(0).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);

        other.GetChild(0).GetChild(0).gameObject.SetActive(false);
        other.GetChild(0).GetChild(1).gameObject.SetActive(false);
        other.GetChild(0).GetChild(2).gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (!_collider)
            _collider = GetComponent<CapsuleCollider>();
    }
}
