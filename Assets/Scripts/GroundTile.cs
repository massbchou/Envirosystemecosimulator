using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GroundTile : MonoBehaviour
{
    
    
    //variables that should be accessible from other scripts
    internal Vector3 _corner1; //northwest corner
    internal Vector3 _corner2; //northeast corner
    internal Vector3 _corner3; //southeast corner
    internal Vector3 _corner4; //southwest corner
    internal Vector3 _center; //center of the tile
    /* Corners are built as such
     * 1---2
     *
     * 4---3
     */
    
    //initialize the tile
    public void Initialize(Vector3 tileCenter, Vector3 tileSize)
    {
        // Calculate the corners in the world space using the tile's center and size
        _corner1 = tileCenter + new Vector3(-tileSize.x / 2, 0, tileSize.z / 2);
        _corner2 = tileCenter + new Vector3(tileSize.x / 2, 0, tileSize.z / 2);
        _corner3 = tileCenter + new Vector3(tileSize.x / 2, 0, -tileSize.z / 2);
        _corner4 = tileCenter + new Vector3(-tileSize.x / 2, 0, -tileSize.z / 2);
        
        // Calculate the center in the world space
        _center = tileCenter;
        
        //set the center's y to the terrain's height at that point
        _center.y = Terrain.activeTerrain.SampleHeight(_center);
        
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        // go to the center of the tile
        transform.position = _center;   
        
        //start spawning cubes
        //StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = _center;
            cube.GetComponent<Renderer>().material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f));
            Destroy(cube, 1);
            yield return new WaitForSeconds(1);
        }
    }
}
