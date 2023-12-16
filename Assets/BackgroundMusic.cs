using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //play background music from the audio source attached to this object
        GetComponent<AudioSource>().Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
