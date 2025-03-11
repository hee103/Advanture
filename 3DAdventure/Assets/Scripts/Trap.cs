using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform rayOrigin; // Raycast를 쏘는 위치
    public float detectionRange = 10f; // 감지 거리
    public GameObject laserObject;
    public GameObject warningMessage;
    public LayerMask playerLayer; // 감지할 레이어 (Player만 감지하도록 설정)


    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, detectionRange, playerLayer))
        {
            Debug.DrawRay(rayOrigin.position, rayOrigin.forward * detectionRange, Color.red);
            if (hit.collider.CompareTag("Player"))
            {
                laserObject.SetActive(true);
                warningMessage.SetActive(true);
                StartCoroutine(HideWarningMessage());
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
        yield return new WaitForSeconds(2f); 
        warningMessage.SetActive(false); 
    }
}
