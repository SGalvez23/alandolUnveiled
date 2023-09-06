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
    public int maxRange = 10;
    public bool connected = false;
    public List<GameObject> anclajes;
    
    private void Awake()
    {
        controller = GetComponentInParent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
    }

    private void FixedUpdate()
    {
        if (shot)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, controller.MousePos);
            Debug.Log(lineRenderer.GetPosition(0));
            distanceJoint.connectedAnchor = anclajes[0].transform.position;
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;
        }
        else if(!shot)
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;
        }
        if(distanceJoint.enabled) { lineRenderer.SetPosition(0, transform.position); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
