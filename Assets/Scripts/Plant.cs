using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    public float avgMaxScale = 2f;
    public float growRate = 1f;
    public float maxAgeSeconds = 30f;
    public float sizeVariancePercentage = 0.25f;
    public float startingScale = 0.1f;
    private float age = 0;
    private float maxScale = 2f;
    // Start is called before the first frame update
    void Start()
    {
        //set current scale of this object and its children to startingScale
        transform.localScale = new Vector3(startingScale, startingScale, startingScale);
        
        //change the maxScale by a random percentage up to sizeVariancePercentage
        maxScale = avgMaxScale * 1 - Random.Range(-sizeVariancePercentage, sizeVariancePercentage);
        if (maxScale < 0)
        {
            maxScale = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        //increase scale until it reaches maxScale
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(growRate * Time.deltaTime, growRate * Time.deltaTime, growRate * Time.deltaTime);
        }
        else
        {
            //check if there are more than 3 plants nearby
            Collider[] nearbyPlants = Physics.OverlapSphere(transform.position, 10f);
            int numPlants = 0;
            foreach (Collider c in nearbyPlants)
            {
                if (c.gameObject.tag == "Plant")
                {
                    numPlants++;
                }
            }
            //25% chance to spawn a new plant nearby every second
            if (Random.Range(0, 4) == 0 && age % 1 < Time.deltaTime && numPlants < 3)
            {
                //spawn a new plant nearby
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                int tries = 0;
                //check if the spawn position is in the terrain's bounds and keep trying until it is
                while (!Terrain.activeTerrain.terrainData.bounds.Contains(spawnPosition) && tries < 5)
                {
                    tries++;
                    spawnPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
                }
                
                
                
                //Get the terrain height at the spawn position
                spawnPosition.y = Terrain.activeTerrain.SampleHeight(spawnPosition);
                //spawn a new plant at the spawn position with age = 0 and a random rotation
                GameObject newPlant = Instantiate(gameObject, spawnPosition, Quaternion.Euler(0f, Random.Range(0f, 360f), 0f));
                newPlant.GetComponent<grow>().age = 0;
            }
        }
        
        
        

        //if age is bigger than maxAgeFrames, then destroy the plant
        if (age > maxAgeSeconds)
        {
            Destroy(gameObject);
        }
        
        //change orientation of the plant based on the terrain
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        
    }
    
    //if colliding with the ground push the plant up
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            transform.position += new Vector3(0, 0.5f, 0);
        }
    }
}
