using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Gancho : MonoBehaviour
{
    Rigidbody2D rb;
    DistanceJoint2D distanceJoint;
    LineRenderer lineRenderer;
    PlayerController controller;
    public GameObject[] ropeSegments;
    public int numLinks = 5;
    public bool canConnect;
    public bool shot;
    [SerializeField]
    float maxRange = 100f; //Rango del Gancho
    Vector3 direction;
    Collider2D currentTarget;
    private bool isConnected;

    public bool hasRan = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponentInParent<PlayerController>();
        lineRenderer = GetComponent<LineRenderer>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        canConnect = true;
        isConnected = false;
    }

    private void FixedUpdate()
    {
        direction = new Vector3(controller.MousePos.x, controller.MousePos.y) - transform.position;
        direction.Normalize();
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, direction, maxRange);
        Debug.DrawRay(transform.position, direction, Color.cyan);

        shot = controller.shot;
        currentTarget = ValidTarget(raycastHit.collider);
        lineRenderer.SetPosition(0, transform.position);
        distanceJoint.connectedAnchor = transform.position;

        if (shot && currentTarget != null && canConnect && !isConnected)
        {
            DrawLineToCollider(currentTarget);
            if(!hasRan)
                GenerateRope();
            //EnableLine();
        }
        else if (shot && currentTarget is null)
        {
            canConnect = true;
            isConnected = false;
            Debug.Log("miss");
        }
        else if (!shot)
        {
            canConnect = true;
            isConnected = false;
            DisableLine();
        }
    }

    private void EnableLine()
    {
        lineRenderer.enabled = true;
        distanceJoint.enabled = true;

        distanceJoint.maxDistanceOnly = true;
        distanceJoint.distance = maxRange;
    }

    private void DisableLine()
    {
        lineRenderer.enabled = false;
        distanceJoint.enabled = false;
    }

    private Collider2D ValidTarget(Collider2D collider)
    {
        while(collider == null) 
        {
            return null;
        }

        if (collider.CompareTag("Anclaje") && !isConnected)
        {
            Debug.Log("valido");
            return collider;
        }

        return null;
    }

    private void DrawLineToCollider(Collider2D collider)
    {    
        lineRenderer.SetPosition(1, collider.transform.position);
        distanceJoint.connectedBody = collider.attachedRigidbody;
        canConnect = false;
        isConnected = true;
    }

    private void GenerateRope()
    {
        Rigidbody2D prevBody = rb;

        for (int i = 0; i < numLinks; i++)
        {
            RopeSegment newSeg = Instantiate(ropeSegments[i].GetComponent<RopeSegment>());
            newSeg.transform.parent = transform;
            newSeg.transform.position = transform.position;
            HingeJoint2D hj = newSeg.GetComponent<HingeJoint2D>();
            hj.connectedBody = prevBody;

            prevBody = newSeg.GetComponent<Rigidbody2D>();

            if(newSeg.gameObject.name == "PedazoFinal(Clone)")
            {
                newSeg.connectedBelow = currentTarget.gameObject;
                newSeg.GetComponent<FixedJoint2D>().connectedBody = currentTarget.attachedRigidbody;
            }
        }

        canConnect = false;
        isConnected = true;
        hasRan = true;
    }
}
