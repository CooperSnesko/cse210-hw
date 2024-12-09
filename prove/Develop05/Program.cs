using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Points { get; set; }
        public bool IsComplete { get; set; }

        public Goal(string name, string description, int points)
        {
            Name = name;
            Description = description;
            Points = points;
            IsComplete = false;
        }

        public abstract void MarkComplete();
        public abstract string SaveToString();
        public abstract void LoadFromData(string data);
        public abstract string DisplayStatus();
    }

    class SimpleGoal : Goal
    {
        public SimpleGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void MarkComplete()
        {
            IsComplete = true;
        }

        public override string SaveToString()
        {
            return $"SimpleGoal:{Name}|{Description}|{Points}|{IsComplete}";
        }

        public override void LoadFromData(string data)
        {
            var parts = data.Split('|');
            Name = parts[0];
            Description = parts[1];
            Points = int.Parse(parts[2]);
            IsComplete = bool.Parse(parts[3]);
        }

        public override string DisplayStatus()
        {
            return $"{(IsComplete ? "[X]" : "[ ]")} {Name} - {Description}";
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points) { }

        public override void MarkComplete()
        {
            IsComplete = false; // Always remains incomplete
        }

        public override string SaveToString()
        {
            return $"EternalGoal:{Name}|{Description}|{Points}";
        }

        public override void LoadFromData(string data)
        {
            var parts = data.Split('|');
            Name = parts[0];
            Description = parts[1];
            Points = int.Parse(parts[2]);
        }

        public override string DisplayStatus()
        {
            return $"{Name} - {Description} (Eternal)";
        }
    }

    class ChecklistGoal : Goal
    {
        public int RequiredCompletions { get; set; }
        public int CurrentCompletions { get; set; }
        public int BonusPoints { get; set; }

        public ChecklistGoal(string name, string description, int points, int requiredCompletions, int bonusPoints)
            : base(name, description, points)
        {
            RequiredCompletions = requiredCompletions;
            BonusPoints = bonusPoints;
            CurrentCompletions = 0;
        }

        public override void MarkComplete()
        {
            CurrentCompletions++;
            if (CurrentCompletions >= RequiredCompletions)
                IsComplete = true;
        }

        public override string SaveToString()
        {
            return $"ChecklistGoal:{Name}|{Description}|{Points}|{RequiredCompletions}|{CurrentCompletions}|{BonusPoints}|{IsComplete}";
        }

        public override void LoadFromData(string data)
        {
            var parts = data.Split('|');
            Name = parts[0];
            Description = parts[1];
            Points = int.Parse(parts[2]);
            RequiredCompletions = int.Parse(parts[3]);
            CurrentCompletions = int.Parse(parts[4]);
            BonusPoints = int.Parse(parts[5]);
            IsComplete = bool.Parse(parts[6]);
        }

        public override string DisplayStatus()
        {
            return $"{(IsComplete ? "[X]" : "[ ]")} {Name} - {Description} (Completed {CurrentCompletions}/{RequiredCompletions})";
        }
    }

    class GoalManager
    {
        private List<Goal> goals = new List<Goal>();
        public int TotalScore { get; private set; }

        public void AddGoal(Goal goal)
        {
            goals.Add(goal);
        }

        public void RecordEvent(int index)
        {
            if (index < 0 || index >= goals.Count)
            {
                Console.WriteLine("Invalid goal index.");
                return;
            }

            var goal = goals[index];
            goal.MarkComplete();

            int pointsEarned = goal.Points;
            if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete)
                pointsEarned += checklistGoal.BonusPoints;

            TotalScore += pointsEarned;
            Console.WriteLine($"You earned {pointsEarned} points!");
        }

        public void DisplayGoals()
        {
            for (int i = 0; i < goals.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {goals[i].DisplayStatus()}");
            }
        }

        public void SaveToFile(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                foreach (var goal in goals)
                {
                    writer.WriteLine(goal.SaveToString());
                }
                writer.WriteLine($"TotalScore:{TotalScore}");
            }
        }

        public void LoadFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            goals.Clear();
            using (var reader = new StreamReader(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("TotalScore:"))
                    {
                        TotalScore = int.Parse(line.Split(':')[1]);
                    }
                    else
                    {
                        var parts = line.Split(':');
                        string type = parts[0];
                        string data = parts[1];
                        Goal goal;

                        switch (type)
                        {
                            case "SimpleGoal":
                                goal = new SimpleGoal("", "", 0);
                                break;
                            case "EternalGoal":
                                goal = new EternalGoal("", "", 0);
                                break;
                            case "ChecklistGoal":
                                goal = new ChecklistGoal("", "", 0, 0, 0);
                                break;
                            default:
                                continue;
                        }

                        goal.LoadFromData(data);
                        goals.Add(goal);
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var manager = new GoalManager();
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Eternal Quest Menu");
                Console.WriteLine("1. Create a new goal");
                Console.WriteLine("2. Record an event");
                Console.WriteLine("3. Display goals");
                Console.WriteLine("4. Save goals");
                Console.WriteLine("5. Load goals");
                Console.WriteLine("6. Display score");
                Console.WriteLine("7. Quit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("1. Simple Goal");
                        Console.WriteLine("2. Eternal Goal");
                        Console.WriteLine("3. Checklist Goal");
                        Console.Write("Choose goal type: ");
                        string goalType = Console.ReadLine();

                        Console.Write("Enter goal name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter goal description: ");
                        string description = Console.ReadLine();
                        Console.Write("Enter goal points: ");
                        int points = int.Parse(Console.ReadLine());

                        if (goalType == "1")
                        {
                            manager.AddGoal(new SimpleGoal(name, description, points));
                        }
                        else if (goalType == "2")
                        {
                            manager.AddGoal(new EternalGoal(name, description, points));
                        }
                        else if (goalType == "3")
                        {
                            Console.Write("Enter required completions: ");
                            int required = int.Parse(Console.ReadLine());
                            Console.Write("Enter bonus points: ");
                            int bonus = int.Parse(Console.ReadLine());
                            manager.AddGoal(new ChecklistGoal(name, description, points, required, bonus));
                        }
                        break;

                    case "2":
                        manager.DisplayGoals();
                        Console.Write("Enter goal number to record: ");
                        int index = int.Parse(Console.ReadLine()) - 1;
                        manager.RecordEvent(index);
                        break;

                    case "3":
                        manager.DisplayGoals();
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.Write("Enter filename: ");
                        string saveFile = Console.ReadLine();
                        manager.SaveToFile(saveFile);
                        break;

                    case "5":
                        Console.Write("Enter filename: ");
                        string loadFile = Console.ReadLine();
                        manager.LoadFromFile(loadFile);
                        break;

                    case "6":
                        Console.WriteLine($"Total Score: {manager.TotalScore}");
                        Console.WriteLine("Press Enter to continue...");
                        Console.ReadLine();
                        break;

                    case "7":
                        running = false;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }
    }
}