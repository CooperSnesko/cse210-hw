namespace Foundation3
{
    public abstract class Event
    {
        private string _title;
        private string _description;
        private string _date;
        private string _time;
        private Address _address;

        public Event(string title, string description, string date, string time, Address address)
        {
            _title = title;
            _description = description;
            _date = date;
            _time = time;
            _address = address;
        }

        public string GetStandardDetails()
        {
            return $"Title: {_title}\nDescription: {_description}\nDate: {_date}\nTime: {_time}\nAddress:\n{_address.GetFullAddress()}";
        }

        public abstract string GetFullDetails();

        public string GetShortDescription()
        {
            return $"Type: {GetType().Name}\nTitle: {_title}\nDate: {_date}";
        }
    }
}