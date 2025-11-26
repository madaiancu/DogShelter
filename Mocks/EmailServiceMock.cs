using DogShelter.Interfaces;

namespace DogShelter.Mocks;

public class EmailServiceMock : IEmailService
{
    private List<EmailCall> _callHistory = new List<EmailCall>();
    private int _callCount = 0;
    public bool SimulateFailure { get; set; } = false;
    public int DelayMs { get; set; } = 0;

    public class EmailCall
    {
        public string To { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public DateTime CalledAt { get; set; }
        public bool Success { get; set; }
    }

    public bool SendEmail(string to, string subject, string body)
    {
        _callCount++;
        if (DelayMs > 0)
        {
            Thread.Sleep(DelayMs);
        }
        if (string.IsNullOrEmpty(to) || !to.Contains("@"))
        {
            Console.WriteLine($"📧 MOCK EMAIL FAILED: Invalid email address '{to}'");
            _callHistory.Add(new EmailCall { 
                To = to, Subject = subject, Body = body, 
                CalledAt = DateTime.Now, Success = false 
            });
            return false;
        }
        if (string.IsNullOrEmpty(subject))
        {
            Console.WriteLine($"📧 MOCK EMAIL FAILED: Empty subject");
            _callHistory.Add(new EmailCall { 
                To = to, Subject = subject, Body = body, 
                CalledAt = DateTime.Now, Success = false 
            });
            return false;
        }
        if (SimulateFailure)
        {
            Console.WriteLine($"📧 MOCK EMAIL FAILED: Simulated failure");
            _callHistory.Add(new EmailCall { 
                To = to, Subject = subject, Body = body, 
                CalledAt = DateTime.Now, Success = false 
            });
            return false;
        }
        Console.WriteLine($"📧 MOCK EMAIL #{_callCount}: To={to}, Subject={subject}");
        _callHistory.Add(new EmailCall { 
            To = to, Subject = subject, Body = body, 
            CalledAt = DateTime.Now, Success = true 
        });
        return true;
    }

    public int GetCallCount() => _callCount;
    public List<EmailCall> GetCallHistory() => _callHistory;
    public void ResetMock()
    {
        _callCount = 0;
        _callHistory.Clear();
        SimulateFailure = false;
        DelayMs = 0;
    }
    public EmailCall? GetLastCall() => _callHistory.LastOrDefault();
}

