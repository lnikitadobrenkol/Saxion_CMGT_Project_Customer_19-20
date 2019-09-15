using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class MessageMenuController : MonoBehaviour
{
    public Question[] questions;

    private static List<Question> unansweredQuestions;

    private Question currentQuestion;

    [SerializeField]
    private Text questionText;

    private void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
    }

    private void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);

        currentQuestion = unansweredQuestions[randomQuestionIndex];

        questionText.text = currentQuestion.question;
    }

    public void UserSelect()
    {
        SetCurrentQuestion();
    }
}
