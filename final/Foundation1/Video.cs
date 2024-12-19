using System;
using System.Collections.Generic;

namespace YouTubeVideoTracking
{
    public class Video
    {
        private string _title;
        private string _author;
        private int _length; // Length in seconds
        private List<Comment> _comments;

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string Author
        {
            get => _author;
            set => _author = value;
        }

        public int Length
        {
            get => _length;
            set => _length = value;
        }

        public Video(string title, string author, int length)
        {
            _title = title;
            _author = author;
            _length = length;
            _comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            _comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return _comments.Count;
        }

        public List<Comment> GetComments()
        {
            return _comments;
        }

        public override string ToString()
        {
            return $"{_title} by {_author} ({_length} seconds)";
        }
    }
}