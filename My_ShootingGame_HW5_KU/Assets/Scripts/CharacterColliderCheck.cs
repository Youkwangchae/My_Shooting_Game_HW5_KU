using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterColliderCheck : MonoBehaviour
{
    public UnityEvent onWin;
    public UnityEvent onLose;

    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider;
        if (other.tag == "Base")
        {
            if (other.name.Contains("Win"))
            {
                onWin.Invoke();
            }
            else
            {
                onLose.Invoke();
            }
        }
    }
}
