using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform target;

    private float x = 0.0f;
    private float y = 0.0f;

    private int mouseXSpeedMod = 5;
    private int mouseYSpeedMod = 5;

    private float MaxViewDistance = 15f;
    private float MinViewDistance = 1f;
    private int ZoomRate = 20;
    public float distance = 3f;
    private float desireDistance;

    private float minYRotation = 0f;
    private float maxYRotation = 30f;
    //private float correctedDistance;
    //private float currentDistance;

    public float cameraTargetHeight = 1.0f;

    void Start()
    {
        Vector3 Angles = transform.eulerAngles;
        x = Angles.x;
        y = Angles.y;
        //currentDistance = distance;
        desireDistance = distance;
        //correctedDistance = distance;
    }

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * mouseXSpeedMod;
        y += Input.GetAxis("Mouse Y") * mouseYSpeedMod;

        y = ClampAngle(y, minYRotation, maxYRotation);
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        desireDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * ZoomRate * Mathf.Abs(desireDistance);
        desireDistance = Mathf.Clamp(desireDistance, MinViewDistance, MaxViewDistance);
        //correctedDistance = desireDistance;

        Vector3 position = target.position - (rotation * Vector3.forward * desireDistance);

        //RaycastHit collisionHit;
        //Vector3 cameraTargetPosition = new Vector3(target.position.x, target.position.y + cameraTargetHeight, target.position.z);

        //bool isCorrected = false;
        //if (Physics.Linecast(cameraTargetPosition, position, out collisionHit))
        //{
        //    position = collisionHit.point;
        //    correctedDistance = Vector3.Distance(cameraTargetPosition, position);
        //    isCorrected = true;
        //}

        //currentDistance = !isCorrected || correctedDistance > currentDistance ? Mathf.Lerp(currentDistance, correctedDistance, Time.deltaTime * ZoomRate) : correctedDistance;

        position = target.position - (rotation * Vector3.forward * desireDistance + new Vector3(0, -cameraTargetHeight, 0));

        transform.rotation = rotation;
        transform.position = position;

        //CameraTarget.rotation = rotation;

        //float cameraX = transform.rotation.x;
        //checks if right mouse button is pushed
        //if (Input.GetMouseButton(1))
        //{
        //    //sets CHARACTERS x rotation to match cameras x rotation
        //    target.eulerAngles = new Vector3(cameraX, transform.eulerAngles.y, transform.eulerAngles.z);
        //}

    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
        {
            angle += 360;
        }
        if (angle > 360)
        {
            angle -= 360;
        }
        return Mathf.Clamp(angle, min, max);
    }
}
