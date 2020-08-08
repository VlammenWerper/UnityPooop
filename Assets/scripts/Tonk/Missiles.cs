using UnityEngine;
using System.Collections;

public class Missiles : MonoBehaviour
{
    public GameObject Explosion;
    public float explosionforce;
    public float radius;
    public float Upforce;
    public float effectTime = 1.8f;
    void OnCollisionEnter()//Collider col)
    {
        Detonate();
    }
    public void Detonate()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        GameObject GO = Instantiate(Explosion, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, radius);
        for (int i = 0; i < colliders.Length; i++)
        {

            Rigidbody rbc = colliders[i].GetComponent<Rigidbody>();
            if (rbc == null) rbc = colliders[i].GetComponentInParent<Rigidbody>();

            if (rbc == null) rbc = colliders[i].GetComponentInChildren<Rigidbody>();

            if (rbc != null)
            {
                rbc.AddExplosionForce(explosionforce, this.transform.position, radius);
            }
        }
        Destroy(gameObject);
        Destroy(GO, effectTime);
    }
}
