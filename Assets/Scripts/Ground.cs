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
    internal Vector3 _center; //center of the ground
    /* Corners are built as such
     * 1---2
     *
     * 4---3
     */
    
    // Start is called before the first frame update
    void Awake()
    {
        // Get the terrain component
        Terrain terrain = GetComponent<Terrain>();

        // Calculate the corners in the world space using terrain's bounds
        Bounds bounds = terrain.terrainData.bounds;
        _corner1 = bounds.min;
        _corner2 = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        _corner3 = bounds.max;
        _corner4 = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);

        // Transform the corners to world space
        _corner1 = transform.TransformPoint(_corner1);
        _corner2 = transform.TransformPoint(_corner2);
        _corner3 = transform.TransformPoint(_corner3);
        _corner4 = transform.TransformPoint(_corner4);

        // Calculate the center in the world space
        _center = bounds.center;
        _center = transform.TransformPoint(_center);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
