using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
   [Header("Floats")]
   public float smoothSpeed = 0.125f;
   public float minY, maxY;

   [Header("Vectors")]
   public Vector3 offset;


   private Transform target;

   private void Start()
   {
      target = GameManager.Instance.player.transform;
   }
   private void LateUpdate()
   {
      Vector3 desiredPosition = new Vector3(transform.position.x, Mathf.Clamp(target.transform.position.y, minY, maxY), transform.position.z) + offset;
      Vector3 smoothedPosition = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed);

      transform.position = smoothedPosition;
   }
}
