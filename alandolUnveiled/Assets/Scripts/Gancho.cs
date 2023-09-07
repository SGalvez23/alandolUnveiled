using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Build;
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
    public bool canConnect;
    public bool shot;
    [SerializeField]
    float maxRange = 100f; //Rango del Gancho
    Vector3 direction;

    private bool isLeftMousePressed = false;
    public bool IsConnected
    {
        get { return isLeftMousePressed; }

        set
        {
            isLeftMousePressed = Mouse.current.leftButton.isPressed;
        }
    }

    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        canConnect = true;
    }

    private void FixedUpdate()
    {
        direction = new Vector3(controller.MousePos.x, controller.MousePos.y) - transform.position;
        direction.Normalize();
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, direction, maxRange);
        Debug.DrawRay(transform.position, direction, Color.cyan);

        shot = controller.shot;
        if (shot && ValidTarget(raycastHit.collider))
        {
            EnableLine();
        }
        else if (!shot || !ValidTarget(raycastHit.collider) || !IsConnected)
        {
            DisableLine();
        }
    }

    private void EnableLine()
    {
        lineRenderer.enabled = true;
        distanceJoint.enabled = true;
    }

    private void DisableLine()
    {
        lineRenderer.enabled = false;
        distanceJoint.enabled = false;
        distanceJoint.connectedBody = null;
    }

    private bool ValidTarget(Collider2D collider)
    {
        Collider2D currentTarget;
        while(collider == null) 
        {
            return false;
        }

        if (collider.CompareTag("Anclaje") && canConnect)
        {
            currentTarget = collider;
            Debug.Log("valido");
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentTarget.transform.position);
            distanceJoint.connectedAnchor = transform.position;
            distanceJoint.connectedBody = currentTarget.attachedRigidbody;
            return true;
        }

        return false;
    }
}
