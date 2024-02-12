using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{

    public Transform watchee; //object you will try to find
    public Transform watcher; //object that will be looking

    [Range(0f, 180f)]
    public float visionAngle = 50f;
    public float visionDistance = 10f;

    bool detected;

    private void update()
    {
        detected = false;

        //detect whether watchee is within the FOV
        Vector2 watcheeVector = watchee.position - watcher.position;
        if (Vector3.Angle(watcheeVector.normalized, watcher.right) < visionAngle * 0.5f){
            if (watcheeVector.magnitude < visionDistance){ detected = true; }
        }
    }

    private void OnDrawGizmos()
    {
        if (visionAngle <= 0f) return;

        float halfVisionAngle = visionAngle * 0.5f;

        Vector2 p1, p2;

        p1 = PointForAngle(halfVisionAngle, visionDistance);
        p2 = PointForAngle(-halfVisionAngle, visionDistance);


        //Draws FOV field visually
        Gizmos.color = detected ? Color.green : Color.red;
        Gizmos.DrawLine(watcher.position, (Vector2)watcher.position + p1);
        Gizmos.DrawLine(watcher.position, (Vector2)watcher.position + p2);
    
        Gizmos.DrawRay(watcher.position, watcher.right * 4f);
    }

    Vector3 PointForAngle(float angle, float distance)
    {
        return watcher.TransformDirection(
            new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), 
            Mathf.Sin(angle * Mathf.Deg2Rad)))
            * distance;
    }

}


//https://youtu.be/lV47ED8h61k?si=6m012cxUMIkJvd5z
//https://www.youtube.com/watch?v=j1-OyLo77ss