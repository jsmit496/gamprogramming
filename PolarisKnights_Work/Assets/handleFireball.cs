using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handleFireball : MonoBehaviour
{
    public string[] targetsCanHitTag;
    public bool canExplode = false; //set if the object explodes or not
    public float explosionForce = 0f;
    public float explosionRadius = 0f;
    public float explosionUpwardForce = 0f;
    public float damage;

    public GameObject explosionObject;
    public ParticleSystem explosionParticle;

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Explosion()
    {
        Vector3 explosionPosition = explosionObject.transform.position;
        Collider[] explosionColliders = Physics.OverlapSphere(explosionPosition, explosionRadius);
        foreach (Collider hit in explosionColliders)
        {
            Rigidbody objectHit = hit.GetComponent<Rigidbody>();
            TestHealth objectHealth = hit.GetComponent<TestHealth>();
            if (objectHit != null)
            {
                objectHit.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, explosionUpwardForce, ForceMode.Impulse);
            }
            if (objectHealth != null)
            {
                objectHealth.currHealth -= damage;
            }
        }
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < targetsCanHitTag.Length; i++)
        {
            if (collision.collider.tag == targetsCanHitTag[i])
            {
                collision.collider.GetComponent<TestHealth>().currHealth -= 10;
                if (canExplode)
                {
                    Explosion();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(explosionObject.transform.position, explosionRadius);
    }
}
