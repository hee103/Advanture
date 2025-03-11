using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f; // üũ�� ����
    private float lastCheckTime; //������ üũ �ð�
    public float maxCheckDistance; // �ִ� üũ �Ÿ�
    public LayerMask layerMask;

    public GameObject curInteractGameObject;// ���� ��ȣ�ۿ��� �� �ִ� ������Ʈ
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
        // checkRate ������ �ΰ� ��ȣ�ۿ� ������ ������Ʈ���� üũ
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            // ȭ�� �߾ӿ��� ���� ĳ��Ʈ �߻�
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            Debug.DrawRay(ray.origin, ray.direction * maxCheckDistance, Color.red, 1f);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // ���� ĳ��Ʈ�� �浹�� ������Ʈ�� ���̾� ����ũ�� ���ԵǾ� �ִ��� Ȯ��
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    // ��ȣ�ۿ��� ������Ʈ�� ������ ��ȣ�ۿ��ߴ� ������Ʈ���� Ȯ��
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

    // ��ȣ�ۿ� ������Ʈ �ؽ�Ʈ ����
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    // 'Use' ��ư�� ������ ���� ��ȣ�ۿ� ó��
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
