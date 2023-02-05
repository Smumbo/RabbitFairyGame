using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startpos;
    public float parallaxFactor;
    public GameObject camera;

    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = camera.transform.position.x * (1 - parallaxFactor);
        float distance = camera.transform.position.x * parallaxFactor;
        Vector3 newPosition = new Vector3(startpos + distance, camera.transform.position.y, transform.position.z);
        transform.position = newPosition;

        if (temp > startpos + (length / 2))
        {
            startpos += length;
        }
        else if (temp < startpos - (length / 2))
        {
            startpos -= length;
        }
    }
}
