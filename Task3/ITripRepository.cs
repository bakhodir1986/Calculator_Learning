namespace Calculator.Task3
{
    public interface ITripRepository
    {
        TripDetails LoadTrip(string touristName);
    }
}
