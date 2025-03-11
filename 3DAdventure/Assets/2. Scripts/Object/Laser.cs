using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Transform pointA; // 레이저 시작 지점
    public Transform pointB; // 레이저 끝 지점
    public LayerMask hitLayers; // 충돌 감지할 레이어
    private LineRenderer lineRenderer; // 레이저 표시

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
    }

    void Update()
    {
        // pointA에서 pointB의 거리와 방향 계산
        Vector3 direction = (pointB.position - pointA.position).normalized;
        float distance = Vector3.Distance(pointA.position, pointB.position);

        // 레이저 발사
        RaycastHit hit;
        if (Physics.Raycast(pointA.position, direction, out hit, distance, hitLayers))
        {
            lineRenderer.SetPosition(0, pointA.position);
            lineRenderer.SetPosition(1, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                // 충돌한 객체의 태그가 Player일 때 플레이어에게 피해를 주는 코드
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
