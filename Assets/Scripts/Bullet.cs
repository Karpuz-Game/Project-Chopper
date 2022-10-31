using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Bullet : MonoBehaviour
{
    //Private
    private Vector3 playerPosition;
    private Vector3 playerDirection;
    private Vector3 targetPosition;
    private Vector3 selfPosition;


    public float bulletSpeed;
    public float bulletDamage;
    private Bullet(float bulletSpeed,float bulletDamage)
    {
        this.bulletSpeed = bulletSpeed;
        this.bulletDamage = bulletDamage;
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        selfPosition = transform.position;
        playerPosition = GameManager.Instance.player.transform.position;
        playerDirection = (playerPosition - selfPosition).normalized;
        targetPosition = selfPosition + playerDirection * 1000;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition,this.bulletSpeed);

        if (transform.position.x < -10)
        {
            Destroy(this.gameObject);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }
}
