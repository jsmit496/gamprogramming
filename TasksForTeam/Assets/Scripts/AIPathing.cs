using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private DestructableWall _myTargetWall;
    private CharacterStats _myTargetStats;
    private CoreStats _myTargetCoreStats;
    private float[] differences;
    private int numMats;
    private Vector3 rayCollisionNormal;
    private Vector3 hitLocationThisFram = Vector3.zero;
    private bool hitTarget = false;
    private bool setUpStuff = false;
    private bool targetDestroyed = false;
    private float timeWaitedBetweenAttacks;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject flareObj = GameObject.FindGameObjectWithTag("Flare");

        //Check Raycast if it hit target
        RaycastHit hitinfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, destructableWallLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetWall = hitinfo.collider.gameObject.GetComponent<DestructableWall>();
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, playerLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetStats = hitinfo.collider.gameObject.GetComponent<CharacterStats>();
        }
        else if (Physics.Raycast(transform.position, transform.forward, out hitinfo, rayDistance, baseCoreLayer.value))
        {
            hitLocationThisFram = hitinfo.point;
            rayCollisionNormal = hitinfo.normal;
            _myTargetCoreStats = hitinfo.collider.gameObject.GetComponent<CoreStats>();
        }
        else
        {
            _myTargetWall = null;
            _myTargetStats = null;
            _myTargetCoreStats = null;
        }

        if (_myTargetWall != null && _myTargetWall.gameObject.GetComponent<DestructableWall>().currHealth > 0)
        {
            _myTargetWall.currHealth -= damage;
        }
        if (flareObj != null)
        {
            if (flareObj.transform.position.magnitude - gameObject.transform.position.magnitude < viewRadius)
            {
                goToTarget = flareObj;
            }
        }
        //else if ()
        else
        {

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
