using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rocket : MonoBehaviour
{
    
    //Public
    public float rocketSpeed = 0.05f;
    
    //Private
    private Vector3 playerLocation;
    private Vector3 startPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        playerLocation = GameManager.Instance.player.transform.position;
        startPosition.x = playerLocation.x + Random.Range(20, 35);
        startPosition.y = Random.Range(-1f, 4.5f);

        transform.position = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = GameManager.Instance.player.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, playerLocation * 100, rocketSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Boom");
    }
}
