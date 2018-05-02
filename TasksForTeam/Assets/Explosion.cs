using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public string[] collisionTargets;
    public string[] damageTargets;
    public float damage;
    public float waitToExplode;
    public float explosionForce = 0f;
    public float explosionRadius = 0f;
    public float explosionUpwardForce = 0f;

    public GameObject explosionObject;

    public bool startCountDown = false;
    public float timeWaited;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (startCountDown)
        {
            timeWaited += Time.deltaTime;
            if (timeWaited >= waitToExplode)
            {
                HandleExplosion();
            }
        }
	}

    public void HandleExplosion()
    {
        Vector3 explosionPosition = explosionObject.transform.position;
        Collider[] explosionColliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in explosionColliders)
        {
            Rigidbody objectHit = hit.GetComponent<Rigidbody>();
            CharacterStats objectHitStats = hit.GetComponent<CharacterStats>();
            if (objectHit != null)
            {
                objectHit.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionUpwardForce, ForceMode.Impulse);
            }
            if (objectHitStats != null)
            {
                objectHitStats.currHealth -= damage;
            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collisionTargets.Length; i++)
        {
            if (collision.collider.tag == collisionTargets[i])
            {
                startCountDown = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(explosionObject.transform.position, explosionRadius);
    }
}
