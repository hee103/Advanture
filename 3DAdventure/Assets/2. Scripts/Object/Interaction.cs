using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // 체크할 간격
    private float lastCheckTime; //마지막 체크 시간
    public float maxCheckDistance; // 최대 체크 거리
    public LayerMask layerMask;

    public GameObject curInteractGameObject;// 현재 상호작용할 수 있는 오브젝트
    private IInteractable curInteractable; 

    public TextMeshProUGUI promptText;
    private Camera camera;

    void Start()
    {
        camera = Camera.main;
      
    }

    // Update is called once per frame
    void Update()
    {
        // checkRate 간격을 두고 상호작용 가능한 오브젝트인지 체크
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            // 화면 중앙에서 레이 캐스트 발사
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red, 1f);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // 레이 캐스트로 충돌한 오브젝트가 레이어 마스크에 포함되어 있는지 확인
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    // 상호작용할 오브젝트가 이전에 상호작용했던 오브젝트인지 확인
                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();

                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    // 상호작용 프롬프트 텍스트 설정
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    // 'Use' 버튼을 눌렀을 때의 상호작용 처리
    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnUse();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
