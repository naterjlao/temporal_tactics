using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines.Editor;
using UnityEngine;

public class revolve : MonoBehaviour
{
    public float speed = 25.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localEulerAngles = transform.localEulerAngles + (new Vector3(0,1,0) *Time.deltaTime);
        transform.Rotate(0,speed * Time.deltaTime,0);
    }
}
