using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    public float maxScale = 2f;
    public float growRate = 1f;
    public float maxAgeSeconds = 30f;
    public float sizeVariancePercentage = 0.25f;
    private float age = 0;
    // Start is called before the first frame update
    void Start()
    {
        //set current scale to 0.1
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        
        //reduce the maxScale by a random percentage up to sizeVariancePercentage
/*        maxScale *= 1 - Random.Range(0, sizeVariancePercentage);
        if (maxScale < 0)
        {
            maxScale = 0.1f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        //increase scale until it reaches maxScale
        if (transform.localScale.x < maxScale)
        {
            Debug.Log("here");
            transform.localScale += new Vector3(growRate * Time.deltaTime, growRate * Time.deltaTime, growRate * Time.deltaTime);
        }
        

        //if age is bigger than maxAgeFrames, then destroy the plant
        if (age > maxAgeSeconds)
        {
            Destroy(gameObject);
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
