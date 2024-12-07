using System;
using System.Collections.Generic;
using System.IO;

namespace JournalApp
{
    class Entry
    {
        public string Prompt { get; set; }
        public string Response { get; set; }
        public string Date { get; set; }

        public Entry(string prompt, string response, string date)
        {
            Prompt = prompt;
            Response = response;
            Date = date;
        }

        public override string ToString()
        {
            return $"{Date} - {Prompt}\n{Response}\n";
        }
    }

    class Journal
    {
        private List<Entry> entries = new List<Entry>();
        private static Random random = new Random();
        private static List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        public void AddEntry()
        {
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine($"Prompt: {prompt}");
            Console.Write("Your response: ");
            string response = Console.ReadLine();
            string date = DateTime.Now.ToShortDateString();
            entries.Add(new Entry(prompt, response, date));
            Console.WriteLine("Entry added successfully!\n");
        }

        public void DisplayEntries()
        {
            if (entries.Count == 0)
            {
                Console.WriteLine("No journal entries found.\n");
                return;
            }

            Console.WriteLine("Journal Entries:\n");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }

        public void SaveToFile()
        {
            Console.Write("Enter filename to save: ");
            string filename = Console.ReadLine();
            using (StreamWriter writer = new StreamWriter(filename))
            {
                foreach (var entry in entries)
                {
                    writer.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
                }
            }
            Console.WriteLine("Journal saved successfully!\n");
        }

        public void LoadFromFile()
        {
            Console.Write("Enter filename to load: ");
            string filename = Console.ReadLine();
            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.\n");
                return;
            }

            entries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (var line in lines)
            {
                string[] parts = line.Split('|');
                if (parts.Length == 3)
                {
                    entries.Add(new Entry(parts[1], parts[2], parts[0]));
                }
            }
            Console.WriteLine("Journal loaded successfully!\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Journal journal = new Journal();
            bool running = true;

            while (running)
            {
                Console.WriteLine("Journal App Menu:");
                Console.WriteLine("1. Write a new entry");
                Console.WriteLine("2. Display journal");
                Console.WriteLine("3. Save journal to file");
                Console.WriteLine("4. Load journal from file");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        journal.AddEntry();
                        break;
                    case "2":
                        journal.DisplayEntries();
                        break;
                    case "3":
                        journal.SaveToFile();
                        break;
                    case "4":
                        journal.LoadFromFile();
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.\n");
                        break;
                }
            }
            Console.WriteLine("Goodbye!");
        }
    }
}