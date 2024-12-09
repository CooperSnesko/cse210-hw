using System;
using System.Collections.Generic;

namespace YouTubeVideoTracking
{
    
    public class Comment
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public Comment(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Name}: {Text}";
        }
    }

    
    public class Video
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Length { get; set; } // Length in seconds
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        
        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        
        public List<Comment> GetComments()
        {
            return Comments;
        }

        public override string ToString()
        {
            return $"{Title} by {Author} ({Length} seconds)";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Video> videos = new List<Video>();

            // fake vids
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