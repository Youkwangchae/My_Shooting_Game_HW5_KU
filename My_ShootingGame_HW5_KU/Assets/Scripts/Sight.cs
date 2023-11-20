using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float distance;
    public float angle;
    public LayerMask objectLayers;
    public LayerMask obstacleLayers;
    public Collider detectedObject;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distance);
    }

    private void Update()
    {
        // Distance Filter
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, distance, objectLayers);

        detectedObject = null;

        for(int i=0;i<colliders.Length;i++)
        {
            Collider collider = colliders[i];

            Vector3 directionToController = Vector3.Normalize(
                collider.bounds.center - transform.position);

            float angleToCollider = Vector3.Angle(
                transform.forward, directionToController);

            Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            Gizmos.DrawRay(transform.position, rightDirection * distance);

            Vector3 leftDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
            Gizmos.DrawRay(transform.position, leftDirection * distance);

            // Viewing Angle Filter
            if (angleToCollider < angle)
            {
                // Occulsion Filter
                if(!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, obstacleLayers))
                {
                    //Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedObject = collider;
                    break;
                }
                else
                {
                    //Debug.DrawLine(transform.position, collider.bounds.center, Color.red);
                }
            }
        }
    }
}
