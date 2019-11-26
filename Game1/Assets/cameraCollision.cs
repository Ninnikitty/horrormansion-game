using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraCollision : MonoBehaviour{

    private Vector3 collidee = Vector3.zero;
    private float collisionAngle = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 v3 = collision.gameObject.transform.position;
        compensateForWalls(this.gameObject.transform.position, ref v3);
    }
    private void compensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
    {
        Debug.DrawLine(fromObject, toTarget, Color.cyan);

        RaycastHit wallHit = new RaycastHit();
        if (Physics.Linecast(fromObject, toTarget, out wallHit))
        {
            Debug.DrawRay(wallHit.point, wallHit.normal, Color.red);

            Vector3 wallHitVector3 = new Vector3(wallHit.point.x, wallHit.point.y, wallHit.point.z);

            toTarget = new Vector3(wallHitVector3.x, toTarget.y, wallHitVector3.z);
        }
    }
}
