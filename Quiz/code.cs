using System;
using System.Collections.Generic;
using System.IO;

namespace OnlineQuizSystem
{
    // Question class
    class Question
    {
        public string QuestionText { get; set; }
        public string[] Options { get; set; } = new string[4];
        public char CorrectOption { get; set; }

        public Question() { }

        public Question(string questionText, string[] options, char correctOption)
        {
            QuestionText = questionText;
            Options = options;
            CorrectOption = Char.ToUpper(correctOption);
        }

        public void DisplayQuestion(int qNumber)
        {
            Console.WriteLine($"\nQuestion {qNumber}: {QuestionText}");
            char optionChar = 'A';
            foreach (var option in Options)
            {
                Console.WriteLine($"{optionChar}. {option}");
                optionChar++;
            }
        }

        public bool CheckAnswer(char userAnswer)
        {
            return Char.ToUpper(userAnswer) == CorrectOption;
        }

        public string ToCSV()
        {
            return $"{QuestionText},{Options[0]},{Options[1]},{Options[2]},{Options[3]},{CorrectOption}";
        }

        public static Question FromCSV(string csvLine)
        {
            var parts = csvLine.Split(',');
            return new Question(parts[0], new string[] { parts[1], parts[2], parts[3], parts[4] }, parts[5][0]);
        }
    }

    // Quiz class
    class Quiz
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        public void AddQuestion(Question q)
        {
            Questions.Add(q);
            SaveQuestionsToFile();
        }

        public void ViewQuestions()
        {
            if (Questions.Count == 0)
            {
                Console.WriteLine("No questions available!");
                return;
            }
            int qNumber = 1;
            foreach (var q in Questions)
            {
                Console.WriteLine($"{qNumber}. {q.QuestionText} (Answer: {q.CorrectOption})");
                qNumber++;
            }
        }

        public void StartQuiz(string playerName)
        {
            if (Questions.Count == 0)
            {
                Console.WriteLine("No questions to take the quiz!");
                return;
            }

            int score = 0;
            int qNumber = 1;
            foreach (var question in Questions)
            {
                question.DisplayQuestion(qNumber);
                Console.Write("Your Answer (A/B/C/D): ");
                char userAnswer = Console.ReadKey().KeyChar;
                Console.WriteLine();
                if (question.CheckAnswer(userAnswer))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct!\n");
                    Console.ResetColor();
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Wrong! Correct Answer: {question.CorrectOption}\n");
                    Console.ResetColor();
                }
                qNumber++;
            }
            Console.WriteLine($"Quiz Completed! {playerName}'s Score: {score} / {Questions.Count}\n");
        }

        // File handling
        private readonly string fileName = "questions.csv";

        public void LoadQuestionsFromFile()
        {
            if (!File.Exists(fileName)) return;
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
                Questions.Add(Question.FromCSV(line));
            }
        }

        public void SaveQuestionsToFile()
        {
            List<string> lines = new List<string>();
            foreach (var q in Questions)
            {
                lines.Add(q.ToCSV());
            }
            File.WriteAllLines(fileName, lines);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Quiz quiz = new Quiz();
            quiz.LoadQuestionsFromFile();

            while (true)
            {
                Console.WriteLine("\n===== ONLINE QUIZ SYSTEM =====");
                Console.WriteLine("1. Take Quiz");
                Console.WriteLine("2. Add Question");
                Console.WriteLine("3. View Questions");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter your name: ");
                        string playerName = Console.ReadLine();
                        quiz.StartQuiz(playerName);
                        break;

                    case "2":
                        Console.Write("Enter question text: ");
                        string text = Console.ReadLine();
                        string[] options = new string[4];
                        for (int i = 0; i < 4; i++)
                        {
                            Console.Write($"Option {(char)('A' + i)}: ");
                            options[i] = Console.ReadLine();
                        }
                        Console.Write("Enter correct option (A/B/C/D): ");
                        char correct = Console.ReadKey().KeyChar;
                        Console.WriteLine();
                        quiz.AddQuestion(new Question(text, options, correct));
                        Console.WriteLine("Question added successfully!");
                        break;

                    case "3":
                        quiz.ViewQuestions();
                        break;

                    case "4":
                        Console.WriteLine("Exiting Quiz System...");
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again.");
                        break;
                }
            }
        }
    }
}
