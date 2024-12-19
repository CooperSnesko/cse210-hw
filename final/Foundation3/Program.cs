using System;

namespace Foundation3
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create addresses
            Address lectureAddress = new Address("123 Main St", "New York", "NY", "USA");
            Address receptionAddress = new Address("456 Elm Ave", "Los Angeles", "CA", "USA");
            Address outdoorAddress = new Address("789 Pine Rd", "Denver", "CO", "USA");

            // Create events
            Lecture lecture = new Lecture("Tech Trends 2024", "A lecture on the latest technology trends.", "2024-12-15", "10:00 AM", lectureAddress, "Dr. Jane Smith", 100);
            Reception reception = new Reception("Networking Night", "A night of networking with industry professionals.", "2024-12-20", "7:00 PM", receptionAddress, "rsvp@example.com");
            OutdoorGathering outdoorGathering = new OutdoorGathering("Winter Fest", "A festive outdoor gathering to celebrate the season.", "2024-12-25", "3:00 PM", outdoorAddress, "Snowy");

            Event[] events = { lecture, reception, outdoorGathering };

            // Display details for each event
            foreach (var eventItem in events)
            {
                Console.WriteLine(eventItem.GetStandardDetails());
                Console.WriteLine();
                Console.WriteLine(eventItem.GetFullDetails());
                Console.WriteLine();
                Console.WriteLine(eventItem.GetShortDescription());
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}