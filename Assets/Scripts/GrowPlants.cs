using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowPlants : MonoBehaviour
{
    //The plant prefab
    public GameObject plantPrefab;
    [SerializeField] private GameObject groundPlane;

    public float growRate = 1f;
    

    private Vector3 groundCenter;

    public bool shouldGrowPlants = false;
    bool growing = false;

    Ground ground;
    
    // Start is called before the first frame update
    void Start()
    {
        //Get the ground's corners
        ground = groundPlane.GetComponent<Ground>();

        groundCenter = ground._center;

    }

    private void Update()
    {
        if(shouldGrowPlants && !growing)
        {
            StartCoroutine(GrowPlantsCoroutine());
            growing = true;
        }
        if(!shouldGrowPlants && growing)
        {
            StopCoroutine(GrowPlantsCoroutine());
            growing = false;
        }
    }

    IEnumerator GrowPlantsCoroutine()
    {
        while (true)
        {
            GrowNewPlant();
            yield return new WaitForSeconds(growRate + Random.Range(-0.5f, 0.5f));
        }
    }
    
    void GrowNewPlant()
    {
        float randX = Random.Range(ground.groundXMin, ground.groundXMax);
        float randZ = Random.Range(ground.groundZMin, ground.groundZMax);
        float terrainY = Terrain.activeTerrain.SampleHeight(new Vector3(randX, 0, randZ));
        //Instantiate a new plant with scale 0.1, at a random position on the ground and a random rotation
        Instantiate(plantPrefab, new Vector3(randX, terrainY, randZ), Quaternion.Euler(0, Random.Range(0, 360), 0));
        
    }
}
