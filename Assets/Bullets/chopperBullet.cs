using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chopperBullet : MonoBehaviour
{
    [Header("Vectors")]
    public Vector2 minPosition;
    public Vector2 maxPosition;

    private void Update()
    {
        /*
         mermi.x>max.x
         mermi.y>max.y
         mermi.x<min.x
         mermi.y<min.y

          */

        if (
            transform.position.x < minPosition.x ||
            transform.position.y < minPosition.y ||
            transform.position.x > maxPosition.x ||
            transform.position.y > maxPosition.y
            )
        {
            this.gameObject.SetActive(false);
            Debug.Log("Yokedildi");
        }
    }

}
