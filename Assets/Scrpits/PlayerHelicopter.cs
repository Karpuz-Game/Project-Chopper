using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHelicopter : MonoBehaviour
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
    private Vector3 chopperRotation;

    Vector3 velocity = Vector3.zero;
   
    private void FixedUpdate() {
        chopperPhysics();
    }

    private void chopperPhysics() {
        horizontal = Input.GetAxisRaw("Horizontal");
        velocity += gravitiy * Time.deltaTime;

        if (Input.GetKey(KeyCode.W)) {
            velocity += thrust;
        }

        if (horizontal != 0) {
            velocity += (horizontal * horizontalThrust * Time.fixedDeltaTime) * horizontalSpeed;
        }
        
        transform.position += velocity * Time.deltaTime;
    }

}
