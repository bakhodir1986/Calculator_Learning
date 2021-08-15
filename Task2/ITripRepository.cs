namespace Calculator.Task2
{
    public interface ITripRepository
    {
        TripDetails LoadTrip(string touristName);
    }
}
