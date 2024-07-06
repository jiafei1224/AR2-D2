using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotateobject : MonoBehaviour
{
     // Start is called before the first frame update
    public GameObject objectRotate;
    public float rotatespeed = 50f;
    bool rotateStatus =false;
    public GameObject PD;

    void Start()
    {
        PD = GameObject.Find("Assets/panda0.prefab");
        
    }

    public void RotateObject()
    {
        if (rotateStatus == false)
        {
            rotateStatus = true;
        }
        else
        {
            rotateStatus = false;
        }
    }
    
    void Update() 
    {
        if (rotateStatus == true)
        {
           PD.transform.Rotate(0, 100 * Time.deltaTime, 0);
        }
    }
}



