using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO; 
using System.Text.RegularExpressions;

public class PersonalTest : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject TestCover;
    public GameObject TestMain;
    public GameObject TestResult;

    public Text questionText;
    public Button answerAButton;
    public Button answerBButton;
    public Slider progressBar;
    public GameObject resultPanel;
    public Text resultText;
    public Text resultInfoText;
    public Text SubResult;

    private int currentQuestionIndex = 0;
    private int totalScore = 0;
    private List<Question> questions;
    private Dictionary<string, int> categoryScores;  // 카테고리별 점수 저장

    void Start()
    {
        TestCover.SetActive(true);
        TestMain.SetActive(false);
        TestResult.SetActive(false);

        categoryScores = new Dictionary<string, int>();  // 카테고리 점수 초기화
        LoadQuestionsFromCSV();

        answerAButton.onClick.AddListener(() => SubmitAnswer("A"));
        answerBButton.onClick.AddListener(() => SubmitAnswer("B"));

        UpdateQuestion();
    }

    void LoadQuestionsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("questions"); 
        string[] data = csvFile.text.Split(new char[] { '\n' });

        questions = new List<Question>();

        for (int i = 1; i < data.Length; i++)
        {
            if (string.IsNullOrEmpty(data[i])) continue;

            string[] row = ParseCSVLine(data[i]);

            if (row.Length == 6)
            {
                string category = row[0];
                string question = row[1];
                string answerA = row[2];
                string answerB = row[3];
                int scoreA = int.Parse(row[4]);
                int scoreB = int.Parse(row[5]);

                questions.Add(new Question(category, question, answerA, answerB, scoreA, scoreB));

                if (!categoryScores.ContainsKey(category))
                {
                    categoryScores[category] = 0;  // 카테고리별 점수 초기화
                }
            }
        }
    }

    string[] ParseCSVLine(string line)
    {
        string pattern = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        string[] fields = Regex.Split(line, pattern);

        for (int i = 0; i < fields.Length; i++)
        {
            fields[i] = fields[i].TrimStart(' ', '"').TrimEnd('"');
        }

        return fields;
    }

    void UpdateQuestion()
    {
        if (currentQuestionIndex < questions.Count)
        {
            Question currentQuestion = questions[currentQuestionIndex];
            questionText.text = currentQuestion.question;
            answerAButton.GetComponentInChildren<Text>().text = currentQuestion.answerA;
            answerBButton.GetComponentInChildren<Text>().text = currentQuestion.answerB;

            progressBar.value = (float)currentQuestionIndex / (questions.Count - 1);
        }
        else
        {
            ShowResult();
        }
    }

    void SubmitAnswer(string answer)
    {
        Question currentQuestion = questions[currentQuestionIndex];

        if (answer == "A")
        {
            totalScore += currentQuestion.scoreA;
            categoryScores[currentQuestion.category] += currentQuestion.scoreA;  // 카테고리별 점수 추가
        }
        else
        {
            totalScore += currentQuestion.scoreB;
            categoryScores[currentQuestion.category] += currentQuestion.scoreB;  // 카테고리별 점수 추가
        }

        currentQuestionIndex++;
        UpdateQuestion();
    }

    public void ShowTest()
    {
        TestCover.SetActive(false);
        TestMain.SetActive(true);
        TestResult.SetActive(false);
    }

    void ShowResult()
    {
        TestCover.SetActive(false);
        TestMain.SetActive(false);
        TestResult.SetActive(true);

        resultPanel.SetActive(true);

        resultText.text = $" {totalScore}점";

        string lowestCategory = GetLowestCategory();

        PlayerPrefs.SetString("LowestCategory", lowestCategory);
        PlayerPrefs.Save();  // PlayerPrefs 데이터를 즉시 저장
        
        SubResult.text = $"자취 유형::{lowestCategory} 형";
        resultInfoText.text = $"{GetResultMessage(totalScore)}\n가장 관리가 필요한 부분은 바로 '{lowestCategory}'! 자취AR지의 귀여운 친구와 함께 습관을 만들어나가요.";
    }

    string GetResultMessage(int score)
    {
        if (score >= 85)
        {
            return "당신은 매우 정리정돈을 잘 하고, 계획적인 자취러입니다!";
        }
        else if (score >= 45)
        {
            return "당신은 어느 정도 균형 잡힌 자취 생활을 하고 있습니다. 그래도 더 멋진 자취러가 되기 위해 노력해볼까요?";
        }
        else
        {
            return "당신은 자취 생활에서 자유롭고 즉흥적인 편입니다. 하지만 정리정돈에 신경을 쓸 필요가 있을 것 같아요!";
        }
    }

    // 카테고리 중 가장 낮은 점수를 반환
    string GetLowestCategory()
    {
        string lowestCategory = null;
        int lowestScore = int.MaxValue;

        foreach (var category in categoryScores)
        {
            if (category.Value < lowestScore)
            {
                lowestScore = category.Value;
                lowestCategory = category.Key;
            }
        }

        return lowestCategory;
    }
}

// 질문 클래스
[System.Serializable]
public class Question
{
    public string category;  // 질문 유형(카테고리)
    public string question;  // 질문 내용
    public string answerA;   // 선택지 A
    public string answerB;   // 선택지 B
    public int scoreA;       // 선택지 A에 해당하는 점수
    public int scoreB;       // 선택지 B에 해당하는 점수

    public Question(string category, string question, string answerA, string answerB, int scoreA, int scoreB)
    {
        this.category = category;
        this.question = question;
        this.answerA = answerA;
        this.answerB = answerB;
        this.scoreA = scoreA;
        this.scoreB = scoreB;
    }
}
