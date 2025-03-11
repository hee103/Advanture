using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Transform rayOrigin; // Raycast�� ��� ��ġ
    public float detectionRange = 10f; // ���� �Ÿ�
    public GameObject laserObject;
    public GameObject warningMessage;
    public LayerMask playerLayer; // ������ ���̾� (Player�� �����ϵ��� ����)


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
