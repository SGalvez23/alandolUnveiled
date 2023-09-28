using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnnoraController : PlayerController
{
    //Scripts
    public HookRope hookRope;

    //Gancho
    public Transform gunHolder;
    public Transform gunPivot;
    public Vector2 grapplePoint;
    public Vector2 grappleDistanceVector;
    bool launchToPoint = true;
    [SerializeField] float launchSpeed = 0.3f;
    public bool infiniteRange = false;
    private float targetDistance = 3;
    private float targetFrequncy = 1;

    SpringJoint2D sj;

    private void Awake()
    {
        sj = GetComponent<SpringJoint2D>();
        sj.enabled = false;
        hookRope.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        sight.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (!LockedVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (IsAiming)
        {
            MouseOnScreen();

            joysitckOnScreen = Camera.main.ScreenToWorldPoint(JoyStickPos);
            Vector3 joyDiff = new Vector3(joysitckOnScreen.x, joysitckOnScreen.y) - transform.position;
            joyDiff.Normalize();
            float rotZ = Mathf.Atan2(joyDiff.y, joyDiff.x) * Mathf.Rad2Deg;
            //arm.transform.rotation = Quaternion.Euler(0f, 0, rotZ + 90);
        }
    }

    //Funcion que coloca el punto final del gancho
    void SetGrapplePoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, difference.normalized, 1 << 11);
        if (hit.collider != null)
        {
            Debug.Log(hit.transform.gameObject.name);
            if (Vector2.Distance(hit.point, firePoint.position) <= maxRange && hit.collider.CompareTag("Anclaje") || infiniteRange)
            {
                grapplePoint = hit.point;
                grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                hookRope.enabled = true;

            }
        }
    }

    //Funcion que modifica el springJoint2D simular el agarre del gancho
    public void Grapple()
    {
        sj.autoConfigureDistance = false;
        if (!launchToPoint && !sj.autoConfigureDistance)
        {
            sj.distance = targetDistance;
            sj.frequency = targetFrequncy;
        }

        if (!launchToPoint)
        {
            if (sj.autoConfigureDistance)
            {
                sj.autoConfigureDistance = true;
                sj.frequency = 0;
            }

            sj.connectedAnchor = grapplePoint;
            sj.enabled = true;
        }
        else
        {
            sj.connectedAnchor = grapplePoint;
            Vector2 distance = firePoint.position - gunHolder.position;
            sj.distance = distance.magnitude;
            sj.frequency = launchSpeed;
            sj.enabled = true;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started && IsAiming)
        {
            shot = true;
            SetGrapplePoint();
            //configurar animacion
        }
        else if (context.canceled)
        {
            shot = false;
            rb.gravityScale = 1;
            sj.enabled = false;
            hookRope.enabled = false;
        }
    }
}