using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float platformSpeed = 2f; // 움직이는 속도  
    public float platformdistance = 3f; // 이동 거리
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
    void MovePlatform() // 플랫폼을 움직이게 하는 함수
    {
        float move = Mathf.PingPong(Time.time * platformSpeed, platformdistance * 2) - platformdistance;
        transform.position = startPos + new Vector3(move, 0, 0);
    }
    private void OnCollisionEnter(Collision collision) // 충돌하고 있을 때 플레이어의 위치는 플랫폼을 따라감
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); 
        }
    }

    private void OnCollisionExit(Collision collision) // 충돌하고 있지 않을 때 플레이어는 플랫폼의 위치를 따라가지 않음 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); 
        }
    }
}
