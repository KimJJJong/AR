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
                    string dialogue = talk.talkContent; // 대화 내용을 얻음

                    // Ch 사용자를 바라보게 하기
                    StartCoroutine(LookAtCameraSmoothly(hitObject));
                    characterMovement.CharacterState(ECharacterState.Reaction);

                    // TalkBox를 캐릭터 위에 생성하고 카메라를 바라보게 설정
                    Vector3 talkBoxPosition = hitObject.transform.position + new Vector3(0, 0.8f,0); // 캐릭터 위에 TalkBox 위치 설정
                    Vector3 directionToCamera = (arCamera.transform.position - talkBoxPosition).normalized;

                    // TalkBox 생성
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
       
        // 캐릭터를 생성하고 카메라를 바라보게 회전
        Vector3 direction = (arCamera.transform.position - hitPosition).normalized;
        direction.y = 0; // Y축 회전 방지
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
        // 카메라 방향을 계산
        Vector3 directionToCamera = (arCamera.transform.position - ch.transform.position).normalized;
        directionToCamera.y = 0; // Y축 회전 방지 (수직 방향은 유지)
        Quaternion targetRotation = Quaternion.LookRotation(directionToCamera); // 목표 회전 계산

        // 현재 회전과 목표 회전 사이를 자연스럽게 회전
        while (Quaternion.Angle(ch.transform.rotation, targetRotation) > 0.1f) // 0.1도 이하로 차이가 날 때까지
        {
            // 부드러운 회전, Slerp로 회전 값을 점진적으로 적용
            ch.transform.rotation = Quaternion.Slerp(ch.transform.rotation, targetRotation, Time.deltaTime * 5);
            yield return null; // 다음 프레임까지 대기
        }

        // 최종적으로 정확한 방향으로 고정
        ch.transform.rotation = targetRotation;
    }
}
