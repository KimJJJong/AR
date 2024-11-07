using System.IO;
using UnityEngine;

public class DataReader : MonoBehaviour
{
    // �׽�Ʈ �Լ�
    void Start()
    {
        // ���� ��� ����
        string filePath = Path.Combine(Application.persistentDataPath, "Dialogue.csv");

        // CSV ���Ͽ��� ��ȭ ���� �о����
        string[] dialogues = LoadDialogueFromCSV(filePath);
        if (dialogues != null)
        {
            foreach (var dialogue in dialogues)
            {
                Debug.Log("CSV���� �ҷ��� ��ȭ: " + dialogue);
            }
        }
    }

    // CSV ���Ͽ��� ��ȭ ������ �о���� �Լ�
    public string[] LoadDialogueFromCSV(string filePath)
    {
        // ������ �������� ���� ��� ���� ����
        if (!File.Exists(filePath))
        {
            Debug.LogWarning("������ �������� ����, �� ������ �����մϴ�: " + filePath);
            CreateDefaultCSV(filePath);
        }

        // CSV ������ ��� ���� �о �迭�� ��ȯ
        return File.ReadAllLines(filePath);
    }

    // �⺻���� CSV ������ �����ϴ� �Լ�
    public void CreateDefaultCSV(string filePath)
    {
        string[] defaultDialogues = {
            "�ȳ��ϼ���!",
            "�� ������ ���� �����Ǿ����ϴ�.",
            "��ȭ�� �߰��� �� �ֽ��ϴ�."
        };

        // �� ���� ���� �� �⺻ ��ȭ ����
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (string line in defaultDialogues)
            {
                writer.WriteLine(line);
            }
        }

        Debug.Log("�� CSV ���� ���� �Ϸ�: " + filePath);
    }
}
