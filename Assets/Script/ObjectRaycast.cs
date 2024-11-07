//using TMPro;
using System.Collections;
using UnityEngine;

public class ObjectRaycast : MonoBehaviour
{
    public Camera arCamera; 
    //public GameObject spawnObject;
    public GameObject spawnCh;
    public TalkManager talkManager;
    private CharacterMovement characterMovement;




    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            Vector2 screenPosition = Input.mousePosition;
            Ray ray = arCamera.ScreenPointToRay(screenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Raycast hit object: " + hitObject.name);

                Vector3 hitPosition = hit.point;
                //Debug.Log("Hit Position: " + hitPosition);

                if (GameManager.Instance.canSpawn)
                {
                    SpawnCharacter(hitPosition);
                }
                else if (hitObject.CompareTag("Character"))
                {
                    // Run behavior tree and get the dialogue
                    var talk = GameManager.Instance.behaviorTree;
                    talk.RunBehaviorTree();
                    string dialogue = talk.talkContent; // ��ȭ ������ ����

                    // Ch ����ڸ� �ٶ󺸰� �ϱ�
                    StartCoroutine(LookAtCameraSmoothly(hitObject));
                    characterMovement.CharacterState(ECharacterState.Reaction);

                    // TalkBox�� ĳ���� ���� �����ϰ� ī�޶� �ٶ󺸰� ����
                    Vector3 talkBoxPosition = hitObject.transform.position + new Vector3(0, 0.8f,0); // ĳ���� ���� TalkBox ��ġ ����
                    Vector3 directionToCamera = (arCamera.transform.position - talkBoxPosition).normalized;

                    // TalkBox ����
                    talkManager.TalkBox(dialogue , talkBoxPosition/*, -directionToCamera*/);
                }
                else if( spawnCh.activeSelf )
                {
                    characterMovement.LerpPosition(hitPosition);
                }
            }
        }
    }

    public void CharacterState()
    {

    }


    void SpawnCharacter(Vector3 hitPosition)
    {
       
        // ĳ���͸� �����ϰ� ī�޶� �ٶ󺸰� ȸ��
        Vector3 direction = (arCamera.transform.position - hitPosition).normalized;
        direction.y = 0; // Y�� ȸ�� ����
        spawnCh.SetActive(true);
        spawnCh.transform.position = hitPosition; //= Instantiate(spawnObject, hitPosition, Quaternion.LookRotation(direction));
        Quaternion lookPoint = Quaternion.LookRotation(direction);
        spawnCh.transform.rotation = lookPoint;
        spawnCh.transform.localScale = new Vector3(0.3f, 0.3f,0.3f);
        characterMovement = spawnCh.GetComponent<CharacterMovement>();
        GameManager.Instance.canSpawn = false;
    }
    IEnumerator LookAtCameraSmoothly(GameObject ch)
    {
        // ī�޶� ������ ���
        Vector3 directionToCamera = (arCamera.transform.position - ch.transform.position).normalized;
        directionToCamera.y = 0; // Y�� ȸ�� ���� (���� ������ ����)
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera); // ��ǥ ȸ�� ���

        // ���� ȸ���� ��ǥ ȸ�� ���̸� �ڿ������� ȸ��
        while (Quaternion.Angle(ch.transform.rotation, targetRotation) > 0.1f) // 0.1�� ���Ϸ� ���̰� �� ������
        {
            // �ε巯�� ȸ��, Slerp�� ȸ�� ���� ���������� ����
            ch.transform.rotation = Quaternion.Slerp(ch.transform.rotation, targetRotation, Time.deltaTime * 5);
            yield return null; // ���� �����ӱ��� ���
        }

        // ���������� ��Ȯ�� �������� ����
        ch.transform.rotation = targetRotation;
    }
}
