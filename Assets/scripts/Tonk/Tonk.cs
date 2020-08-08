using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonk : MonoBehaviour
{
    public float speed;
    public float rotationspeed;
    public float missilespeed;
    public GameObject body;
    public Rigidbody rb;
    public GameObject Missile;
    public GameObject MisseleSpawn;
    public List<GameObject> bullets = new List<GameObject>();
    public bool[] landed = new bool[2];
    private bool Running;
    public void SetRunState(bool state)
    {
        Running = state;
    }
    public bool GetRunState()
    {
        return Running;
    }
    private void Awake()
    {
        landed = new bool[2];
    }
    private void Start()
    {
        Running = true;
        rb = GetComponent<Rigidbody>();
    }
    private void OnCollisionStay(Collision other)
    {
        Collider myCollider = other.contacts[0].thisCollider;
        if ((myCollider.gameObject.name == "RightTrack"))
            landed[0] = true;

        if ((myCollider.gameObject.name == "RightTrack"))
            landed[1] = true;
    }
    private void OnCollisionExit(Collision other)
    {
        landed[0] = false;
        landed[1] = false;
    }
    private void Update()
    {
        if (Running)
        {
            Controls();
        }
    }
    private void Controls()
    {
        if (landed[0] && landed[1])
        {
            Vector3 Force = transform.forward * speed;
            if (Input.GetKey(KeyCode.Z))
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(Force);
            }
            if (Input.GetKey(KeyCode.S))
            {
                rb.velocity = new Vector3(0, 0, 0);
                rb.AddForce(-Force);
            }
            if (Input.GetKey(KeyCode.Q)) transform.Rotate(new Vector3(0, -rotationspeed * Time.deltaTime, 0));
            if (Input.GetKey(KeyCode.D)) transform.Rotate(new Vector3(0, rotationspeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 pos = MisseleSpawn.transform.position;
            Quaternion dir = MisseleSpawn.transform.rotation;
            GameObject GO = Instantiate(Missile, pos, dir);
            GO.GetComponent<Rigidbody>().velocity = GO.transform.forward * missilespeed;
            bullets.Add(GO);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Missiles mis;
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i] != null)
                {
                    mis = bullets[i].GetComponent<Missiles>();
                    mis.Detonate();
                }
            }
        }
        for (int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i] == null)
            {
                bullets.Remove(bullets[i]);
            }
        }
    }
}
