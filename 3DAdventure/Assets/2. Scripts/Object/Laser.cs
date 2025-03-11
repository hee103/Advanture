using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public LayerMask hitLayers; // 충돌 감지할 레이어
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
    }

    void Update()
    {
        Vector3 direction = (pointB.position - pointA.position).normalized;
        float distance = Vector3.Distance(pointA.position, pointB.position);

        // 레이저 발사 (Raycast)
        RaycastHit hit;
        if (Physics.Raycast(pointA.position, direction, out hit, distance, hitLayers))
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerCondition>()?.TakeDamage(10);
            }
        }
        else
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, pointB.position);
        }
    }
}
