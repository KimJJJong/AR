using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBehaviorTree : MonoBehaviour
{
    public int favorability = 0;
    public string talkContent;

    // Dictionary 호감도 && 대화 매칭
    private Dictionary<int, List<string>> dialogueDictionary = new Dictionary<int, List<string>>();

    void Start()
    {
        // Resources에서 파일을 불러오기
        LoadDialogueFromResources("Dialogue");
        Debug.Log("현 호감도 : " + favorability);
    }

    // CSV 파일을 Resources에서 로드하는 메서드
    void LoadDialogueFromResources(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName); // Resources 폴더에서 CSV 파일 불러오기

        if (csvFile == null)
        {
            Debug.LogError("CSV 파일을 찾을 수 없습니다: " + fileName);
            return;
        }

        // CSV 내용을 파싱
        string[] lines = csvFile.text.Split('\n');  // 각 줄로 나누기
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');

            if (parts.Length == 2 && int.TryParse(parts[0], out int favorabilityLevel))
            {
                if (!dialogueDictionary.ContainsKey(favorabilityLevel))
                {
                    dialogueDictionary[favorabilityLevel] = new List<string>();
                }

                // 대답을 리스트에 추가
                dialogueDictionary[favorabilityLevel].Add(parts[1]);
            }
        }

        Debug.Log("대화 내용을 CSV에서 불러왔습니다.");
    }

    // Behavior Tree 실행
    public void RunBehaviorTree()
    {
        BehaviorNode tree = new SelectorNode(
            new SequenceNode(
                new ConditionNode(() => favorability == 3),
                new ActionNode(() => SpeakRandomDialogue(3)) // 호감도 3 대화 실행
            ),
            new SequenceNode(
                new ConditionNode(() => favorability == 2),
                new ActionNode(() => SpeakRandomDialogue(2)) // 호감도 2 대화 실행
            ),
            new SequenceNode(
                new ConditionNode(() => favorability == 1),
                new ActionNode(() => SpeakRandomDialogue(1)) // 호감도 1 대화 실행
            ),
            new ActionNode(() => SpeakRandomDialogue(0))
        );

        // Behavior Tree 실행
        tree.Execute();
    }

    // 특정 호감도에 맞는 대화를 실행
    bool SpeakRandomDialogue(int favorabilityLevel)
    {
        if (dialogueDictionary.ContainsKey(favorabilityLevel))
        {
            List<string> dialogues = dialogueDictionary[favorabilityLevel];

            int randomIndex = UnityEngine.Random.Range(0, dialogues.Count);
            string selectedDialogue = dialogues[randomIndex];

            Debug.Log("대화: " + selectedDialogue);
            talkContent = selectedDialogue;
            return true;
        }
        else
        {
            Debug.LogError("해당 호감도에 대한 대화가 없습니다.");
            return false;
        }
    }

    // 호감도 설정
    public void SetFavorability(int level)
    {
        favorability = level;
        Debug.Log("호감도가 " + favorability + "로 설정되었습니다.");

        //RunBehaviorTree(); // 필요시 Behavior Tree 실행
    }
}
