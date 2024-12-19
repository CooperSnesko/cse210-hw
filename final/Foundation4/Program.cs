using System;
using System.Collections.Generic;

namespace Foundation4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create activities
            Running running = new Running("03 Nov 2022", 30, 3.0);
            Cycling cycling = new Cycling("04 Nov 2022", 45, 15.0);
            Swimming swimming = new Swimming("05 Nov 2022", 25, 20);

            // Add activities to a list
            List<Activity> activities = new List<Activity> { running, cycling, swimming };

            // Display summaries for each activity
            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}