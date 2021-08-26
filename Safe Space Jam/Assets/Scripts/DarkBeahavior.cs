using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBeahavior : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform originPoint;
    public Transform endPoint;
    public Collider2D playerCollider;
    private ParticleSystem particleSystem;

    [Range (0.5f, 10f)] public float beamWidth = 1f;

    private Vector2 beamDirection;

    private void Start()
    {
        lineRenderer.startWidth = beamWidth;
        lineRenderer.endWidth = beamWidth;
        particleSystem = endPoint.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        UpdateDarkBeam();
    }

    private void UpdateDarkBeam()
    {
        lineRenderer.SetPosition(0, originPoint.position);

        lineRenderer.SetPosition(1, endPoint.position);

        particleSystem.transform.position = endPoint.position;

        endPoint.position = lineRenderer.GetPosition(1);

        beamDirection = (Vector2)endPoint.position - (Vector2)originPoint.position;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)originPoint.position, beamDirection.normalized, beamDirection.magnitude);

        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            particleSystem.transform.position = hit.point;
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

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(originPos, endPos);
    }
#endif
}
