using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{
    Animator animator;
    public float moveSpeed = 5f; // 이동 속도
    private Vector3 targetPosition; // 목표 위치
    private bool isMoving = false; // 이동 중인지 여부

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    public void LerpPosition(Vector3 pos)
    {
        targetPosition = pos; // 목표 위치 설정
        isMoving = true; // 이동 시작

        // 이동 전, 목표 위치 방향을 바라보도록 회전 설정
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // Y축 고정, 즉 위아래로 회전하지 않도록 설정
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = lookRotation; // 회전 적용
        }

        CharacterState(ECharacterState.Walk); // 캐릭터 상태를 Walk로 변경
    }

    private void MoveTowardsTarget()
    {
        // 목표 위치에 거의 도달했을 때
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = transform.position;
//            transform.position = targetPosition; // 목표 위치에 정확히 설정
            isMoving = false; // 이동 완료
            CharacterState(ECharacterState.Idle); // 캐릭터 상태를 Idle로 변경
        }
        else
        {
            // 현재 위치에서 목표 위치로 부드럽게 이동
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    public void CharacterState(ECharacterState characterState)
    {
        switch (characterState)
        {
            case ECharacterState.Idle:
                animator.SetInteger("animation", 1 );
                Debug.Log("Character is Idle");
                break;
            case ECharacterState.Walk:
                animator.SetInteger("animation",15);
                Debug.Log("Character is Walking");
                
                break;
            case ECharacterState.Run:
                Debug.Log("Character is Running");
                break;
            case ECharacterState.Reaction:
                StartCoroutine(Reaction());
                Debug.Log("Character is Reacting");
                break;
        }
    }
    IEnumerator Reaction()
    {
        animator.SetInteger("animation", 13);
        yield return new WaitForSeconds(1);
        CharacterState(ECharacterState.Idle);
    }
}
