using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBeahavior : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform originPoint;
    public Transform endPoint;
    public Collider2D playerCollider;
    private ParticleSystem particals;

    [Range (0.5f, 10f)] public float beamWidth = 1f;

    private Vector2 beamDirection;

    private void Start()
    {
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = beamWidth;
        particals = endPoint.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        UpdateDarkBeam();
    }

    private void UpdateDarkBeam()
    {
        lineRenderer.SetPosition(0, originPoint.position);

        lineRenderer.SetPosition(1, endPoint.position);

        endPoint.position = lineRenderer.GetPosition(1);

        beamDirection = (Vector2)endPoint.position - (Vector2)originPoint.position;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)originPoint.position, beamDirection.normalized, beamDirection.magnitude);

        particals.transform.position = hit.point;

        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
        }

        if (hit.collider == playerCollider)
        {
            Debug.Log("Hit the Player");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector2 originPos = originPoint.position;
        Vector2 endPos = endPoint.position;

        Gizmos.DrawLine(originPos, endPos);
    }
#endif
}
