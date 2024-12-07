using System;

class Program
{
    static void Main(string[] args)
    {
        Scripture scripture = new Scripture(new Reference("Proverbs", 3, 5, 6),
            "Trust in the Lord with all your heart and lean not on your own understanding; in all your ways submit to him, and he will make your paths straight.");

        while (!scripture.IsFullyHidden)
        {
            Console.Clear();
            Console.WriteLine(scripture.Display());
            Console.WriteLine("\nPress Enter to hide words, or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input?.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords();
        }

        Console.Clear();
        Console.WriteLine("All words are hidden. Well done!");
    }
}

public class Reference
{
    public string Book { get; private set; }
    public int StartChapter { get; private set; }
    public int StartVerse { get; private set; }
    public int? EndVerse { get; private set; }

    public Reference(string book, int startChapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        StartChapter = startChapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public override string ToString()
    {
        return EndVerse.HasValue
            ? $"{Book} {StartChapter}:{StartVerse}-{EndVerse}"
            : $"{Book} {StartChapter}:{StartVerse}";
    }
}

public class Scripture
{
    public Reference Reference { get; private set; }
    private List<Word> Words { get; set; }

    public bool IsFullyHidden => Words.All(w => w.IsHidden);

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        foreach (var word in Words.Where(w => !w.IsHidden).OrderBy(x => random.Next()).Take(3))
        {
            word.Hide();
        }
    }

    public string Display()
    {
        return $"{Reference}\n{string.Join(" ", Words.Select(w => w.Display()))}";
    }
}

public class Word
{
    private string Text { get; set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string Display()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}
