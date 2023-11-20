using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { GoToBase, AttackBase, ChasePlayer, AttackPlayer};

    public EnemyState currentState;

    public Sight sightSensor;

    public Transform baseTransform;
    public float baseAttackDistance;

    public float shootSpeed;

    public float playerAttackDistance;
    public NavMeshAgent agent = null;

    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;

    public Transform bulletSpawnTrans;

    void Shoot(Transform target)
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if(timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;            

            GameObject _bullet = Instantiate(bulletPrefab, transform.parent.position + new Vector3(20, 10, 20), transform.parent.rotation);

            //_bullet.transform.SetParent(transform.parent);

            Vector3 directionToTarget = target.position - transform.position;
            _bullet.transform.GetComponent<Rigidbody>().AddForce(directionToTarget.normalized * shootSpeed, ForceMode.Impulse);

            Destroy(_bullet, fireRate);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;

        transform.parent.forward = directionToPosition;
    }

    private void Awake()
    {
        if(!baseTransform)
        baseTransform = GameObject.Find("Main Base Cube - Lose").transform;

        if(!agent)
        agent = GetComponentInParent<NavMeshAgent>();

        if (!agent.enabled && agent.gameObject.transform.localScale.x > 0)
            agent.enabled = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, baseAttackDistance);
    }

    void GoToBase() 
    {
        if (!agent)
            agent = GetComponentInParent<NavMeshAgent>();

        if (!agent.enabled && agent.gameObject.transform.localScale.x > 0)
            agent.enabled = true;

        if(agent && agent.enabled)
        {
            agent.isStopped = false;
            agent.SetDestination(baseTransform.position);
        }

        if(sightSensor.detectedObject != null)
        {
            currentState = EnemyState.ChasePlayer;
        }

        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);

        //Debug.Log(distanceToBase);
        if(distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }
    }

    void AttackBase() 
    {
        Debug.Log("AttackBase");
        if (!agent)
            agent = GetComponentInParent<NavMeshAgent>();

        if (!agent.enabled && agent.gameObject.transform.localScale.x > 0)
            agent.enabled = true;

        if (agent && agent.enabled)
        {
            agent.isStopped = true;
            agent.speed = 0;
        }

        agent.speed = 0;

        LookTo(baseTransform.position);
        Shoot(baseTransform);
    }

    void ChasePlayer()
    {
        if (!agent)
            agent = GetComponentInParent<NavMeshAgent>();

        if (!agent.enabled && agent.gameObject.transform.localScale.x > 0)
            agent.enabled = true;

        if (agent && agent.enabled)
            agent.isStopped = false;

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        if (agent && agent.enabled)
            agent.SetDestination(sightSensor.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if(distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }
    }

    void AttackPlayer() 
    {
        Debug.Log("AttackPlayer");
        if (!agent)
            agent = GetComponentInParent<NavMeshAgent>();

        if (!agent.enabled && agent.gameObject.transform.localScale.x > 0)
            agent.enabled = true;

        if (agent && agent.enabled)
        {            
            agent.isStopped = true;
        }

        agent.speed = 0;

        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot(sightSensor.detectedObject.transform);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
            currentState = EnemyState.ChasePlayer;
    }
    
    private void Update()
    {
        if (currentState == EnemyState.GoToBase) { GoToBase(); }
        else if (currentState == EnemyState.AttackBase) { AttackBase(); }
        else if (currentState == EnemyState.ChasePlayer) { ChasePlayer(); }
        else { AttackPlayer(); }
    }

}
