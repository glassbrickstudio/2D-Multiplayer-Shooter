using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{

    public float Speed = 25f;
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    public Vector2 MinBoundary;
    public Vector2 MaxBoundary;



    void Start()
    {
        targetPos = transform.position;
    }


    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * Speed;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

              transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinBoundary.x, MaxBoundary.x),
                  Mathf.Clamp(transform.position.y, MinBoundary.y, MaxBoundary.y), transform.position.z);

        }
    }
}
