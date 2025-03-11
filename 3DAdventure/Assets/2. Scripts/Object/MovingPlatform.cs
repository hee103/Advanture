using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed = 2f; // �����̴� �ӵ�  
    public float platformdistance = 3f; // �̵� �Ÿ�
    private Vector3 startPos; 

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }
    void MovePlatform() // �÷����� �����̰� �ϴ� �Լ�
    {
        float move = Mathf.PingPong(Time.time * platformSpeed, platformdistance * 2) - platformdistance;
        transform.position = startPos + new Vector3(move, 0, 0);
    }
    private void OnCollisionEnter(Collision collision) // �浹�ϰ� ���� �� �÷��̾��� ��ġ�� �÷����� ����
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); 
        }
    }

    private void OnCollisionExit(Collision collision) // �浹�ϰ� ���� ���� �� �÷��̾�� �÷����� ��ġ�� ������ ���� 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); 
        }
    }
}
