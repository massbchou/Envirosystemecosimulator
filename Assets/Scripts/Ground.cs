using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private int _gridGranularity = 2; //x by x grid
    
    
    
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
    
    //array of ground tiles
    internal GameObject[,] _groundTiles;
    
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

        // Calculate the size of the grid
        _groundTiles = new GameObject[_gridGranularity, _gridGranularity];

        // Calculate the size of each tile
        Vector3 tileSize = new Vector3((_corner2.x - _corner1.x) / _gridGranularity, 0, (_corner4.z - _corner1.z) / _gridGranularity);

        // Calculate the position of each tile
        for (int x = 0; x < _gridGranularity; x++)
        {
            for (int z = 0; z < _gridGranularity; z++)
            {
                Vector3 tileCenter = _corner1 + new Vector3(tileSize.x * (x + 0.5f), 0, tileSize.z * (z + 0.5f));
                // Create a tile with the GroundTile script attached. 
                // Naming Scheme is GroundTile_x_z
                GameObject tile = new GameObject("GroundTile_" + x + "_" + z);
                // Add the GroundTile script to the tile
                GroundTile groundTile = tile.AddComponent<GroundTile>();
                // Initialize the GroundTile with the specified data
                groundTile.Initialize(tileCenter, tileSize);
                // Set the tile's parent to the Ground
                tile.transform.parent = transform;
                // put the tile in the array
                _groundTiles[x, z] = tile;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
