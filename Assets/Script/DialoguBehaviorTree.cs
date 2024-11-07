using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviorTree : MonoBehaviour
{
    public int favorability = 0;
    public string talkContent;

    // Dictionary ȣ���� && ��ȭ ��Ī
    private Dictionary<int, List<string>> dialogueDictionary = new Dictionary<int, List<string>>();

    void Start()
    {
        // Resources���� ������ �ҷ�����
        LoadDialogueFromResources("Dialogue");
        Debug.Log("�� ȣ���� : " + favorability);
    }

    // CSV ������ Resources���� �ε��ϴ� �޼���
    void LoadDialogueFromResources(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName); // Resources �������� CSV ���� �ҷ�����

        if (csvFile == null)
        {
            Debug.LogError("CSV ������ ã�� �� �����ϴ�: " + fileName);
            return;
        }

        // CSV ������ �Ľ�
        string[] lines = csvFile.text.Split('\n');  // �� �ٷ� ������
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');

            if (parts.Length == 2 && int.TryParse(parts[0], out int favorabilityLevel))
            {
                if (!dialogueDictionary.ContainsKey(favorabilityLevel))
                {
                    dialogueDictionary[favorabilityLevel] = new List<string>();
                }

                // ����� ����Ʈ�� �߰�
                dialogueDictionary[favorabilityLevel].Add(parts[1]);
            }
        }

        Debug.Log("��ȭ ������ CSV���� �ҷ��Խ��ϴ�.");
    }

    // Behavior Tree ����
    public void RunBehaviorTree()
    {
        BehaviorNode tree = new SelectorNode(
            new SequenceNode(
                new ConditionNode(() => favorability == 3),
                new ActionNode(() => SpeakRandomDialogue(3)) // ȣ���� 3 ��ȭ ����
            ),
            new SequenceNode(
                new ConditionNode(() => favorability == 2),
                new ActionNode(() => SpeakRandomDialogue(2)) // ȣ���� 2 ��ȭ ����
            ),
            new SequenceNode(
                new ConditionNode(() => favorability == 1),
                new ActionNode(() => SpeakRandomDialogue(1)) // ȣ���� 1 ��ȭ ����
            ),
            new ActionNode(() => SpeakRandomDialogue(0))
        );

        // Behavior Tree ����
        tree.Execute();
    }

    // Ư�� ȣ������ �´� ��ȭ�� ����
    bool SpeakRandomDialogue(int favorabilityLevel)
    {
        if (dialogueDictionary.ContainsKey(favorabilityLevel))
        {
            List<string> dialogues = dialogueDictionary[favorabilityLevel];

            int randomIndex = UnityEngine.Random.Range(0, dialogues.Count);
            string selectedDialogue = dialogues[randomIndex];

            Debug.Log("��ȭ: " + selectedDialogue);
            talkContent = selectedDialogue;
            return true;
        }
        else
        {
            Debug.LogError("�ش� ȣ������ ���� ��ȭ�� �����ϴ�.");
            return false;
        }
    }

    // ȣ���� ����
    public void SetFavorability(int level)
    {
        favorability = level;
        Debug.Log("ȣ������ " + favorability + "�� �����Ǿ����ϴ�.");

        //RunBehaviorTree(); // �ʿ�� Behavior Tree ����
    }
}
