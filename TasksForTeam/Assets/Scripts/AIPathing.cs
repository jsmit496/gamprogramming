using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIPathing : MonoBehaviour
{
    public float speed = 4.0f;
    public float damage = 10;
    public int pathTargetToGo = 0;
    public GameObject goToTarget;
    public LayerMask destructableWallLayer;
    public LayerMask playerLayer;
    public LayerMask baseCoreLayer;
    public float rayDistance = 3.0f;
    public float viewRadius = 10;
    public float waitBetweenAttacks = 2;
    public bool isNight = false;
    public string flareInstantiatedObjName = "Flare(Clone)";
    public string baseCoreObjName = "Core";
    public string playerName = "Player";
    public GameObject navPoint;
    public GameObject defaultNavPoint;

    private DestructableWall _myTargetWall;
    private CharacterStats _myTargetStats;
    private CharacterStats _myTargetCoreStats;
    private float[] differences;
    private int numMats;
    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFram = Vector3.zero;
    private bool hitTarget = false;
    private bool setUpStuff = false;
    private bool targetDestroyed = false;
    private float timeWaitedBetweenAttacks;
    private bool canAttack = true;
    public bool canMove = true;
    NavMeshAgent agent;
    private GameObject flareObj;

    // Use this for initialization
    void Start()
    {
        timeWaitedBetweenAttacks = 0;
        agent = this.GetComponent<NavMeshAgent>();

        if (agent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();

        if (canAttack == false)
        {
            timeWaitedBetweenAttacks += Time.deltaTime;
            if (timeWaitedBetweenAttacks >= waitBetweenAttacks)
            {
                canAttack = true;
                timeWaitedBetweenAttacks = 0;
            }
        }
    }

    private void SetDestination()
    {
        GameObject flareObj = GameObject.Find(flareInstantiatedObjName);
        GameObject coreObj = GameObject.Find(baseCoreObjName);
        GameObject playerObj = GameObject.Find(playerName);
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, destructableWallLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetWall = hitinfo.collider.gameObject.GetComponent<DestructableWall>();
            if (_myTargetWall.currHealth > 0)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, playerLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetStats = hitinfo.collider.gameObject.GetComponent<CharacterStats>();
            if (_myTargetStats.currHealth > 0)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, baseCoreLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetCoreStats = hitinfo.collider.gameObject.GetComponent<CharacterStats>();
            if (_myTargetCoreStats.currHealth > 0)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }
        else
        {
            _myTargetWall = null;
            _myTargetStats = null;
            _myTargetCoreStats = null;
            agent.enabled = true;
        }

        float distanceFromPlayer = viewRadius * 2;
        float distanceFromFlare = viewRadius * 2;
        if (playerObj != null)
        {
            distanceFromPlayer = Vector3.Distance(playerObj.transform.position, transform.position);
            if (distanceFromPlayer < viewRadius)
            {
                canMove = true;
                agent.enabled = true;
            }
        }
        else if (flareObj != null)
        {
            distanceFromFlare = Vector3.Distance(flareObj.transform.position, transform.position);
            if (distanceFromFlare < viewRadius)
            {
                canMove = true;
                agent.enabled = true;
            }
        }
        else if (coreObj != null)
        {
            canMove = true;
            agent.enabled = true;
        }
        else
        {
            canMove = false;
        }

        if (_myTargetWall != null && _myTargetWall.gameObject.GetComponent<DestructableWall>().currHealth > 0)
        {
            if (canAttack == true)
            {
                _myTargetWall.currHealth -= damage;
                canAttack = false;
            }
        }
        if (_myTargetStats != null && _myTargetStats.currHealth > 0)
        {
            if (canAttack == true)
            {
                _myTargetStats.currHealth -= damage;
                canAttack = false;
            }
        }
        if (_myTargetCoreStats != null && _myTargetCoreStats.currHealth > 0)
        {
            if (canAttack == true)
            {
                _myTargetCoreStats.currHealth -= damage;
                canAttack = false;
            }
        }

        if (canMove == true)
        {
            agent.enabled = true;
            if (playerObj != null && distanceFromPlayer <= viewRadius)
            {
                agent.SetDestination(playerObj.transform.position);
            }
            else if (flareObj != null && distanceFromFlare <= viewRadius)
            {
                agent.SetDestination(flareObj.transform.position);
            }
            else if (coreObj != null)
            {
                agent.SetDestination(coreObj.transform.position);
            }
            else
            {
                agent.SetDestination(defaultNavPoint.transform.position);
            }
        }
        else if (canMove == false)
        {
            agent.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * rayDistance);

        //Check if target it by changing color
        if (hitTarget)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(hitLocationThisFram, hitLocationThisFram + rayCollisionNormal);
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
