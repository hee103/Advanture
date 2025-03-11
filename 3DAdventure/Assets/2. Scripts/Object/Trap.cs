using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform rayOrigin; // Raycast를 쏘는 위치
    public float detectionRange = 10f; // 감지 거리
    public GameObject laserObject; // 레이저 오브젝트
    public GameObject warningMessage; // 경고 메세지
    public LayerMask playerLayer; // 감지할 레이어 (Player만 감지하도록 설정)
    private bool hasWarned = false; // 경고 메시지가 이미 표시되었는지 여부 확인

    void Update()
    {
        // 레이캐스트로 플레이어를 감지
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, detectionRange, playerLayer))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRange, Color.red);

            if (hit.collider.CompareTag("Player") && !hasWarned)
            {
                // 플레이어가 감지되었고 아직 경고 메시지가 표시되지 않았다면
                laserObject.SetActive(true); 
                warningMessage.SetActive(true); 
                StartCoroutine(HideWarningMessage());
                hasWarned = true; 
            }
        }
    }

    void OnDrawGizmos()
    {
        if (rayOrigin != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRange);
        }
    }

    IEnumerator HideWarningMessage()
    {
        // 경고 메시지를 2초 동안 보여주고 숨김
        yield return new WaitForSeconds(2f); 
        warningMessage.SetActive(false); 
    }
}
