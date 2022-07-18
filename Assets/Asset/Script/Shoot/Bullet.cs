using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dir = transform.up.y * speed * Time.deltaTime;
        transform.position += new Vector3 (0,dir,0);
    }
}
