using UnityEngine;
using System.Collections;

public class LookatCam : MonoBehaviour
{
    public Camera cam;
    void Start()
    {
        cam = GameObject.Find("Player").GetComponentInChildren<Camera>();
    }
    void Update()
    {
        if (cam != null)
        {
            transform.LookAt(cam.transform.position);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 180, transform.localEulerAngles.z);
        }
    }
}
