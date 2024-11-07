using System.IO;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    // 테스트 함수
    void Start()
    {
        // 파일 경로 설정
        string filePath = Path.Combine(Application.persistentDataPath, "Dialogue.csv");

        // CSV 파일에서 대화 내용 읽어오기
        string[] dialogues = LoadDialogueFromCSV(filePath);
        if (dialogues != null)
        {
            foreach (var dialogue in dialogues)
            {
                Debug.Log("CSV에서 불러온 대화: " + dialogue);
            }
        }
    }

    // CSV 파일에서 대화 내용을 읽어오는 함수
    public string[] LoadDialogueFromCSV(string filePath)
    {
        // 파일이 존재하지 않을 경우 새로 생성
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("파일이 존재하지 않음, 새 파일을 생성합니다: " + filePath);
            CreateDefaultCSV(filePath);
        }

        // CSV 파일의 모든 줄을 읽어서 배열로 반환
        return File.ReadAllLines(filePath);
    }

    // 기본적인 CSV 파일을 생성하는 함수
    public void CreateDefaultCSV(string filePath)
    {
        string[] defaultDialogues = {
            "안녕하세요!",
            "이 파일은 새로 생성되었습니다.",
            "대화를 추가할 수 있습니다."
        };

        // 새 파일 생성 및 기본 대화 저장
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string line in defaultDialogues)
            {
                writer.WriteLine(line);
            }
        }

        Debug.Log("새 CSV 파일 생성 완료: " + filePath);
    }
}
