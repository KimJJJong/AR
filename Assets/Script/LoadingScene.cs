using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    public GameObject bird;    // 가로로 이동할 사각형
    public GameObject egg; // 아래로 이동할 오브젝트
    public GameObject story;
    public GameObject petNaming;
    public GameObject effect;

    public float speed = 300f;        // 사각형 이동 속도
    public float verticalSpeed = 300f; // 수직 오브젝트 이동 속도
    public float stopYPosition = -350f; // 오브젝트가 멈출 y 좌표 (원하는 값으로 설정)

    private bool eggActivated = false;

    void Start()
    {
        story.SetActive(true);
        petNaming.SetActive(false);
        egg.SetActive(false);
        effect.SetActive(false);
    }

    void Update()
    {
        if (story.activeSelf) {
            bird.transform.Translate(Vector3.left * speed * Time.deltaTime);

            if (!eggActivated && bird.transform.position.x <= 300)
            {
                egg.SetActive(true);
                eggActivated = true; // 한 번만 실행되도록 설정
            }

            if (bird.transform.position.x > Screen.width + bird.GetComponent<RectTransform>().rect.width)
            {
                bird.SetActive(false);
            }

            if (eggActivated)
            {
                Movegg();
            }
        }
        
    }

    // 수직 오브젝트를 아래로 이동시키는 함수
    void Movegg()
    {
        // 아래로 이동
        egg.transform.Translate(Vector3.down * verticalSpeed * Time.deltaTime);

        // 오브젝트가 stopYPosition에 도달했을 때 멈춤
        if (egg.transform.localPosition.y <= stopYPosition) // Y좌표가 stopYPosition보다 작거나 같을 때
        {
            // 오브젝트 위치를 정확하게 stopYPosition에 고정
            egg.transform.localPosition = new Vector3(0, stopYPosition, 0);

            // 움직임을 멈추기 위해 더 이상 Translate 호출을 하지 않음
            verticalSpeed = 0f; // 속도를 0으로 설정하여 멈추게 함
            egg.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            StartCoroutine(WaitAndChangeScene());
        }
    }

    IEnumerator WaitAndChangeScene()
    {
        // 1초 대기
        yield return new WaitForSeconds(1f);

        story.SetActive(false);
        petNaming.SetActive(true);
        effect.SetActive(true);

        yield return new WaitForSeconds(2f);
        effect.SetActive(false);

        //UnityEngine.SceneManagement.SceneManager.LoadScene("");
    }

}
