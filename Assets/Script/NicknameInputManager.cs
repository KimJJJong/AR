using UnityEngine;
using UnityEngine.UI;

public class NicknameInputManager : MonoBehaviour
{
    // Input Field를 연결할 변수
    public InputField nicknameInputField;

    // 유저의 닉네임을 저장할 변수
    private string userNickname;

    // 닉네임을 저장하는 메서드
    public void GetNickname()
    {
        // Input Field에서 입력된 텍스트를 userNickname 변수에 저장
        userNickname = nicknameInputField.text;
        //Debug.Log("User Nickname: " + userNickname);
    }
}
