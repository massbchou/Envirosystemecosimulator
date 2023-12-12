using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    public float maxScale = 2f;
    public float growRate = 1f;
    public float maxAgeSeconds = 30f;
    public float sizeVariancePercentage = 0.25f;
    public float startingScale = 0.1f;
    private float age = 0;
    // Start is called before the first frame update
    void Start()
    {
        //set current scale of this object and its children to startingScale
        transform.localScale = new Vector3(startingScale, startingScale, startingScale);
        
        //reduce the maxScale by a random percentage up to sizeVariancePercentage
        maxScale *= 1 - Random.Range(0, sizeVariancePercentage);
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
