using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    //Private
    private Vector3 playerLocation;
    private float distanceToPlayer;
    private string enemyType;
    private Vector3 startPosition;
    private float bulletDamage;
    private float fireRange;
    private float bulletSpeed;
    private float enemyHealth;
    private float fireDelay;

    //Public
    
        //General
        
        public float enemyMoveSpeed = 0.01f;
        public float minimumSpawnDistance = 15;
        public float maximumSpawnDistance = 30;
        public GameObject bulletPrefab;
        
        //Tank
    
        public float tankMoveSpeed = 0.001f;
        public float tankBulletDamage = 10f;
        public float tankFireRange = 15;
        public float tankBulletSpeed = .05f;
        public float tankHealth = 100;
        public float tankFireDelay = 2;
    
        //Helicopter
    
        public float helicopterMoveSpeed = .005f;
        public float helicopterBulletDamage = 5;
        public float helicopterFireRange = 10;
        public float helicopterBulletSpeed = .08f;
        public float helicopterHealth = 50;
        public float helicopterFireDelay = 1;
    
        
    private void Start()
    {
        playerLocation = GameManager.Instance.player.transform.position;
        SpriteRenderer spriteRendererRef = gameObject.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer;

        var dice = Mathf.RoundToInt(Random.Range(0f, 1f));
        enemyType = dice switch
        {
            0 => "Tank",
            1 => "Helicopter",
            _ => "Enemy"
        };

        switch (enemyType)
        {
            case "Tank":
                startPosition.y = -4.5f;
                enemyMoveSpeed = tankMoveSpeed;
                bulletDamage = tankBulletDamage;
                fireRange = tankFireRange;
                bulletSpeed = tankBulletSpeed;
                fireDelay = tankFireDelay;
                enemyHealth = tankHealth;
                spriteRendererRef.color = Color.green;
                break;
            
            case "Helicopter":
                startPosition.y = Random.Range(-2f, 4.5f);
                enemyMoveSpeed = helicopterMoveSpeed;
                bulletDamage = helicopterBulletDamage;
                fireRange = helicopterFireRange;
                bulletSpeed = helicopterBulletSpeed;
                fireDelay = helicopterFireDelay;
                enemyHealth = helicopterHealth;
                spriteRendererRef.color = Color.red;
                break;
        }

        
        startPosition.x = playerLocation.x + Random.Range(minimumSpawnDistance, maximumSpawnDistance);
        transform.position = startPosition;
        
        fireDelay = Random.Range((fireDelay - 0.5f), (fireDelay + 0.5f));
        InvokeRepeating("Fire", fireDelay,fireDelay);
    }   

    // Update is called once per frame
    private void Update()
    {
        playerLocation = GameManager.Instance.player.transform.position;
        transform.position += new Vector3(-enemyMoveSpeed, 0, 0);
        distanceToPlayer = Vector3.Distance(transform.position, playerLocation);

        if (transform.position.x < -11)
        {
            transform.position = startPosition;
        }
        
    }
    
    private void Fire()
    {
        if (distanceToPlayer < fireRange && transform.position.x>playerLocation.x)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().bulletDamage = bulletDamage;
            bullet.GetComponent<Bullet>().bulletSpeed = bulletSpeed;
        }
    }
    
}

