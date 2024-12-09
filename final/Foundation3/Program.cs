using System;


public class Address
{
    private string street;
    private string city;
    private string stateOrProvince;
    private string country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateOrProvince = stateOrProvince;
        this.country = country;
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {stateOrProvince}\n{country}";
    }
}


public abstract class Event
{
    private string title;
    private string description;
    private string date;
    private string time;
    private Address address;

    public Event(string title, string description, string date, string time, Address address)
    {
        this.title = title;
        this.description = description;
        this.date = date;
        this.time = time;
        this.address = address;
    }

    public string GetStandardDetails()
    {
        return $"Title: {title}\nDescription: {description}\nDate: {date}\nTime: {time}\nAddress:\n{address.GetFullAddress()}";
    }

    public abstract string GetFullDetails();

    public string GetShortDescription()
    {
        return $"Type: {GetType().Name}\nTitle: {title}\nDate: {date}";
    }
}


public class Lecture : Event
{
    private string speaker;
    private int capacity;

    public Lecture(string title, string description, string date, string time, Address address, string speaker, int capacity)
        : base(title, description, date, time, address)
    {
        this.speaker = speaker;
        this.capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Lecture\nSpeaker: {speaker}\nCapacity: {capacity}";
    }
}


public class Reception : Event
{
    private string rsvpEmail;

    public Reception(string title, string description, string date, string time, Address address, string rsvpEmail)
        : base(title, description, date, time, address)
    {
        this.rsvpEmail = rsvpEmail;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Reception\nRSVP Email: {rsvpEmail}";
    }
}


public class OutdoorGathering : Event
{
    private string weatherForecast;

    public OutdoorGathering(string title, string description, string date, string time, Address address, string weatherForecast)
        : base(title, description, date, time, address)
    {
        this.weatherForecast = weatherForecast;
    }

    public override string GetFullDetails()
    {
        return $"{GetStandardDetails()}\nType: Outdoor Gathering\nWeather: {weatherForecast}";
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        
        Address lectureAddress = new Address("123 Main St", "New York", "NY", "USA");
        Address receptionAddress = new Address("456 Elm Ave", "Los Angeles", "CA", "USA");
        Address outdoorAddress = new Address("789 Pine Rd", "Denver", "CO", "USA");

        
        Lecture lecture = new Lecture("Tech Trends 2024", "A lecture on the latest technology trends.", "2024-12-15", "10:00 AM", lectureAddress, "Dr. Jane Smith", 100);
        Reception reception = new Reception("Networking Night", "An night of networking with industry professionals.", "2024-12-20", "7:00 PM", receptionAddress, "rsvp@example.com");
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