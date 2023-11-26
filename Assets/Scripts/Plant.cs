using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    public float maxScale = 2f;
    public float growRate = 0.01f;
    public float maxAgeSeconds = 30f;
    private float age = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        age += Time.deltaTime;

        //increase scale until it reaches maxScale
        if (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(growRate, growRate, growRate);
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
