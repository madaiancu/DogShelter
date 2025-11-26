using DogShelter.Data;

namespace DogShelter.Pages;

public static class AdoptionsPage
{
    public static string GetHtml()
    {
        var dogsOptions = string.Join("", GlobalData.Dogs.Select(dog => $@"
                        <option value='{dog.id}'>{dog.name} - {dog.breed} ({dog.age} ani)</option>"));
        
        var adoptersOptions = string.Join("", GlobalData.Adopters.Select(adopter => $@"
                        <option value='{adopter.id}'>{adopter.name} - {adopter.email}</option>"));
        
        var adoptionsList = GlobalData.Adoptions.Count == 0 ? 
            @"<p style='text-align: center; color: #666; font-style: italic; padding: 40px;'>
                Încă nu au fost procesate adopții. Folosește formularul de mai sus pentru a începe primul proces de adopție!
            </p>" :
            string.Join("", GlobalData.Adoptions.Select(adoption => $@"
            <div class='adoption-card'>
                <h4 class='adoption-title'>❤️ {adoption.dogName} → {adoption.adopterName}</h4>
                <div class='adoption-info'><strong>Câine:</strong> {adoption.dogName} ({adoption.dogBreed})</div>
                <div class='adoption-info'><strong>Adoptator:</strong> {adoption.adopterName}</div>
                <div class='adoption-info'><strong>Email:</strong> {adoption.adopterEmail}</div>
                <div class='adoption-info'><strong>Data adopției:</strong> {((DateTime)adoption.adoptionDate).ToString("dd.MM.yyyy HH:mm")}</div>
                <div class='adoption-info'><strong>Status:</strong> {adoption.status}</div>
            </div>"));
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>❤️ Procesul de Adopție - DogShelter</title>
    <meta charset='utf-8'>
    {GetStyles()}
</head>
<body>
    <div class='container'>
        <a href='/' class='back-btn'>← Înapoi la Dashboard</a>
        <h1>❤️ Procesul de Adopție</h1>
        
        <div class='stats-grid'>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.Dogs.Count}</div>
                <div>Câini Disponibili</div>
            </div>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.Adopters.Count}</div>
                <div>Adoptatori Înregistrați</div>
            </div>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.Adoptions.Count}</div>
                <div>Adopții Realizate</div>
            </div>
        </div>
        
        <div class='mock-info'>
            <h4>🧪 Mock-uri Active pentru Adopții:</h4>
            <p>• <strong>Email Service:</strong> Trimite confirmări către adoptator și admin</p>
            <p>• <strong>Logger:</strong> Înregistrează toate adopțiile</p>
            <p>• <strong>Veterinary:</strong> Programează control medical post-adopție</p>
        </div>
        
        <div class='form-section'>
            <h3>Procesează Adopție Nouă</h3>
            <form id='adoptionForm'>
                <div class='form-group'>
                    <label>Selectează Câinele:</label>
                    <select id='selectedDog' required>
                        <option value=''>Alege câinele pentru adopție...</option>
                        {dogsOptions}
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Selectează Adoptatorul:</label>
                    <select id='selectedAdopter' required>
                        <option value=''>Alege adoptatorul...</option>
                        {adoptersOptions}
                    </select>
                </div>
                
                <button type='submit' class='btn'>❤️ Procesează Adopția (cu Mock-uri)</button>
            </form>
        </div>
        
        <div class='form-section'>
            <h3>📋 Istoric Adopții ({GlobalData.Adoptions.Count} adopții)</h3>
            <div id='adoptionsList'>
                {adoptionsList}
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
        body {
            font-family: 'Poppins', sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 50%, #f093fb 100%);
            min-height: 100vh;
            margin: 0;
            padding: 20px;
        }
        .container {
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(20px);
            border-radius: 25px;
            padding: 50px;
            max-width: 1200px;
            margin: 0 auto;
            box-shadow: 0 25px 50px rgba(0, 0, 0, 0.15);
        }
        h1 {
            text-align: center;
            font-size: 3em;
            color: #2c3e50;
            margin-bottom: 30px;
        }
        .back-btn {
            display: inline-block;
            background: linear-gradient(135deg, #6c757d, #495057);
            color: white;
            padding: 12px 24px;
            border-radius: 10px;
            text-decoration: none;
            margin-bottom: 20px;
            font-weight: bold;
        }
        .form-section {
            background: rgba(255, 255, 255, 0.8);
            padding: 30px;
            border-radius: 20px;
            margin: 20px 0;
            box-shadow: 0 10px 30px rgba(0,0,0,0.1);
        }
        .form-group {
            margin: 15px 0;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #2c3e50;
        }
        select {
            width: 100%;
            padding: 12px;
            border: 2px solid #e9ecef;
            border-radius: 10px;
            font-size: 1em;
            box-sizing: border-box;
        }
        .btn {
            background: linear-gradient(135deg, #e74c3c, #c0392b);
            color: white;
            padding: 15px 30px;
            border: none;
            border-radius: 10px;
            font-size: 1.1em;
            font-weight: bold;
            cursor: pointer;
            width: 100%;
            margin-top: 20px;
        }
        .adoption-card {
            background: white;
            padding: 20px;
            margin: 15px 0;
            border-radius: 15px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
            border-left: 5px solid #e74c3c;
        }
        .adoption-title {
            color: #e74c3c;
            font-size: 1.3em;
            font-weight: bold;
            margin: 0 0 10px 0;
        }
        .adoption-info {
            color: #666;
            margin: 5px 0;
        }
        .mock-info {
            background: rgba(231, 76, 60, 0.1);
            padding: 15px;
            border-radius: 10px;
            margin: 20px 0;
            border-left: 4px solid #e74c3c;
        }
        .stats-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin: 20px 0;
        }
        .stat-box {
            background: linear-gradient(135deg, #e74c3c, #c0392b);
            color: white;
            padding: 20px;
            border-radius: 15px;
            text-align: center;
        }
        .stat-number {
            font-size: 2em;
            font-weight: bold;
        }
    </style>";
    }
    
    private static string GetScript()
    {
        return @"
    <script>
        document.getElementById('adoptionForm').addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const adoptionData = {
                dogId: parseInt(document.getElementById('selectedDog').value),
                adopterId: parseInt(document.getElementById('selectedAdopter').value)
            };
            
            if (!adoptionData.dogId || !adoptionData.adopterId) {
                alert('❌ Te rog selectează atât câinele cât și adoptatorul!');
                return;
            }
            
            try {
                const response = await fetch('/api/adoptions', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(adoptionData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    alert('✅ Adopția a fost procesată cu succes!\n\n' + 
                          'Mock-uri activate:\n' +
                          '📝 Logger: Adopție înregistrată\n' +
                          '📧 Email: Confirmări trimise\n' +
                          '🏥 Veterinary: Control programat');
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


