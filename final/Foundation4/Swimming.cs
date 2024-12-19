namespace Foundation4
{
    public class Swimming : Activity
    {
        private int _laps;

        public Swimming(string date, int lengthMinutes, int laps)
            : base(date, lengthMinutes)
        {
            _laps = laps;
        }

        public override double GetDistance()
        {
            return _laps * 50 / 1000.0 * 0.62; // Convert laps to miles
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
            return $"{GetDate()} {this.GetType().Name} ({GetLengthMinutes()} min): Distance {GetDistance():0.0} miles, Speed {GetSpeed():0.0} mph, Pace {GetPace():0.0} min per mile, Laps {_laps}";
        }
    }
}