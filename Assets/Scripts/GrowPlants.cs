using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    //The plant prefab
    public GameObject plantPrefab;
    [SerializeField] private GameObject groundPlane;
    
    //spawning range
    private float groundXMin; //left
    private float groundXMax; //right
    private float groundZMin; //bottom
    private float groundZMax; //top
    private Vector3 groundCenter;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the ground's corners
        Ground ground = groundPlane.GetComponent<Ground>();
        groundXMin = ground._corner1.x;
        groundXMax = ground._corner2.x;
        groundZMin = ground._corner1.z;
        groundZMax = ground._corner4.z;
        groundCenter = ground._center;
    }

    // Update is called once per frame
    void Update()
    {
        //Generate a random number between 0 and 1
        float randomNumber = Random.Range(0f, 1f);
        
        //if bigger than 0.99, then grow a new plant
        if (randomNumber > 0.99f)
        {
            GrowNewPlant();
        }
    }
    
    void GrowNewPlant()
    {
        float randX = Random.Range(groundXMin, groundXMax);
        float randZ = Random.Range(groundZMin, groundZMax);
        float terrainY = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
        //Instantiate a new plant with scale 0.1, at a random position on the ground and a random rotation
        Instantiate(plantPrefab, new Vector3(randX, terrainY, randZ), Quaternion.Euler(0, Random.Range(0, 360), 0));
        
    }
}
