using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] 
    private float _speedLaser = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move up
        transform.Translate(Vector3.up * _speedLaser * Time.deltaTime);
        
        // destroy laser if y > 8 
        if(transform.position.y >= 8)
        {
            /*if(transform.parent != null)
            {
                Destroy(transform.parent);
            }*/
            Destroy(gameObject);
        }
    }
}
