using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Sprite on;
    private AudioSource woosh;
    private bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        woosh = this.GetComponent<AudioSource>();
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && !isOn)
        {
            player.lastCheckpoint = this.gameObject;
            woosh.Play();
            this.GetComponent<SpriteRenderer>().sprite = on;
            isOn = true;
        }
    }

}
