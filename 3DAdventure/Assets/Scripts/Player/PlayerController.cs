using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    public float jumpPower;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurXRot;
    public float lookSensitivity;

    public float jumpingZone;
    private int jumpCount = 0;
    private int maxJumpCount = 1;
    private Vector2 mouseDelta;

    private float fallThreshold = -10f; // 낙하 피해를 받을 최소 속도
    private float currentVelocity;

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

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && jumpCount < maxJumpCount)
        {
            rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount++; // 점프할 때마다 증가
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = rigidbody.velocity.y;

        rigidbody.velocity = dir;
    }

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
                //Debug.DrawRay(rays[i].origin, rays[i].direction * 1.2f, Color.red);
                return true;
            }
            //else
            //{
            //    Debug.Log("바닥 감지가 안된다용");
            //}
        }

        return false;
    }
    private bool IsOnGroundLayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.2f))
        {
            return hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground");
        }
        return false;
    }
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



}
