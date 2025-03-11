using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; // 이동 속도
    private Vector2 curMovementInput; // 현재 입력값
    public float jumpPower; // 점프 힘
    public LayerMask groundLayerMask; 

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook; //카메라 최소 회전 각도
    public float maxXLook; // 카메라 최대 회전 각도
    private float camCurXRot; // 현재 x 회전 각도
    public float lookSensitivity; // 마우스 이동에 대한 회전 민감도

    public float jumpingZone; // 점프대 힘
    private int jumpCount = 0; // 현재 점프 횟수
    private int maxJumpCount = 1; // 최대 점프 횟수
    private Vector2 mouseDelta;

    private float fallThreshold = -10f; // 낙하 피해를 받을 최소 속도
    private float currentVelocity; // 현재 속도

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rigidbody;
    private PlayerCondition playerCondition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerCondition = GetComponent<PlayerCondition>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void FixedUpdate()
    {
        Move();
        if (!IsGrounded())
        {
            currentVelocity = rigidbody.velocity.y; // 낙하 속도 기록
        }
    }
    private void Update()
    {
        if (IsGrounded())
        {
            jumpCount = 0;

            // 바닥이 "Ground" 레이어인지 추가 확인
            if (IsOnGroundLayer() && currentVelocity < fallThreshold)
            {
                playerCondition.TakeDamage(50);
            }

            currentVelocity = 0;
        }
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 이동 입력 처리 (WASD 또는 화살표)
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
    }


    // 점프 입력 처리
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && jumpCount < maxJumpCount)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++; // 점프할 때마다 증가
        }
    }

    // 이동 처리
    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

    // 카메라 회전 처리
    void CameraLook()
    {
        // X축 (위아래) 회전 - 카메라만 회전
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // Y축 (좌우) 회전 - Animator 방해 무시하고 강제 적용
        float yRotation = mouseDelta.x * lookSensitivity;
        transform.rotation *= Quaternion.Euler(0, yRotation, 0);
    }


    // 바닥에 닿았는지 확인
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1f, groundLayerMask))
            {
              
                return true;
            }

        }

        return false;
    }


    // 바닥이 Ground 레이어인지 확인
    private bool IsOnGroundLayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.2f))
        {
            return hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        }
        return false;
    }

    // 점프대와 충돌 시 추가 점프력 적용
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("JumpingZone"))
        {
            rigidbody.AddForce(Vector3.up * jumpingZone, ForceMode.Impulse);
        }
    }
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle; 
    }

    // 대시 입력 처리
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            playerCondition.DashStart();
        }
        else
        {
            playerCondition.DashStop();
        }

    }

}
