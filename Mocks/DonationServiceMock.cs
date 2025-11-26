using DogShelter.Interfaces;

namespace DogShelter.Mocks;

public class DonationServiceMock : IDonationService
{
    private List<DonationCall> _callHistory = new List<DonationCall>();
    private int _callCount = 0;
    private decimal _totalDonations = 0;
    public bool SimulatePaymentFailure { get; set; } = false;
    public decimal MinAmount { get; set; } = 1;
    public decimal MaxAmount { get; set; } = 10000;

    public class DonationCall
    {
        public string DonorName { get; set; } = "";
        public decimal Amount { get; set; }
        public string Message { get; set; } = "";
        public DateTime CalledAt { get; set; }
        public bool Success { get; set; }
        public string? FailureReason { get; set; }
    }

    public bool ProcessDonation(string donorName, decimal amount, string message)
    {
        _callCount++;
        if (string.IsNullOrEmpty(donorName) || donorName.Length < 2)
        {
            Console.WriteLine($"💰 MOCK DONATION FAILED: Invalid donor name");
            _callHistory.Add(new DonationCall { 
                DonorName = donorName, Amount = amount, Message = message, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = "Invalid donor name"
            });
            return false;
        }
        if (amount < MinAmount)
        {
            Console.WriteLine($"💰 MOCK DONATION FAILED: Amount too low (min {MinAmount} RON)");
            _callHistory.Add(new DonationCall { 
                DonorName = donorName, Amount = amount, Message = message, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = $"Amount below minimum ({MinAmount} RON)"
            });
            return false;
        }
        if (amount > MaxAmount)
        {
            Console.WriteLine($"💰 MOCK DONATION FAILED: Amount too high (max {MaxAmount} RON)");
            _callHistory.Add(new DonationCall { 
                DonorName = donorName, Amount = amount, Message = message, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = $"Amount exceeds maximum ({MaxAmount} RON)"
            });
            return false;
        }
        if (SimulatePaymentFailure)
        {
            Console.WriteLine($"💰 MOCK DONATION FAILED: Payment processor error");
            _callHistory.Add(new DonationCall { 
                DonorName = donorName, Amount = amount, Message = message, 
                CalledAt = DateTime.Now, Success = false,
                FailureReason = "Payment processor error"
            });
            return false;
        }
        _totalDonations += amount;
        Console.WriteLine($"💰 MOCK DONATION #{_callCount}: Donor={donorName}, Amount={amount} RON (Total: {_totalDonations} RON)");
        _callHistory.Add(new DonationCall { 
            DonorName = donorName, Amount = amount, Message = message, 
            CalledAt = DateTime.Now, Success = true 
        });
        return true;
    }

    public int GetCallCount() => _callCount;
    public decimal GetTotalDonations() => _totalDonations;
    public List<DonationCall> GetCallHistory() => _callHistory;
    public void ResetMock()
    {
        _callCount = 0;
        _totalDonations = 0;
        _callHistory.Clear();
        SimulatePaymentFailure = false;
        MinAmount = 1;
        MaxAmount = 10000;
    }
    public DonationCall? GetLastCall() => _callHistory.LastOrDefault();
}

