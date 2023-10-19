using UnityEngine;

/*public class HookRope : MonoBehaviour
{
    public Player annora;
    public LineRenderer lr;

    [SerializeField] private int percision = 40;
    [Range(0, 20)][SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings:")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)][SerializeField] private float StartWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression:")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField][Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;
    public bool isGrappling = false;
    bool strightLine = true;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.enabled = false;
        lr.positionCount = percision;
        waveSize = StartWaveSize;
    }

    private void OnEnable()
    {
        moveTime = 0;
        lr.enabled = true;
        lr.positionCount = percision;
        waveSize = StartWaveSize;
        strightLine = false;

        LinePointsToFirePoint();
    }

    private void OnDisable()
    {
        lr.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < percision; i++)
        {
            lr.SetPosition(i, annora.firePoint.position);
        }
    }

    private void FixedUpdate()
    {
        moveTime += Time.deltaTime;

        DrawRope();
    }

    void DrawRope()
    {
        if (!strightLine)
        {
            if (lr.GetPosition(percision - 1).x != annora.grapplePoint.x)
            {
                DrawRopeWaves();
                annora.Grapple();
                isGrappling = true;
            }
            else
            {
                strightLine = true;
            }
        }
        else
        {
            if (!isGrappling)
            {
                annora.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;
                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = (float)i / ((float)percision - 1f);
            Vector2 offset = Vector2.Perpendicular(annora.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(annora.firePoint.position, annora.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(annora.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            lr.SetPosition(i, currentPosition);
        }
    }

    void DrawRopeNoWaves()
    {
        lr.SetPosition(0, annora.firePoint.position);
        lr.SetPosition(1, annora.grapplePoint);
    }
}
*/