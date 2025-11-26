using DogShelter.Data;

namespace DogShelter.Pages;

public static class DonationsPage
{
    public static string GetHtml()
    {
        var donationsList = GlobalData.Donations.Count == 0 ? 
            @"<p style='text-align: center; color: #666; font-style: italic; padding: 40px;'>
                Încă nu au fost primite donații. Fii primul care susține adăpostul!
            </p>" :
            string.Join("", GlobalData.Donations.OrderByDescending(d => (DateTime)d.donationDate).Select(donation => $@"
            <div class='donation-card'>
                <h4 class='donation-amount'>💰 {donation.amount} RON</h4>
                <div class='donation-info'><strong>Donator:</strong> {donation.donorName}</div>
                <div class='donation-info'><strong>Email:</strong> {donation.donorEmail}</div>
                <div class='donation-info'><strong>Scop:</strong> {donation.purpose}</div>
                <div class='donation-info'><strong>Data:</strong> {((DateTime)donation.donationDate).ToString("dd.MM.yyyy HH:mm")}</div>
                {(string.IsNullOrEmpty(donation.message.ToString()) ? "" : $"<div class='donation-info'><strong>Mesaj:</strong> {donation.message}</div>")}
            </div>"));
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>💰 Donații - DogShelter</title>
    <meta charset='utf-8'>
    {GetStyles()}
</head>
<body>
    <div class='container'>
        <a href='/' class='back-btn'>← Înapoi la Dashboard</a>
        <h1>💰 Donații pentru Adăpost</h1>
        
        <div class='stats-grid'>
            <div class='stat-box'>
                <div class='stat-number'>{(GlobalData.Donations.Count > 0 ? GlobalData.Donations.Sum(d => (decimal)d.amount).ToString("F0") : "0")} RON</div>
                <div>Total Donații</div>
            </div>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.Donations.Count}</div>
                <div>Numărul Donatorilor</div>
            </div>
            <div class='stat-box'>
                <div class='stat-number'>{(GlobalData.Donations.Count > 0 ? ((decimal)GlobalData.Donations.Average(d => (decimal)d.amount)).ToString("F0") : "0")} RON</div>
                <div>Donația Medie</div>
            </div>
        </div>
        
        <div class='mock-info'>
            <h4>🧪 Mock-uri Active pentru Donații:</h4>
            <p>• <strong>Donation Service:</strong> Procesează și validează donațiile</p>
            <p>• <strong>Email Service:</strong> Trimite confirmări și mulțumiri</p>
            <p>• <strong>Logger:</strong> Înregistrează toate donațiile</p>
        </div>
        
        <div class='form-section'>
            <h3>Fă o Donație</h3>
            <form id='donationForm'>
                <div class='form-group'>
                    <label>Numele Donatorului:</label>
                    <input type='text' id='donorName' required>
                </div>
                
                <div class='form-group'>
                    <label>Email (pentru confirmarea donației):</label>
                    <input type='email' id='donorEmail' required>
                </div>
                
                <div class='form-group'>
                    <label>Suma Donației (RON):</label>
                    <input type='number' id='donationAmount' min='1' step='0.01' required>
                    <div class='quick-amounts'>
                        <button type='button' class='quick-btn' onclick='setAmount(50)'>50 RON</button>
                        <button type='button' class='quick-btn' onclick='setAmount(100)'>100 RON</button>
                        <button type='button' class='quick-btn' onclick='setAmount(200)'>200 RON</button>
                        <button type='button' class='quick-btn' onclick='setAmount(500)'>500 RON</button>
                    </div>
                </div>
                
                <div class='form-group'>
                    <label>Scopul Donației:</label>
                    <select id='donationPurpose' required>
                        <option value=''>Selectează scopul...</option>
                        <option value='Hrană pentru câini'>Hrană pentru câini</option>
                        <option value='Tratamente medicale'>Tratamente medicale</option>
                        <option value='Îngrijire veterinară'>Îngrijire veterinară</option>
                        <option value='Întreținere adăpost'>Întreținere adăpost</option>
                        <option value='Echipamente'>Echipamente și jucării</option>
                        <option value='Donație generală'>Donație generală</option>
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Mesaj (opțional):</label>
                    <textarea id='donationMessage' rows='3' placeholder='Mesaj pentru echipa DogShelter...'></textarea>
                </div>
                
                <button type='submit' class='btn'>💰 Donează (cu Mock-uri)</button>
            </form>
        </div>
        
        <div class='form-section'>
            <h3>📋 Istoric Donații ({GlobalData.Donations.Count} donații)</h3>
            <div id='donationsList'>
                {donationsList}
            </div>
        </div>
    </div>
    
    {GetScript()}
</body>
</html>";
    }
    
    private static string GetStyles()
    {
        return @"
    <style>
        body { font-family: 'Poppins', sans-serif; background: linear-gradient(135deg, #667eea 0%, #764ba2 50%, #f093fb 100%); min-height: 100vh; margin: 0; padding: 20px; }
        .container { background: rgba(255, 255, 255, 0.95); backdrop-filter: blur(20px); border-radius: 25px; padding: 50px; max-width: 1200px; margin: 0 auto; box-shadow: 0 25px 50px rgba(0, 0, 0, 0.15); }
        h1 { text-align: center; font-size: 3em; color: #2c3e50; margin-bottom: 30px; }
        .back-btn { display: inline-block; background: linear-gradient(135deg, #6c757d, #495057); color: white; padding: 12px 24px; border-radius: 10px; text-decoration: none; margin-bottom: 20px; font-weight: bold; }
        .form-section { background: rgba(255, 255, 255, 0.8); padding: 30px; border-radius: 20px; margin: 20px 0; box-shadow: 0 10px 30px rgba(0,0,0,0.1); }
        .form-group { margin: 15px 0; }
        label { display: block; margin-bottom: 5px; font-weight: bold; color: #2c3e50; }
        input, select, textarea { width: 100%; padding: 12px; border: 2px solid #e9ecef; border-radius: 10px; font-size: 1em; box-sizing: border-box; }
        .btn { background: linear-gradient(135deg, #f39c12, #e67e22); color: white; padding: 15px 30px; border: none; border-radius: 10px; font-size: 1.1em; font-weight: bold; cursor: pointer; width: 100%; margin-top: 20px; }
        .donation-card { background: white; padding: 20px; margin: 15px 0; border-radius: 15px; box-shadow: 0 5px 15px rgba(0,0,0,0.1); border-left: 5px solid #f39c12; }
        .donation-amount { color: #f39c12; font-size: 1.5em; font-weight: bold; margin: 0 0 10px 0; }
        .donation-info { color: #666; margin: 5px 0; }
        .mock-info { background: rgba(243, 156, 18, 0.1); padding: 15px; border-radius: 10px; margin: 20px 0; border-left: 4px solid #f39c12; }
        .stats-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px; margin: 20px 0; }
        .stat-box { background: linear-gradient(135deg, #f39c12, #e67e22); color: white; padding: 20px; border-radius: 15px; text-align: center; }
        .stat-number { font-size: 2em; font-weight: bold; }
        .quick-amounts { display: grid; grid-template-columns: repeat(auto-fit, minmax(100px, 1fr)); gap: 10px; margin: 15px 0; }
        .quick-btn { background: linear-gradient(135deg, #3498db, #2980b9); color: white; padding: 10px; border: none; border-radius: 8px; cursor: pointer; font-weight: bold; }
    </style>";
    }
    
    private static string GetScript()
    {
        return @"
    <script>
        function setAmount(amount) {
            document.getElementById('donationAmount').value = amount;
        }
        
        document.getElementById('donationForm').addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const donationData = {
                donorName: document.getElementById('donorName').value,
                donorEmail: document.getElementById('donorEmail').value,
                amount: parseFloat(document.getElementById('donationAmount').value),
                purpose: document.getElementById('donationPurpose').value,
                message: document.getElementById('donationMessage').value
            };
            
            try {
                const response = await fetch('/api/donations', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(donationData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    alert('✅ Donația a fost procesată cu succes!\n\n' + 
                          'Sumă: ' + donationData.amount + ' RON\n' +
                          'Mock-uri activate:\n' +
                          '📝 Logger: Donație înregistrată\n' +
                          '💰 Donation Service: Procesare validată\n' +
                          '📧 Email: Mulțumire trimisă');
                    window.location.reload();
                } else {
                    alert('❌ Eroare: ' + result.error);
                }
            } catch (error) {
                alert('❌ Eroare de conexiune!');
            }
        });
    </script>";
    }
}


