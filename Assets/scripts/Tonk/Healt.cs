using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Healt : MonoBehaviour
{
    public float healt;
    public float MaxHealt;
    public Slider slider;
    void Start()
    {
        healt = MaxHealt;
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "projectile")
        {
            healt -= 20;
        }
        if (slider != null)
        {
            if (MaxHealt != 0)
            {
                slider.SetValueWithoutNotify(healt / MaxHealt);
            }
        }
    }
}
