using System;
using System.Collections.Generic;

// Activity
public abstract class Activity
{
    private string date;
    private int lengthMinutes;

    public Activity(string date, int lengthMinutes)
    {
        this.date = date;
        this.lengthMinutes = lengthMinutes;
    }

    public int GetLengthMinutes()
    {
        return lengthMinutes;
    }

    public string GetDate()
    {
        return date;
    }

    public abstract double GetDistance(); 

    public abstract double GetSpeed(); 

    public abstract double GetPace(); 

    public virtual string GetSummary()
    {
        return $"{date} {this.GetType().Name} ({lengthMinutes} min): Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace {GetPace():0.0} min per mile";
    }
}

// Running
public class Running : Activity
{
    private double distanceMiles;

    public Running(string date, int lengthMinutes, double distanceMiles) 
        : base(date, lengthMinutes)
    {
        this.distanceMiles = distanceMiles;
    }

    public override double GetDistance()
    {
        return distanceMiles;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetLengthMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetLengthMinutes() / GetDistance();
    }
}

// Cycling
public class Cycling : Activity
{
    private double speedMph;

    public Cycling(string date, int lengthMinutes, double speedMph) 
        : base(date, lengthMinutes)
    {
        this.speedMph = speedMph;
    }

    public override double GetDistance()
    {
        return (speedMph * GetLengthMinutes()) / 60;
    }

    public override double GetSpeed()
    {
        return speedMph;
    }

    public override double GetPace()
    {
        return 60 / GetSpeed();
    }
}

// Swimming
public class Swimming : Activity
{
    private int laps;

    public Swimming(string date, int lengthMinutes, int laps) 
        : base(date, lengthMinutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0 * 0.62;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetLengthMinutes()) * 60;
    }

    public override double GetPace()
    {
        return GetLengthMinutes() / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{GetDate()} {this.GetType().Name} ({GetLengthMinutes()} min): Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace {GetPace():0.0} min per mile, Laps {laps}";
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
    
        Running running = new Running("03 Nov 2022", 30, 3.0);
        Cycling cycling = new Cycling("04 Nov 2022", 45, 15.0);
        Swimming swimming = new Swimming("05 Nov 2022", 25, 20);

        
        List<Activity> activities = new List<Activity> { running, cycling, swimming };

    
        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}