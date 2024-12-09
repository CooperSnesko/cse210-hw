using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    abstract class Activity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }

        public Activity(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void StartMessage()
        {
            Console.WriteLine($"Starting {Name} Activity");
            Console.WriteLine(Description);
            Console.Write("Enter duration (seconds): ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Prepare to begin...");
            PauseWithAnimation(3);
        }

        public void EndMessage()
        {
            Console.WriteLine("\nGood job!");
            Console.WriteLine($"You have completed the {Name} Activity for {Duration} seconds.");
            PauseWithAnimation(3);
        }

        public void PauseWithAnimation(int seconds)
        {
            DateTime end = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < end)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            Console.WriteLine();
        }

        public abstract void RunActivity();
    }

    class BreathingActivity : Activity
    {
        public BreathingActivity() : base("Breathing", "This activity will help you relax by pacing your breathing.") { }

        public override void RunActivity()
        {
            StartMessage();
            DateTime end = DateTime.Now.AddSeconds(Duration);
            while (DateTime.Now < end)
            {
                Console.WriteLine("Breathe in...");
                PauseWithAnimation(3);
                Console.WriteLine("Breathe out...");
                PauseWithAnimation(3);
            }
            EndMessage();
        }
    }

    class ReflectionActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times?",
            "What could you learn from this experience?"
        };

        public ReflectionActivity() : base("Reflection", "This activity will help you reflect on moments of strength and resilience.") { }

        public override void RunActivity()
        {
            StartMessage();
            Random random = new Random();
            Console.WriteLine(prompts[random.Next(prompts.Count)]);
            DateTime end = DateTime.Now.AddSeconds(Duration);

            while (DateTime.Now < end)
            {
                Console.WriteLine(questions[random.Next(questions.Count)]);
                PauseWithAnimation(5);
            }

            EndMessage();
        }
    }

    class ListingActivity : Activity
    {
        private List<string> prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity() : base("Listing", "This activity will help you list things you're grateful for.") { }

        public override void RunActivity()
        {
            StartMessage();
            Random random = new Random();
            Console.WriteLine(prompts[random.Next(prompts.Count)]);
            Console.WriteLine("You have a few seconds to start thinking...");
            PauseWithAnimation(5);

            Console.WriteLine("Start listing items (press Enter after each item, and type 'done' to finish):");
            int count = 0;
            DateTime end = DateTime.Now.AddSeconds(Duration);

            while (DateTime.Now < end)
            {
                string item = Console.ReadLine();
                if (item.ToLower() == "done") break;
                count++;
            }

            Console.WriteLine($"You listed {count} items.");
            EndMessage();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program Menu:");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. Quit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        new BreathingActivity().RunActivity();
                        break;
                    case "2":
                        new ReflectionActivity().RunActivity();
                        break;
                    case "3":
                        new ListingActivity().RunActivity();
                        break;
                    case "4":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }

            Console.WriteLine("Goodbye!");
        }
    }
}