using DogShelter.Data;

namespace DogShelter.Pages;

public static class VeterinaryPage
{
    public static string GetHtml()
    {
        var dogsOptions = string.Join("", GlobalData.Dogs.Select(dog => $@"
                        <option value='{dog.id}'>{dog.name} - {dog.breed} ({dog.age} ani)</option>"));
        
        var appointmentsList = GlobalData.VetAppointments.Count == 0 ? 
            @"<p style='text-align: center; color: #666; font-style: italic; padding: 40px;'>
                Încă nu sunt programări veterinare. Programează prima consultație!
            </p>" :
            string.Join("", GlobalData.VetAppointments.OrderByDescending(a => (DateTime)a.appointmentDate).Select(appointment => $@"
            <div class='appointment-card'>
                <h4 class='appointment-title'>🏥 {appointment.dogName} - {appointment.type}</h4>
                <div class='appointment-info'><strong>Câine:</strong> {appointment.dogName} ({appointment.dogBreed})</div>
                <div class='appointment-info'><strong>Veterinar:</strong> {appointment.veterinarian}</div>
                <div class='appointment-info'><strong>Data:</strong> {((DateTime)appointment.appointmentDate).ToString("dd.MM.yyyy HH:mm")}</div>
                <div class='appointment-info'><strong>Programat:</strong> {((DateTime)appointment.scheduledDate).ToString("dd.MM.yyyy HH:mm")}</div>
                <span class='status-{(appointment.status == "Programată" ? "scheduled" : appointment.status == "Finalizată" ? "completed" : "pending")}'>{appointment.status}</span>
                {(string.IsNullOrEmpty(appointment.observations.ToString()) ? "" : $"<div class='appointment-info'><strong>Observații:</strong> {appointment.observations}</div>")}
            </div>"));
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>🏥 Servicii Veterinare - DogShelter</title>
    <meta charset='utf-8'>
    {GetStyles()}
</head>
<body>
    <div class='container'>
        <a href='/' class='back-btn'>← Înapoi la Dashboard</a>
        <h1>🏥 Servicii Veterinare</h1>
        
        <div class='stats-grid'>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.VetAppointments.Count}</div>
                <div>Programări Totale</div>
            </div>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.VetAppointments.Count(a => a.status == "Programată")}</div>
                <div>Programări Active</div>
            </div>
            <div class='stat-box'>
                <div class='stat-number'>{GlobalData.VetAppointments.Count(a => a.status == "Finalizată")}</div>
                <div>Tratamente Finalizate</div>
            </div>
        </div>
        
        <div class='mock-info'>
            <h4>🧪 Mock-uri Active pentru Veterinar:</h4>
            <p>• <strong>Veterinary Service:</strong> Programează și gestionează consultațiile</p>
            <p>• <strong>Email Service:</strong> Trimite confirmări de programare</p>
            <p>• <strong>Logger:</strong> Înregistrează toate activitățile veterinare</p>
        </div>
        
        <div class='form-section'>
            <h3>Programează Consultație Veterinară</h3>
            <form id='vetForm'>
                <div class='form-group'>
                    <label>Selectează Câinele:</label>
                    <select id='selectedDog' required>
                        <option value=''>Alege câinele pentru consultație...</option>
                        {dogsOptions}
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Tipul Consultației:</label>
                    <select id='appointmentType' required>
                        <option value=''>Selectează tipul...</option>
                        <option value='Control medical general'>Control medical general</option>
                        <option value='Vaccinare'>Vaccinare</option>
                        <option value='Deparazitare'>Deparazitare</option>
                        <option value='Tratament medical'>Tratament medical</option>
                        <option value='Chirurgie'>Chirurgie</option>
                        <option value='Control post-adopție'>Control post-adopție</option>
                        <option value='Urgență'>Urgență</option>
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Data și Ora Programării:</label>
                    <input type='datetime-local' id='appointmentDate' required min='{DateTime.Now.ToString("yyyy-MM-ddTHH:mm")}'>
                </div>
                
                <div class='form-group'>
                    <label>Veterinarul:</label>
                    <select id='veterinarian' required>
                        <option value=''>Selectează veterinarul...</option>
                        <option value='Dr. Popescu Mihai'>Dr. Popescu Mihai</option>
                        <option value='Dr. Ionescu Ana'>Dr. Ionescu Ana</option>
                        <option value='Dr. Georgescu Radu'>Dr. Georgescu Radu</option>
                    </select>
                </div>
                
                <div class='form-group'>
                    <label>Observații (opțional):</label>
                    <textarea id='observations' rows='3' placeholder='Observații despre starea câinelui sau tratamentul necesar...'></textarea>
                </div>
                
                <button type='submit' class='btn'>🏥 Programează Consultația (cu Mock-uri)</button>
            </form>
        </div>
        
        <div class='form-section'>
            <h3>📋 Programări Veterinare ({GlobalData.VetAppointments.Count} programări)</h3>
            <div id='appointmentsList'>
                {appointmentsList}
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
        .btn { background: linear-gradient(135deg, #27ae60, #2ecc71); color: white; padding: 15px 30px; border: none; border-radius: 10px; font-size: 1.1em; font-weight: bold; cursor: pointer; width: 100%; margin-top: 20px; }
        .appointment-card { background: white; padding: 20px; margin: 15px 0; border-radius: 15px; box-shadow: 0 5px 15px rgba(0,0,0,0.1); border-left: 5px solid #27ae60; }
        .appointment-title { color: #27ae60; font-size: 1.3em; font-weight: bold; margin: 0 0 10px 0; }
        .appointment-info { color: #666; margin: 5px 0; }
        .mock-info { background: rgba(39, 174, 96, 0.1); padding: 15px; border-radius: 10px; margin: 20px 0; border-left: 4px solid #27ae60; }
        .stats-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px; margin: 20px 0; }
        .stat-box { background: linear-gradient(135deg, #27ae60, #2ecc71); color: white; padding: 20px; border-radius: 15px; text-align: center; }
        .stat-number { font-size: 2em; font-weight: bold; }
        .status-pending { background: #fff3cd; color: #856404; padding: 5px 10px; border-radius: 15px; font-size: 0.9em; }
        .status-completed { background: #d4edda; color: #155724; padding: 5px 10px; border-radius: 15px; font-size: 0.9em; }
        .status-scheduled { background: #cce5ff; color: #004085; padding: 5px 10px; border-radius: 15px; font-size: 0.9em; }
    </style>";
    }
    
    private static string GetScript()
    {
        return @"
    <script>
        document.getElementById('vetForm').addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const appointmentData = {
                dogId: parseInt(document.getElementById('selectedDog').value),
                type: document.getElementById('appointmentType').value,
                appointmentDate: document.getElementById('appointmentDate').value,
                veterinarian: document.getElementById('veterinarian').value,
                observations: document.getElementById('observations').value
            };
            
            if (!appointmentData.dogId) {
                alert('❌ Te rog selectează câinele pentru consultație!');
                return;
            }
            
            try {
                const response = await fetch('/api/veterinary', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(appointmentData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    alert('✅ Programarea veterinară a fost creată cu succes!\n\n' + 
                          'Câine: ' + result.appointment.dogName + '\n' +
                          'Data: ' + new Date(appointmentData.appointmentDate).toLocaleString() + '\n' +
                          'Mock-uri activate:\n' +
                          '📝 Logger: Programare înregistrată\n' +
                          '🏥 Veterinary Service: Consultație programată\n' +
                          '📧 Email: Confirmare trimisă');
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


