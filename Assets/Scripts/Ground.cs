using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    //variables that should be accessible from other scripts
    internal Vector3 _corner1; //northwest corner
    internal Vector3 _corner2; //northeast corner
    internal Vector3 _corner3; //southeast corner
    internal Vector3 _corner4; //southwest corner
    /* Corners are built as such
     * 1---2
     *
     * 4---3
     */
    
    // Start is called before the first frame update
    void Start()
    {
        //calculate the corners in the world space
        _corner1 = transform.position + new Vector3(-transform.localScale.x / 2, 0, transform.localScale.z / 2);
        _corner2 = transform.position + new Vector3(transform.localScale.x / 2, 0, transform.localScale.z / 2);
        _corner3 = transform.position + new Vector3(transform.localScale.x / 2, 0, -transform.localScale.z / 2);
        _corner4 = transform.position + new Vector3(-transform.localScale.x / 2, 0, -transform.localScale.z / 2);
        
        _corner1 = transform.TransformPoint(_corner1);
        _corner2 = transform.TransformPoint(_corner2);
        _corner3 = transform.TransformPoint(_corner3);
        _corner4 = transform.TransformPoint(_corner4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
