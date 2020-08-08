using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public GameObject Cannoen;
    public GameObject Slope;

    public Rigidbody rb;
    public GameObject Missile;
    public GameObject MisseleSpawn;
    public float Maxdistance;
    public float EnemySpeed;
    public float missilespeed;

    public float lastSHOT;
    public float delay;
    public float extraHeight;
    bool landed = true;
    public GameObject Target;
    public Healt EnemyHealt;
    private void Update()
    {
        if(EnemyHealt != null)
            if(EnemyHealt.healt <= 0) { Destroy(this.gameObject);}
    }

    void OnTriggerStay(Collider col)
    {
        Vector3 Force = transform.forward * EnemySpeed;
        //GameObject Target = col.gameObject.GetComponent<GameObject>();
        Target = col.gameObject.transform.root.gameObject;
        if (Target.tag == "Player")
        {
            float x = Mathf.Abs(transform.position.x - Target.transform.position.x);
            float y = Mathf.Abs(transform.position.y - Target.transform.position.y);
            float z = Mathf.Abs(transform.position.z - Target.transform.position.z);
            float distance = (Mathf.Sqrt((x * x) + (y * y)));
            distance = Mathf.Sqrt((distance * distance) + (z * z));
            extraHeight = distance/(missilespeed/1.5f);
            Cannoen.transform.LookAt(new Vector3(Target.transform.position.x, Cannoen.transform.position.y,Target.transform.position.z));
            Vector3 Height = new Vector3(Target.transform.position.x, Target.transform.position.y + extraHeight, Target.transform.position.z);
            Slope.transform.LookAt(Height);

            if (landed)
            {
                if (distance > Maxdistance)
                {
                    transform.LookAt(Target.transform.position);
                    {
                        rb.velocity = new Vector3(0, 0, 0);
                        rb.AddForce(Force);
                    }
                }
            }
            lastSHOT += Time.deltaTime;
            RaycastHit hit;
            if (Physics.Raycast(MisseleSpawn.transform.position, Vector3.forward, out hit))
            {
                if (hit.collider.gameObject.tag != "")
                {

                }
            }

  
            if (lastSHOT > delay)
            {
                Vector3 pos = MisseleSpawn.transform.position;
                Quaternion dir = MisseleSpawn.transform.rotation;
                GameObject GO = Instantiate(Missile, pos, dir);
                GO.GetComponent<Rigidbody>().velocity = (GO.transform.forward * missilespeed);
                lastSHOT = 0;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "terrain")
        {
            //landed = true;
        }
    }
    void OnCollisionExit()
    {
        //landed = false;
    }
}
