namespace DogShelter.Interfaces;

public interface IDonationService
{
    bool ProcessDonation(string donorName, decimal amount, string message);
}


