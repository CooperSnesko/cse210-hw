using System;

namespace YouTubeVideoTracking
{
    public class Comment
    {
        private string _name;
        private string _text;

        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public string Text
        {
            get => _text;
            set => _text = value;
        }

        public Comment(string name, string text)
        {
            _name = name;
            _text = text;
        }

        public override string ToString()
        {
            return $"{_name}: {_text}";
        }
    }
}