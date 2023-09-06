using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

[RequireComponent(typeof(Rigidbody2D))]
public class Gancho : MonoBehaviour
{
    DistanceJoint2D distanceJoint;
    LineRenderer lineRenderer;
    PlayerController controller;
    public bool shot;
    public float maxRange = 10f;
    public bool connected = false;
    public List<GameObject> anclajes;
    RaycastHit2D ray;
    private bool isAiming;
    private GameObject brazo;
    
    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        brazo = controller.arm;
    }

    private void FixedUpdate()
    {
        ray = Physics2D.Raycast(controller.MousePos, transform.position, maxRange);
        Vector3 difference = new Vector3(controller.MousePos.x, controller.MousePos.y) - transform.position;
        difference.Normalize();
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0f, 0f, rotationZ + 90);
        Debug.DrawRay(controller.MousePos, transform.position, Color.cyan);
        //isAiming = controller.IsAiming;

        if (shot)
        {
            if (ray.collider != null)
            {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, ray.collider.transform.position);
                lineRenderer.enabled = true;
                distanceJoint.enabled = true;
                distanceJoint.connectedAnchor = ray.collider.transform.position;
                distanceJoint.connectedBody = ray.collider.attachedRigidbody;
            }
        }
        else if (!shot)
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
            distanceJoint.connectedBody = null;
        }
        if (distanceJoint.enabled) { lineRenderer.SetPosition(0, transform.position); }
    }
}
