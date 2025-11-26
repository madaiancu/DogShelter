using DogShelter.Data;

namespace DogShelter.Pages;

public static class DogsPage
{
    public static string GetHtml(List<dynamic> dogs)
    {
        var dogsList = string.Join("", dogs.Select(dog => $@"
            <div class='dog-card'>
                <h4 class='dog-name'>🐕 {dog.name}</h4>
                <div class='dog-info'><strong>Rasă:</strong> {dog.breed}</div>
                <div class='dog-info'><strong>Vârstă:</strong> {dog.age} ani</div>
                <div class='dog-info'><strong>Greutate:</strong> {dog.weight} kg</div>
                <div class='dog-info'><strong>Sănătate:</strong> {dog.health}</div>
                <div class='dog-info'><strong>Adăugat:</strong> {((DateTime)dog.dateAdded).ToString("dd.MM.yyyy")}</div>
            </div>"));
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>🐕 Gestionare Câini cu Mock-uri</title>
    <meta charset='utf-8'>
    {GetStyles()}
</head>
<body>
    <div class='container'>
        <a href='/' class='back-btn'>← Înapoi la Dashboard</a>
        <h1>🐕 Gestionare Câini</h1>
        
        <div class='mock-info'>
            <h4>🧪 Mock-uri Active:</h4>
            <p>• <strong>VeterinaryService:</strong> Programează automat control medical la adăugare</p>
            <p>• <strong>Logger:</strong> Înregistrează toate acțiunile</p>
            <p>• <strong>Email:</strong> Trimite notificări (simulat)</p>
        </div>
        
        <div class='form-section'>
            <h3>Adaugă Câine Nou</h3>
            <form id='dogForm'>
                <div class='form-group'>
                    <label>Nume Câine:</label>
                    <input type='text' id='dogName' required>
                </div>
                
                <div class='form-group'>
                    <label>Rasă:</label>
                    <select id='dogBreed' required>
                        <option value=''>Selectează rasa...</option>
                        <option value='Labrador'>Labrador</option>
                        <option value='Golden Retriever'>Golden Retriever</option>
                        <option value='German Shepherd'>German Shepherd</option>
                        <option value='Husky'>Husky</option>
                        <option value='Bulldog'>Bulldog</option>
                        <option value='Beagle'>Beagle</option>
                        <option value='Maidanez'>Maidanez (Metis)</option>
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Vârstă (ani):</label>
                    <input type='number' id='dogAge' min='0' max='20' required>
                </div>
                
                <div class='form-group'>
                    <label>Greutate (kg):</label>
                    <input type='number' id='dogWeight' min='1' max='100' step='0.1' required>
                </div>
                
                <div class='form-group'>
                    <label>Stare de Sănătate:</label>
                    <select id='dogHealth' required>
                        <option value=''>Selectează...</option>
                        <option value='Excelentă'>Excelentă</option>
                        <option value='Bună'>Bună</option>
                        <option value='Necesită îngrijire'>Necesită îngrijire</option>
                    </select>
                </div>
                
                <button type='submit' class='btn'>🐕 Adaugă Câinele (cu Mock-uri)</button>
            </form>
        </div>
        
        <div class='form-section'>
            <h3>📋 Lista Câinilor ({dogs.Count} câini)</h3>
            {dogsList}
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
        input, select {
            width: 100%;
            padding: 12px;
            border: 2px solid #e9ecef;
            border-radius: 10px;
            font-size: 1em;
            box-sizing: border-box;
        }
        .btn {
            background: linear-gradient(135deg, #6f42c1, #9b59b6);
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
        .dog-card {
            background: white;
            padding: 20px;
            margin: 15px 0;
            border-radius: 15px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.1);
        }
        .dog-name {
            color: #6f42c1;
            font-size: 1.5em;
            font-weight: bold;
            margin: 0 0 10px 0;
        }
        .dog-info {
            color: #666;
            margin: 5px 0;
        }
        .mock-info {
            background: rgba(111, 66, 193, 0.1);
            padding: 15px;
            border-radius: 10px;
            margin: 20px 0;
            border-left: 4px solid #6f42c1;
        }
    </style>";
    }
    
    private static string GetScript()
    {
        return @"
    <script>
        document.getElementById('dogForm').addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const dogData = {
                name: document.getElementById('dogName').value,
                breed: document.getElementById('dogBreed').value,
                age: parseInt(document.getElementById('dogAge').value),
                weight: parseFloat(document.getElementById('dogWeight').value),
                health: document.getElementById('dogHealth').value
            };
            
            try {
                const response = await fetch('/api/dogs', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(dogData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    alert('✅ Câinele a fost adăugat cu succes!\n\n' + 
                          'Mock-uri activate:\n' +
                          '📝 Logger: Acțiune înregistrată\n' +
                          '🏥 Veterinary: Programare creată\n' +
                          '📧 Email: Notificare trimisă');
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


