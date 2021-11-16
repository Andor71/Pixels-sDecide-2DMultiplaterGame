using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{


    private float lenght, startpos;
    GameObject camera;
    public float Effect;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.Find("PlayerCamera");
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp =(camera.transform.position.x *(1- Effect));
        float dist= (camera.transform.position.x*Effect);
        transform.position = new Vector3(startpos + dist , transform.position.y , transform.position.z);

        if(temp > startpos + lenght){
            startpos += lenght;
        }
        else if(temp < startpos - lenght){
            startpos -= lenght;
        }
    }
    
        
    
}
