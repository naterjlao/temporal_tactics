using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTargetTracker : MonoBehaviour
{
    public MonoBehaviour target;

    private Vector3 origin;
    private Vector3 mask;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position; // Quick n' dirty: cache origin position 
        mask = new Vector3(1,0,1); // we only care about the floor axes
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = origin + Vector3.Scale(target.transform.position, mask);
    }
}
