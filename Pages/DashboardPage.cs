using DogShelter.Data;

namespace DogShelter.Pages;

public static class DashboardPage
{
    public static string GetHtml(List<dynamic> dogs)
    {
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>🐕 DogShelter - Cu Mock-uri Integrate</title>
    <meta charset='utf-8'>
    <style>
        body {{
            font-family: 'Poppins', sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 50%, #f093fb 100%);
            min-height: 100vh;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            background: rgba(255, 255, 255, 0.95);
            backdrop-filter: blur(20px);
            border-radius: 25px;
            padding: 50px;
            max-width: 1200px;
            margin: 0 auto;
            box-shadow: 0 25px 50px rgba(0, 0, 0, 0.15);
        }}
        h1 {{
            text-align: center;
            font-size: 3em;
            font-weight: 700;
            background: linear-gradient(135deg, #6f42c1, #9b59b6, #e74c3c);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            margin-bottom: 30px;
        }}
        .stats {{
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
            gap: 20px;
            margin: 30px 0;
        }}
        .stat-card {{
            background: linear-gradient(135deg, #6f42c1, #9b59b6);
            color: white;
            padding: 25px;
            border-radius: 15px;
            text-align: center;
        }}
        .stat-number {{
            font-size: 2.5em;
            font-weight: bold;
            margin-bottom: 10px;
        }}
        .nav-grid {{
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 30px;
            margin: 40px 0;
        }}
        .nav-card {{
            background: linear-gradient(135deg, rgba(255,255,255,0.9), rgba(248,249,250,0.9));
            padding: 30px;
            border-radius: 20px;
            text-align: center;
            transition: all 0.3s ease;
            border: 2px solid transparent;
            text-decoration: none;
            color: inherit;
            display: block;
        }}
        .nav-card:hover {{
            transform: translateY(-10px);
            box-shadow: 0 20px 40px rgba(0,0,0,0.15);
            border-color: #6f42c1;
        }}
        .nav-icon {{
            font-size: 3em;
            margin-bottom: 15px;
        }}
        .nav-title {{
            font-size: 1.5em;
            font-weight: bold;
            color: #2c3e50;
            margin-bottom: 10px;
        }}
        .nav-desc {{
            color: #666;
            line-height: 1.5;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>🐕 DogShelter</h1>
        
        <div class='stats'>
            <div class='stat-card'>
                <div class='stat-number'>{dogs.Count}</div>
                <div>Câini în Adăpost</div>
            </div>
            <div class='stat-card'>
                <div class='stat-number'>{GlobalData.Adoptions.Count}</div>
                <div>Adopții Realizate</div>
            </div>
            <div class='stat-card'>
                <div class='stat-number'>{(GlobalData.Donations.Count > 0 ? GlobalData.Donations.Sum(d => (decimal)d.amount).ToString("F0") : "0")} RON</div>
                <div>Donații Primite</div>
            </div>
            <div class='stat-card'>
                <div class='stat-number'>{GlobalData.Adopters.Count}</div>
                <div>Adoptatori Înregistrați</div>
            </div>
        </div>
        
        <div class='nav-grid'>
            <a href='/dogs' class='nav-card'>
                <div class='nav-icon'>🐕</div>
                <div class='nav-title'>Gestionare Câini</div>
                <div class='nav-desc'>Adaugă câini cu mock veterinary și logger</div>
            </a>
            
            <a href='/adopters' class='nav-card'>
                <div class='nav-icon'>👥</div>
                <div class='nav-title'>Adoptatori</div>
                <div class='nav-desc'>Înregistrează adoptatori cu mock email și logger</div>
            </a>
            
            <a href='/adoptions' class='nav-card'>
                <div class='nav-icon'>❤️</div>
                <div class='nav-title'>Adopții</div>
                <div class='nav-desc'>Procesează adopții cu mock email</div>
            </a>
            
            <a href='/donations' class='nav-card'>
                <div class='nav-icon'>💰</div>
                <div class='nav-title'>Donații</div>
                <div class='nav-desc'>Primește donații cu mock donation service</div>
            </a>
            
            <a href='/veterinary' class='nav-card'>
                <div class='nav-icon'>🏥</div>
                <div class='nav-title'>Servicii Veterinare</div>
                <div class='nav-desc'>Programează consultații cu mock veterinary service</div>
            </a>
            
            <a href='/test-mocks' class='nav-card'>
                <div class='nav-icon'>🧪</div>
                <div class='nav-title'>Testează Mock-urile</div>
                <div class='nav-desc'>Testează toate mock-urile integrate</div>
            </a>
            
            <a href='/mock-demo' class='nav-card' style='border: 3px solid #667eea; background: linear-gradient(135deg, rgba(102,126,234,0.1), rgba(118,75,162,0.1));'>
                <div class='nav-icon'>🎯</div>
                <div class='nav-title' style='color: #667eea;'>Demo Mock-uri Avansate</div>
                <div class='nav-desc' style='color: #667eea; font-weight: 600;'>✨ NOU: Validări, state tracking, istoricul apelurilor!</div>
            </a>
        </div>
        
        <div style='text-align: center; margin-top: 40px; padding: 20px; background: rgba(111, 66, 193, 0.1); border-radius: 15px;'>
            <p style='color: #666; font-size: 1.2em; margin: 0;'>
                ✨ Mock-urile sunt integrate TRANSPARENT în aplicație!
            </p>
        </div>
    </div>
</body>
</html>";
    }
}

