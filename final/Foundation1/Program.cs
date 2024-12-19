using System;
using System.Collections.Generic;

namespace YouTubeVideoTracking
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Video> videos = new List<Video>();

            // Fake videos
            Video video1 = new Video("tech tips", "Mrtech", 600);
            video1.AddComment(new Comment("Alice", "Great tutorial!"));
            video1.AddComment(new Comment("Bob", "Very helpful thanks!"));
            video1.AddComment(new Comment("Charlie", "Great tech tips!"));

            Video video2 = new Video("Fitness tips", "Mrfitness", 300);
            video2.AddComment(new Comment("Dana", "Super motivating!"));
            video2.AddComment(new Comment("Eve", "Loved the energy!"));

            Video video3 = new Video("Cooking tips", "Mrchef", 900);
            video3.AddComment(new Comment("Frank", "Perfect for beginners"));
            video3.AddComment(new Comment("Grace", "Can't wait to try these recipes."));
            video3.AddComment(new Comment("Hank", "Please make a video on desserts!"));

            videos.Add(video1);
            videos.Add(video2);
            videos.Add(video3);

            foreach (var video in videos)
            {
                Console.WriteLine(video);
                Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

                foreach (var comment in video.GetComments())
                {
                    Console.WriteLine($"  - {comment}");
                }

                Console.WriteLine();
            }
        }
    }
}