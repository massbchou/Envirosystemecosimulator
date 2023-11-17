using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    //The plant prefab
    public GameObject plantPrefab;
    public int maxPlants = 10;
    public float groundx = 10f;
    public float groundz = 10f;
    public float groundy = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
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
        
        //Instantiate a new plant with scale 0.1, at a random position on the ground
        Instantiate(plantPrefab, new Vector3(Random.Range(-groundx, groundx), groundy, Random.Range(-groundz, groundz)), Quaternion.identity);
        
        


    }
}
