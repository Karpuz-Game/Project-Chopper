using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chopperBrain : MonoBehaviour
{
    [Header("Floats")]
    public float maxSpeed;
    private float horizontal;
    public float horizontalSpeed;
    public float clampXmin, clampXmax;
    public float clampYmin, clampYmax;
    public float bulletSpeed = 50f;
    public float forwardRotation, backwardRotation,rotationSpeed;


    [Header("Vectors")]
    public Vector3 gravitiy;
    public Vector3 thrust;
    public Vector3 horizontalThrust;
    private Vector3 clampPosition;
    private Vector3 mouseMove;
    Vector3 chopperRotation;


    Vector3 velocity = Vector3.zero;

    [Header("GameObjects")]
    private GameObject turret;
    private GameObject crossHair;


    [Header("Cams")]
    private Camera mainCam;

    [Header("Others")]
    public chopperBulletPool chopperBulletPool;


    private void Start()
    {
        turret = gameObject.transform.GetChild(0).gameObject;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        crossHair = gameObject.transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        MouseTracking();
        //TODO: fix fire back
        if (Input.GetMouseButtonDown(0))
        {
            Fire(chopperBulletPool.GetNextAvailableObject());
        }
    }
    private void FixedUpdate()
    {
        chopperPhysics();
    }



    private void chopperPhysics() {
        horizontal = Input.GetAxisRaw("Horizontal");

        velocity += gravitiy * Time.deltaTime;

        if (horizontal != 0)
            velocity += (horizontal * horizontalThrust * Time.fixedDeltaTime) * horizontalSpeed;

        if (horizontal > 0 && chopperRotation.z > forwardRotation)
        {
            chopperRotation.z -= Time.deltaTime* rotationSpeed;
        }else if (horizontal < 0 && chopperRotation.z < backwardRotation) {
                chopperRotation.z += Time.deltaTime * rotationSpeed;
        }
        else {
            if (chopperRotation.z < 0 && chopperRotation.z != 0) {
                chopperRotation.z += Time.deltaTime * rotationSpeed;
            }
            if (chopperRotation.z > 0 && chopperRotation.z != 0) {
                chopperRotation.z -= Time.deltaTime * rotationSpeed;
            }
        }
            

        transform.rotation = Quaternion.Euler(chopperRotation);



        if (Input.GetKey(KeyCode.W)) {
            velocity += thrust;
        }
        
        clampPosition = transform.position;

        clampPosition.x = Mathf.Clamp(clampPosition.x, clampXmin, clampXmax);
        clampPosition.y = Mathf.Clamp(clampPosition.y, clampYmin, clampYmax);


        transform.position = clampPosition;

        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if (velocity.x != 0 && horizontal == 0)
        {
            if (velocity.x > 0)
                velocity -= ((horizontalThrust / 5) * Time.fixedDeltaTime) * horizontalSpeed;
            if (velocity.x < 0)
                velocity += ((horizontalThrust / 5) * Time.fixedDeltaTime) * horizontalSpeed;
        }
    }

    private void MouseTracking()
    {
        mouseMove = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mouseMove.Set(mouseMove.x, mouseMove.y, transform.position.z);

        float angleRad = Mathf.Atan2(
           mouseMove.y - turret.transform.position.y,
           mouseMove.x - turret.transform.position.x
           );

        float angleDeg = (180 / Mathf.PI) * angleRad;

        turret.transform.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(angleDeg, -80.0F, 41.15F));

        crossHair.transform.position = mouseMove;

    }

    private void Fire(GameObject bullet)
    {
        bullet.transform.position = turret.transform.position;
        bullet.transform.rotation = turret.transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce((crossHair.transform.position - turret.transform.position).normalized * bulletSpeed * 1000);
    }
}
