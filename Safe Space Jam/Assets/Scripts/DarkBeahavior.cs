using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBeahavior : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform originPoint;
    public Transform endPoint;
    public Collider2D playerCollider;
    public Transform playerTranform;
    [SerializeField] List<Transform> nodes;
    LineRenderer lr;
    LineCollision lc;

    [Range (0.5f, 10f)] public float beamWidth = 1f;

    private Vector2 beamDirection;

    private void Start()
    {
        lr = lineRenderer;
        lr.positionCount = nodes.Count;
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = beamWidth;
        lc = GetComponent<LineCollision>();
    }

    void Update()
    {
        UpdateDarkBeam();
    }

    private void UpdateDarkBeam()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)originPoint.position, beamDirection.normalized, beamDirection.magnitude);

        lineRenderer.SetPosition(0, originPoint.position);

        lineRenderer.SetPosition(1, endPoint.position);

        beamDirection = (Vector2)endPoint.position - (Vector2)originPoint.position;

        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);            
        }
    }

    public Vector3[] GetPositions()
    {
        Vector3[] positions = new Vector3[lr.positionCount];
        lr.GetPositions(positions);
        return positions;
    }

    public float GetWidth()
    {
        return lr.startWidth;
    }
}
