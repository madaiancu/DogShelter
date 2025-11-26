using DogShelter.Data;

namespace DogShelter.Pages;

public static class AdoptersPage
{
    public static string GetHtml()
    {
        var adoptersList = string.Join("", GlobalData.Adopters.Select(adopter => $@"
            <div class='adopter-card'>
                <h4 class='adopter-name'>👥 {adopter.name}</h4>
                <div class='adopter-info'><strong>Email:</strong> {adopter.email}</div>
                <div class='adopter-info'><strong>Telefon:</strong> {adopter.phone}</div>
                <div class='adopter-info'><strong>Vârstă:</strong> {adopter.age} ani</div>
                <div class='adopter-info'><strong>Experiență:</strong> {adopter.experience}</div>
                <div class='adopter-info'><strong>Locuință:</strong> {adopter.housing}</div>
                <div class='adopter-info'><strong>Înregistrat:</strong> {((DateTime)adopter.dateRegistered).ToString("dd.MM.yyyy")}</div>
            </div>"));
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>👥 Gestionare Adoptatori - DogShelter</title>
    <meta charset='utf-8'>
    {GetStyles()}
</head>
<body>
    <div class='container'>
        <a href='/' class='back-btn'>← Înapoi la Dashboard</a>
        <h1>👥 Gestionare Adoptatori</h1>
        
        <div class='mock-info'>
            <h4>🧪 Mock-uri Active:</h4>
            <p>• <strong>Email Service:</strong> Trimite email de bun venit la înregistrare</p>
            <p>• <strong>Logger:</strong> Înregistrează toate acțiunile adoptatorilor</p>
        </div>
        
        <div class='form-section'>
            <h3>Înregistrează Adoptator Nou</h3>
            <form id='adopterForm'>
                <div class='form-group'>
                    <label>Nume Complet:</label>
                    <input type='text' id='adopterName' required>
                </div>
                
                <div class='form-group'>
                    <label>Email:</label>
                    <input type='email' id='adopterEmail' required>
                </div>
                
                <div class='form-group'>
                    <label>Telefon:</label>
                    <input type='tel' id='adopterPhone' required>
                </div>
                
                <div class='form-group'>
                    <label>Vârstă:</label>
                    <input type='number' id='adopterAge' min='18' max='100' required>
                </div>
                
                <div class='form-group'>
                    <label>Experiență cu câinii:</label>
                    <select id='adopterExperience' required>
                        <option value=''>Selectează...</option>
                        <option value='Prima dată'>Prima dată</option>
                        <option value='Am avut câini înainte'>Am avut câini înainte</option>
                        <option value='Experiență mare'>Experiență mare (5+ câini)</option>
                        <option value='Profesionist'>Profesionist (veterinar/dresaj)</option>
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Tip locuință:</label>
                    <select id='adopterHousing' required>
                        <option value=''>Selectează...</option>
                        <option value='Apartament'>Apartament</option>
                        <option value='Casă cu curte mică'>Casă cu curte mică</option>
                        <option value='Casă cu curte mare'>Casă cu curte mare</option>
                        <option value='Fermă/Teren mare'>Fermă/Teren mare</option>
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Motivația pentru adopție:</label>
                    <textarea id='adopterMotivation' rows='3' placeholder='De ce doriți să adoptați un câine?'></textarea>
                </div>
                
                <button type='submit' class='btn'>👥 Înregistrează Adoptatorul (cu Mock-uri)</button>
            </form>
        </div>
        
        <div class='form-section'>
            <h3>📋 Lista Adoptatorilor ({GlobalData.Adopters.Count} adoptatori)</h3>
            {adoptersList}
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
        input, select, textarea {
            width: 100%;
            padding: 12px;
            border: 2px solid #e9ecef;
            border-radius: 10px;
            font-size: 1em;
            box-sizing: border-box;
        }
        .btn {
            background: linear-gradient(135deg, #28a745, #20c997);
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
        .adopter-card {
            background: white;
            padding: 20px;
            margin: 15px 0;
            border-radius: 15px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
        }
        .adopter-name {
            color: #28a745;
            font-size: 1.5em;
            font-weight: bold;
            margin: 0 0 10px 0;
        }
        .adopter-info {
            color: #666;
            margin: 5px 0;
        }
        .mock-info {
            background: rgba(40, 167, 69, 0.1);
            padding: 15px;
            border-radius: 10px;
            margin: 20px 0;
            border-left: 4px solid #28a745;
        }
    </style>";
    }
    
    private static string GetScript()
    {
        return @"
    <script>
        document.getElementById('adopterForm').addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const adopterData = {
                name: document.getElementById('adopterName').value,
                email: document.getElementById('adopterEmail').value,
                phone: document.getElementById('adopterPhone').value,
                age: parseInt(document.getElementById('adopterAge').value),
                experience: document.getElementById('adopterExperience').value,
                housing: document.getElementById('adopterHousing').value,
                motivation: document.getElementById('adopterMotivation').value
            };
            
            try {
                const response = await fetch('/api/adopters', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(adopterData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    alert('✅ Adoptatorul a fost înregistrat cu succes!\n\n' + 
                          'Mock-uri activate:\n' +
                          '📝 Logger: Acțiune înregistrată\n' +
                          '📧 Email: Mesaj de bun venit trimis');
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


