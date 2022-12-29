using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class RuedaVolante : MonoBehaviour
{

    public CircularDrive circularDrive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(circularDrive.outAngle, 0, 0);
    }
}