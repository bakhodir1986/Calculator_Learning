namespace Calculator.Task1
{
    public interface ITripRepository
    {
        TripDetails LoadTrip(string touristName);
    }
}
