using UnityEngine;
using System.Collections;

public class CameraTonk : MonoBehaviour
{
    public GameObject Cannoen;
    public GameObject Slope;

    public GameObject tonk;
    private Tonk tonkscripts;

    private bool grounded;
    private bool ThirdPersonV;
    public float Sensitivity = 1;
    public float Max;
    public float Min;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        tonkscripts = tonk.GetComponent<Tonk>();

    }
    void Update()
    {
        if (tonkscripts.GetRunState())
        {
            if (Time.timeScale >= 1)
                control();
            if (Time.timeScale == 0)
                Cursor.lockState = CursorLockMode.None;
        }
    }
    private void control()
    {
        Cursor.lockState = CursorLockMode.Locked;
        float newRotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * Sensitivity;
        float newRotationY = transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * Sensitivity;

        float difx = Mathf.Sin(Mathf.Deg2Rad * (Slope.transform.localEulerAngles.y + 90));
        float difz = Mathf.Cos(Mathf.Deg2Rad * (Slope.transform.localEulerAngles.y + 90));

        if ((tonkscripts.landed[0] != true)&&(tonkscripts.landed[1] != true))
        {       
            if (newRotationY > 359)
            {
                tonk.transform.localEulerAngles += new Vector3(difx, 0, difz);
            }
        }
        if (newRotationY < 271)
        {
            tonk.transform.localEulerAngles -= new Vector3(difx, 0, difz);
        }
        newRotationY = Mathf.Clamp(newRotationY, 270, 359);
        transform.localEulerAngles = new Vector3(newRotationY, newRotationX, 0f);

        Cannoen.transform.localEulerAngles = new Vector3(Cannoen.transform.localEulerAngles.x, transform.localEulerAngles.y, Cannoen.transform.localEulerAngles.z);
        Slope.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Cannoen.transform.localEulerAngles.y, Cannoen.transform.localEulerAngles.z);
        //Cannoen.transform.rotation = Quaternion.Euler(Cannoen.transform.localRotation.x,transform.localRotation.y,Cannoen.transform.rotation.z);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        { ThirdPersonV = false; }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        { ThirdPersonV = true; }
    }
}
