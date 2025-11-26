using DogShelter.Data;

namespace DogShelter.Pages;

public static class TestMocksPage
{
    public static string GetHtml()
    {
        var dogsOptions = string.Join("", GlobalData.Dogs.Select(dog => $@"
                    <option value='{dog.id}'>{dog.name} - {dog.breed}</option>"));
        
        return $@"
<!DOCTYPE html>
<html>
<head>
    <title>🧪 Testează Mock-urile - DogShelter</title>
    <meta charset='utf-8'>
    {GetStyles()}
</head>
<body>
    <div class='container'>
        <a href='/' class='back-btn'>← Înapoi la Dashboard</a>
        <h1>🧪 Testează Mock-urile</h1>
        
        <div class='stats-overview'>
            <div class='stat-box'><div class='stat-number'>{GlobalData.Dogs.Count}</div><div>Câini în Sistem</div></div>
            <div class='stat-box'><div class='stat-number'>{GlobalData.Adopters.Count}</div><div>Adoptatori Înregistrați</div></div>
            <div class='stat-box'><div class='stat-number'>{GlobalData.Adoptions.Count}</div><div>Adopții Procesate</div></div>
            <div class='stat-box'><div class='stat-number'>{GlobalData.Donations.Count}</div><div>Donații Primite</div></div>
            <div class='stat-box'><div class='stat-number'>{GlobalData.VetAppointments.Count}</div><div>Programări Veterinare</div></div>
        </div>
        
        <div style='background: linear-gradient(135deg, #667eea, #764ba2); color: white; padding: 25px; border-radius: 15px; margin: 30px 0; text-align: center; box-shadow: 0 10px 30px rgba(0,0,0,0.2);'>
            <h2 style='margin: 0 0 15px 0; font-size: 2em;'>🎯 MOCK-URILE AU FOST ÎMBUNĂTĂȚITE!</h2>
            <p style='font-size: 1.2em; margin: 15px 0;'>
                Acum mock-urile includ: Validări Realiste, State Tracking, Istoricul Apelurilor, Comportament Configurabil și multe altele!
            </p>
            <a href='/mock-demo' style='display: inline-block; background: white; color: #667eea; padding: 15px 30px; border-radius: 10px; text-decoration: none; font-weight: bold; font-size: 1.1em; margin-top: 15px; transition: all 0.3s ease;'>
                ✨ Vezi Demo Mock-uri Avansate →
            </a>
        </div>
        
        {GetTestSections(dogsOptions)}
        
        <div class='test-results' id='testResults'>
            <p style='text-align: center; color: #666; font-style: italic;'>
                🧪 Selectează un test de mai sus pentru a vedea rezultatele aici...
            </p>
        </div>
    </div>
    
    {GetScript()}
</body>
</html>";
    }
    
    private static string GetTestSections(string dogsOptions)
    {
        return $@"
        <div class='test-section'>
            <h3>📧 Test EmailService Mock</h3>
            <p>Testează trimiterea de email-uri prin mock service.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Email fără @ (ex: test.com)<br>
                • Subiect sub 3 caractere (ex: Hi)<br>
                • Conține fail în email sau subiect
            </div>
            <div class='form-group'>
                <label>Email destinatar:</label>
                <input type='email' id='testEmail' value='test@example.com'>
            </div>
            <div class='form-group'>
                <label>Subiect:</label>
                <input type='text' id='testSubject' value='Test Email Mock'>
            </div>
            <div class='form-group'>
                <label>Mesaj:</label>
                <input type='text' id='testMessage' value='Acesta este un test pentru EmailService Mock'>
            </div>
            <button class='test-button' onclick='testEmailService()'>🧪 Testează Email Service</button>
        </div>
        
        <div class='test-section'>
            <h3>🏥 Test VeterinaryService Mock</h3>
            <p>Testează programarea consultațiilor veterinare.</p>
            <div class='form-group'>
                <label>Câine pentru test:</label>
                <select id='testDogId'>{dogsOptions}</select>
            </div>
            <div class='form-group'>
                <label>Tip consultație:</label>
                <select id='testConsultationType'>
                    <option value='Control medical'>Control medical</option>
                    <option value='Vaccinare'>Vaccinare</option>
                    <option value='Tratament'>Tratament</option>
                </select>
            </div>
            <button class='test-button' onclick='testVeterinaryService()'>🧪 Testează Veterinary Service</button>
        </div>
        
        <div class='test-section'>
            <h3>💰 Test DonationService Mock</h3>
            <p>Testează procesarea donațiilor.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Nume sub 2 caractere (ex: A)<br>
                • Sumă 0 sau negativă<br>
                • Sumă peste 10.000 RON<br>
                • Conține fail în nume sau scop
            </div>
            <div class='form-group'>
                <label>Nume donator:</label>
                <input type='text' id='testDonorName' value='Test Donator'>
            </div>
            <div class='form-group'>
                <label>Sumă (RON):</label>
                <input type='number' id='testAmount' value='100' min='1'>
            </div>
            <div class='form-group'>
                <label>Scop:</label>
                <input type='text' id='testPurpose' value='Test donație'>
            </div>
            <button class='test-button' onclick='testDonationService()'>🧪 Testează Donation Service</button>
        </div>
        
        <div class='test-section'>
            <h3>📝 Test Logger Mock</h3>
            <p>Testează înregistrarea mesajelor de log.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Mesaj sub 5 caractere (ex: Hi)<br>
                • Mesaj peste 100 caractere<br>
                • Conține fail sau error test
            </div>
            <div class='form-group'>
                <label>Mesaj de test:</label>
                <input type='text' id='testLogMessage' value='Acesta este un mesaj de test pentru Logger'>
            </div>
            <div class='form-group'>
                <label>Tip log:</label>
                <select id='testLogType'>
                    <option value='info'>Info</option>
                    <option value='error'>Error</option>
                </select>
            </div>
            <button class='test-button' onclick='testLoggerService()'>🧪 Testează Logger Service</button>
        </div>
        
        <div class='test-section'>
            <h3>🔄 Test Integrat - Fluxul Complet</h3>
            <p>Testează toate mock-urile într-un singur flux (adăugare câine + programare veterinară).</p>
            <button class='test-button' onclick='testIntegratedFlow()'>🧪 Testează Fluxul Complet</button>
        </div>
        
        <div class='test-section'>
            <h3>🎭 Test Moq - Comportament Mock</h3>
            <p>Testează comportamentul mock-urilor cu Moq patterns.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Scenario: fail → Mock va returna false<br>
                • Scenario: error → Mock va arunca excepție<br>
                • Scenario: timeout → Mock va simula timeout
            </div>
            <div class='form-group'>
                <label>Tip Mock:</label>
                <select id='moqMockType'>
                    <option value='email'>Email Mock</option>
                    <option value='veterinary'>Veterinary Mock</option>
                    <option value='donation'>Donation Mock</option>
                    <option value='logger'>Logger Mock</option>
                </select>
            </div>
            <div class='form-group'>
                <label>Scenario Test:</label>
                <select id='moqScenario'>
                    <option value='success'>Success (va trece)</option>
                    <option value='fail'>Fail (va eșua)</option>
                    <option value='error'>Error (va arunca excepție)</option>
                    <option value='timeout'>Timeout (va expira)</option>
                </select>
            </div>
            <div class='form-group'>
                <label>Parametru Test:</label>
                <input type='text' id='moqParameter' value='test@example.com' placeholder='Email, nume, mesaj, etc.'>
            </div>
            <button class='test-button' onclick='testMoqBehavior()'>🎭 Testează Moq Behavior</button>
        </div>
        
        <div class='test-section'>
            <h3>🔢 Test Moq - Verificare Apeluri (Setup/Verify)</h3>
            <p>Testează numărul de apeluri către mock-uri (Moq Verify pattern).</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Număr apeluri așteptat: 0 → Mock nu trebuie apelat<br>
                • Număr apeluri: 5+ → Verifică exact 5 apeluri<br>
                • Mode: AtLeastOnce/Never → Verifică frecvența
            </div>
            <div class='form-group'>
                <label>Tip Serviciu Mock:</label>
                <select id='verifyServiceType'>
                    <option value='email'>Email Service</option>
                    <option value='veterinary'>Veterinary Service</option>
                    <option value='donation'>Donation Service</option>
                    <option value='logger'>Logger Service</option>
                </select>
            </div>
            <div class='form-group'>
                <label>Scenariu de Test:</label>
                <select id='testScenario'>
                    <option value='add_dog'>Adaugă Câine (1 apel)</option>
                    <option value='process_adoption'>Procesează Adopție (2 apeluri)</option>
                    <option value='simple_donation'>Donație Simplă (1 apel)</option>
                    <option value='complete_adoption'>Adopție Completă (3 apeluri)</option>
                    <option value='complex_operation'>Operație Complexă (4 apeluri)</option>
                </select>
                <div style='font-size: 0.9em; color: #666; margin-top: 5px;'>
                    📝 Backend-ul va executa scenariul și va face numărul SPECIFIC de apeluri
                </div>
            </div>
            <div class='form-group'>
                <label>Număr de Apeluri AȘTEPTATE (pentru Verify):</label>
                <input type='number' id='expectedCalls' value='1' min='0' max='10'>
                <div style='font-size: 0.9em; color: #666; margin-top: 5px;'>
                    🎯 De câte ori AȘTEPȚI să fi fost apelat mock-ul?
                </div>
            </div>
            <div class='form-group'>
                <label>Mod Verificare:</label>
                <select id='verifyMode'>
                    <option value='exactly'>Exactly (exact numărul)</option>
                    <option value='atLeastOnce'>AtLeastOnce (minim 1)</option>
                    <option value='never'>Never (niciodată)</option>
                    <option value='atMost'>AtMost (maxim numărul)</option>
                </select>
            </div>
            <button class='test-button' onclick='testMoqVerify()'>🔢 Testează Moq Verify</button>
        </div>
        
        <div class='test-section'>
            <h3>🎯 Test Moq - Return Values Condiționat</h3>
            <p>Testează mock-uri care returnează valori diferite bazate pe parametri de intrare.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Input 'invalid' → Returnează null<br>
                • Input 'error' → Aruncă excepție<br>
                • Input numeric < 0 → Returnează error
            </div>
            <div class='form-group'>
                <label>Tip Mock pentru Return Value:</label>
                <select id='returnMockType'>
                    <option value='getUserById'>GetUserById (User mock)</option>
                    <option value='calculateDiscount'>CalculateDiscount (Donation mock)</option>
                    <option value='getAppointmentStatus'>GetAppointmentStatus (Veterinary mock)</option>
                    <option value='formatMessage'>FormatMessage (Logger mock)</option>
                </select>
            </div>
            <div class='form-group'>
                <label>Input pentru Test:</label>
                <input type='text' id='returnTestInput' value='1' placeholder='ID, sumă, status, etc.'>
            </div>
            <div class='form-group'>
                <label>Expected Return Type:</label>
                <select id='expectedReturnType'>
                    <option value='object'>Object (date complexe)</option>
                    <option value='string'>String</option>
                    <option value='number'>Number</option>
                    <option value='boolean'>Boolean</option>
                    <option value='null'>Null (pentru invalid input)</option>
                </select>
            </div>
            <button class='test-button' onclick='testMoqReturnValues()'>🎯 Testează Return Values</button>
        </div>
        
        <div class='test-section'>
            <h3>🔧 Test Stub - Date Predefinite</h3>
            <p>Testează stub-urile cu date predefinite și validări.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • ID invalid (ex: -1, 999)<br>
                • Date în trecut pentru programări<br>
                • Email-uri din blacklist
            </div>
            <div class='form-group'>
                <label>Tip Stub:</label>
                <select id='stubType'>
                    <option value='user'>User Data Stub</option>
                    <option value='appointment'>Appointment Stub</option>
                    <option value='payment'>Payment Stub</option>
                    <option value='notification'>Notification Stub</option>
                </select>
            </div>
            <div class='form-group'>
                <label>ID pentru Test:</label>
                <input type='number' id='stubId' value='1' min='-10' max='1000'>
            </div>
            <div class='form-group'>
                <label>Data (pentru programări):</label>
                <input type='date' id='stubDate' value='{DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")}'>
            </div>
            <button class='test-button' onclick='testStubData()'>🔧 Testează Stub Data</button>
        </div>
        
        <div class='test-section'>
            <h3>⚡ Test Performance Mock</h3>
            <p>Testează performanța mock-urilor și timeout-uri.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Delay peste 5000ms → Timeout<br>
                • Operații peste 100 → Overload<br>
                • Memory usage peste 1000MB
            </div>
            <div class='form-group'>
                <label>Tip Test Performance:</label>
                <select id='perfTestType'>
                    <option value='latency'>Latency Test</option>
                    <option value='throughput'>Throughput Test</option>
                    <option value='memory'>Memory Usage Test</option>
                    <option value='concurrent'>Concurrent Operations</option>
                </select>
            </div>
            <div class='form-group'>
                <label>Delay (ms):</label>
                <input type='number' id='perfDelay' value='1000' min='0' max='10000'>
            </div>
            <div class='form-group'>
                <label>Numărul de operații:</label>
                <input type='number' id='perfOperations' value='10' min='1' max='200'>
            </div>
            <button class='test-button' onclick='testPerformanceMock()'>⚡ Testează Performance</button>
        </div>
        
        <div class='test-section'>
            <h3>🎯 Test Validare Complexă</h3>
            <p>Testează validări complexe cu multiple condiții.</p>
            <div class='warning-box'>
                <strong>💡 Cum să provoace FAIL:</strong><br>
                • Email + Vârstă: sub 18 ani → Respins<br>
                • Donație + Frecvență: peste 5 pe zi → Blocat<br>
                • Programare + Disponibilitate: weekend → Indisponibil
            </div>
            <div class='form-group'>
                <label>Tip Validare:</label>
                <select id='validationType' onchange='updateValidationFields()'>
                    <option value='age_email'>Vârstă + Email</option>
                    <option value='donation_frequency'>Donație + Frecvență</option>
                    <option value='appointment_availability'>Programare + Disponibilitate</option>
                    <option value='multi_field'>Validare Multi-Field</option>
                </select>
            </div>
            <div class='form-group'>
                <label>Email:</label>
                <input type='email' id='validationEmail' value='test@example.com'>
            </div>
            <div class='form-group'>
                <label>Vârstă:</label>
                <input type='number' id='validationAge' value='25' min='1' max='100'>
            </div>
            
            <!-- Câmp pentru Programare + Disponibilitate -->
            <div class='form-group' id='daysField' style='display: none;'>
                <label>Zile în viitor pentru programare:</label>
                <input type='number' id='validationDays' value='3' min='1' max='30'>
                <small style='color: #666; font-size: 0.9em;'>
                    📅 Numărul de zile de la azi pentru programare (evită weekend: Sâmbătă/Duminică)
                </small>
            </div>
            
            <!-- Câmp pentru celelalte validări -->
            <div class='form-group' id='amountField'>
                <label id='amountLabel'>Sumă/Frecvență:</label>
                <input type='number' id='validationAmount' value='100' min='1' max='10000'>
                <small id='amountHelp' style='color: #666; font-size: 0.9em;'>
                    📊 Suma pentru validare (între 1 și 1000)
                </small>
            </div>
            
            <button class='test-button' onclick='testComplexValidation()'>🎯 Testează Validare Complexă</button>
        </div>
        
        <div class='test-section' style='background: linear-gradient(135deg, rgba(255, 107, 107, 0.1), rgba(255, 159, 64, 0.1)); border-left: 5px solid #ff6b6b;'>
            <h3>🆕 Test Adăugare Câine sau Adoptator</h3>
            <p>Testează validările complete pentru adăugarea câinilor sau adoptatorilor.</p>
            
            <div class='form-group'>
                <label><strong style='font-size: 1.1em;'>Ce dorești să adaugi?</strong></label>
                <select id='entityType' onchange='toggleEntityFields()' style='font-size: 1.05em; font-weight: bold;'>
                    <option value='dog'>🐕 Câine Nou</option>
                    <option value='adopter'>👤 Adoptator Nou</option>
                </select>
            </div>
            
            <!-- Câmpuri pentru Câine -->
            <div id='dogFields' style='display: block; background: rgba(111, 66, 193, 0.05); padding: 20px; border-radius: 10px; margin: 15px 0;'>
                <h4 style='color: #6f42c1; margin-top: 0;'>🐕 Informații Câine</h4>
                <div class='warning-box'>
                    <strong>❌ Validări care provoacă FAIL:</strong><br>
                    • Nume gol sau null<br>
                    • Vârstă > 20 ani sau < 0<br>
                    • Greutate > 100 kg sau <= 0
                </div>
                <div class='form-group'>
                    <label>Nume Câine:</label>
                    <input type='text' id='dogName' value='Rex' placeholder='Introdu numele câinelui'>
                </div>
                <div class='form-group'>
                    <label>Vârstă (ani):</label>
                    <input type='number' id='dogAge' value='5' min='0' max='25'>
                    <small style='color: #666; font-size: 0.9em;'>📝 MAX: 20 ani (încearcă 25 pentru FAIL)</small>
                </div>
                <div class='form-group'>
                    <label>Greutate (kg):</label>
                    <input type='number' id='dogWeight' value='25' min='0' max='150' step='0.1'>
                    <small style='color: #666; font-size: 0.9em;'>📝 MAX: 100 kg (încearcă 120 pentru FAIL)</small>
                </div>
            </div>
            
            <!-- Câmpuri pentru Adoptator -->
            <div id='adopterFields' style='display: none; background: rgba(40, 167, 69, 0.05); padding: 20px; border-radius: 10px; margin: 15px 0;'>
                <h4 style='color: #28a745; margin-top: 0;'>👤 Informații Adoptator</h4>
                <div class='warning-box'>
                    <strong>❌ Validări care provoacă FAIL:</strong><br>
                    • Nume gol, null sau doar spații<br>
                    • Email fără @ sau gol<br>
                    • Telefon nu este format 07XXXXXXXX (10 cifre, începe cu 07)
                </div>
                <div class='form-group'>
                    <label>Nume Adoptator:</label>
                    <input type='text' id='adopterName' value='Ion Popescu' placeholder='Introdu numele adoptatorului'>
                </div>
                <div class='form-group'>
                    <label>Email:</label>
                    <input type='email' id='adopterEmail' value='ion@example.com' placeholder='email@example.com'>
                    <small style='color: #666; font-size: 0.9em;'>📝 Trebuie să conțină @ (încearcă fără @ pentru FAIL)</small>
                </div>
                <div class='form-group'>
                    <label>Telefon:</label>
                    <input type='tel' id='adopterPhone' value='0743099200' placeholder='07XXXXXXXX'>
                    <small style='color: #666; font-size: 0.9em;'>📝 Format: 07XXXXXXXX - 10 cifre (încearcă 123 pentru FAIL)</small>
                </div>
            </div>
            
            <button class='test-button' onclick='testAddEntity()' style='background: linear-gradient(135deg, #ff6b6b, #ee5a6f); font-size: 1.1em;'>
                🧪 Testează Adăugare Entitate
            </button>
        </div>";
    }
    
    private static string GetStyles()
    {
        return @"
    <style>
        body { font-family: 'Poppins', sans-serif; background: linear-gradient(135deg, #667eea 0%, #764ba2 50%, #f093fb 100%); min-height: 100vh; margin: 0; padding: 20px; }
        .container { background: rgba(255, 255, 255, 0.95); backdrop-filter: blur(20px); border-radius: 25px; padding: 50px; max-width: 1200px; margin: 0 auto; box-shadow: 0 25px 50px rgba(0, 0, 0, 0.15); }
        h1 { text-align: center; font-size: 3em; color: #2c3e50; margin-bottom: 30px; }
        .back-btn { display: inline-block; background: linear-gradient(135deg, #6c757d, #495057); color: white; padding: 12px 24px; border-radius: 10px; text-decoration: none; margin-bottom: 20px; font-weight: bold; }
        .test-section { background: rgba(255, 255, 255, 0.8); padding: 30px; border-radius: 20px; margin: 20px 0; box-shadow: 0 10px 30px rgba(0,0,0,0.1); }
        .form-group { margin: 15px 0; }
        label { display: block; margin-bottom: 5px; font-weight: bold; color: #2c3e50; }
        input, select { width: 100%; padding: 12px; border: 2px solid #e9ecef; border-radius: 10px; font-size: 1em; box-sizing: border-box; }
        .test-button { background: linear-gradient(135deg, #3498db, #2980b9); color: white; padding: 15px 30px; border: none; border-radius: 10px; font-size: 1.1em; font-weight: bold; cursor: pointer; width: 100%; margin: 10px 0; transition: all 0.3s ease; }
        .test-button:hover { transform: translateY(-2px); box-shadow: 0 10px 25px rgba(52, 152, 219, 0.3); }
        .test-results { background: rgba(255, 255, 255, 0.9); padding: 20px; border-radius: 15px; margin: 20px 0; min-height: 100px; border: 2px dashed #ddd; }
        .result-success { background: rgba(40, 167, 69, 0.1); border-color: #28a745; color: #155724; }
        .result-error { background: rgba(220, 53, 69, 0.1); border-color: #dc3545; color: #721c24; }
        .stats-overview { display: grid; grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); gap: 20px; margin: 20px 0; }
        .stat-box { background: linear-gradient(135deg, #17a2b8, #138496); color: white; padding: 20px; border-radius: 15px; text-align: center; }
        .stat-number { font-size: 2em; font-weight: bold; }
        .warning-box { background: rgba(255, 193, 7, 0.1); padding: 10px; border-radius: 8px; margin: 10px 0; border-left: 4px solid #ffc107; }
    </style>";
    }
    
    private static string GetScript()
    {
        return @"
    <script>
        function showResult(title, content, isSuccess = true) {
            const resultsDiv = document.getElementById('testResults');
            resultsDiv.className = 'test-results ' + (isSuccess ? 'result-success' : 'result-error');
            resultsDiv.innerHTML = '<h4>' + title + '</h4><div>' + content + '</div>';
        }
        
        async function testEmailService() {
            const email = document.getElementById('testEmail').value;
            const subject = document.getElementById('testSubject').value;
            const message = document.getElementById('testMessage').value;
            
            try {
                const response = await fetch('/api/test-email', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email, subject, message })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test EmailService - SUCCESS', 
                        '<strong>Email trimis către:</strong> ' + email + '<br>' +
                        '<strong>Subiect:</strong> ' + subject + '<br>' +
                        '<strong>Mesaj:</strong> ' + message + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString() + '<br>' +
                        '<strong>Status Mock:</strong> Email procesat cu succes!'
                    );
                } else {
                    showResult('❌ Test EmailService - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test EmailService - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testVeterinaryService() {
            const dogId = document.getElementById('testDogId').value;
            const type = document.getElementById('testConsultationType').value;
            
            try {
                const response = await fetch('/api/test-veterinary', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ dogId: parseInt(dogId), type })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test VeterinaryService - SUCCESS', 
                        '<strong>Câine:</strong> ' + result.dogName + '<br>' +
                        '<strong>Tip consultație:</strong> ' + type + '<br>' +
                        '<strong>Data programată:</strong> ' + new Date(Date.now() + 7*24*60*60*1000).toLocaleDateString() + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString() + '<br>' +
                        '<strong>Status Mock:</strong> Programare creată cu succes!'
                    );
                } else {
                    showResult('❌ Test VeterinaryService - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test VeterinaryService - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testDonationService() {
            const donorName = document.getElementById('testDonorName').value;
            const amount = parseFloat(document.getElementById('testAmount').value);
            const purpose = document.getElementById('testPurpose').value;
            
            try {
                const response = await fetch('/api/test-donation', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ donorName, amount, purpose })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test DonationService - SUCCESS', 
                        '<strong>Donator:</strong> ' + donorName + '<br>' +
                        '<strong>Sumă:</strong> ' + amount + ' RON<br>' +
                        '<strong>Scop:</strong> ' + purpose + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString() + '<br>' +
                        '<strong>Status Mock:</strong> Donație procesată cu succes!'
                    );
                } else {
                    showResult('❌ Test DonationService - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test DonationService - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testLoggerService() {
            const message = document.getElementById('testLogMessage').value;
            const logType = document.getElementById('testLogType').value;
            
            try {
                const response = await fetch('/api/test-logger', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ message, logType })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test Logger - SUCCESS', 
                        '<strong>Mesaj:</strong> ' + message + '<br>' +
                        '<strong>Tip:</strong> ' + logType.toUpperCase() + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString() + '<br>' +
                        '<strong>Status Mock:</strong> Mesaj înregistrat cu succes!'
                    );
                } else {
                    showResult('❌ Test Logger - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Logger - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testIntegratedFlow() {
            try {
                showResult('🔄 Test Integrat - ÎN PROGRES', 'Se testează fluxul complet cu toate mock-urile...');
                
                const dogResponse = await fetch('/api/dogs', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        name: 'TestDog_' + Date.now(),
                        breed: 'Test Breed',
                        age: 2,
                        weight: 15.5,
                        health: 'Excelentă'
                    })
                });
                
                const dogResult = await dogResponse.json();
                if (!dogResult.success) throw new Error('Adăugarea câinelui a eșuat');
                
                const vetResponse = await fetch('/api/veterinary', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({
                        dogId: dogResult.dog.id,
                        type: 'Control medical test',
                        appointmentDate: new Date(Date.now() + 7*24*60*60*1000).toISOString(),
                        veterinarian: 'Dr. Test',
                        observations: 'Test integrat mock-uri'
                    })
                });
                
                const vetResult = await vetResponse.json();
                if (!vetResult.success) throw new Error('Programarea veterinară a eșuat');
                
                showResult('✅ Test Integrat - SUCCESS', 
                    '<strong>🐕 Câine adăugat:</strong> ' + dogResult.dog.name + '<br>' +
                    '<strong>🏥 Programare creată:</strong> ' + vetResult.appointment.type + '<br>' +
                    '<strong>📝 Mock-uri activate:</strong> Logger, Veterinary, Email<br>' +
                    '<strong>⏰ Timestamp:</strong> ' + new Date().toLocaleString() + '<br>' +
                    '<strong>✨ Status:</strong> Fluxul complet funcționează perfect!'
                );
            } catch (error) {
                showResult('❌ Test Integrat - FAILED', 'Eroare în fluxul integrat: ' + error.message, false);
            }
        }
        
        async function testMoqBehavior() {
            const mockType = document.getElementById('moqMockType').value;
            const scenario = document.getElementById('moqScenario').value;
            const parameter = document.getElementById('moqParameter').value;
            
            try {
                const response = await fetch('/api/test-moq', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ mockType, scenario, parameter })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test Moq - SUCCESS', 
                        '<strong>Mock Type:</strong> ' + mockType + '<br>' +
                        '<strong>Scenario:</strong> ' + scenario + '<br>' +
                        '<strong>Parameter:</strong> ' + parameter + '<br>' +
                        '<strong>Behavior:</strong> ' + result.behavior + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString()
                    );
                } else {
                    showResult('❌ Test Moq - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Moq - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testMoqVerify() {
            const serviceType = document.getElementById('verifyServiceType').value;
            const scenario = document.getElementById('testScenario').value;
            const expectedCalls = parseInt(document.getElementById('expectedCalls').value);
            const verifyMode = document.getElementById('verifyMode').value;
            
            try {
                const response = await fetch('/api/test-moq-verify', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ serviceType, scenario, expectedCalls, verifyMode })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test Moq Verify - SUCCESS', 
                        '<strong>Serviciu:</strong> ' + serviceType + '<br>' +
                        '<strong>Scenariu executat:</strong> ' + scenario + '<br>' +
                        '<strong>Apeluri EFECTIV făcute de scenariu:</strong> ' + result.actualCalls + '<br>' +
                        '<strong>Apeluri AȘTEPTATE:</strong> ' + expectedCalls + '<br>' +
                        '<strong>Mod verificare:</strong> ' + verifyMode + '<br>' +
                        '<strong>Status:</strong> ' + result.verifyStatus + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString()
                    );
                } else {
                    showResult('❌ Test Moq Verify - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Moq Verify - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testMoqReturnValues() {
            const mockType = document.getElementById('returnMockType').value;
            const testInput = document.getElementById('returnTestInput').value;
            const expectedReturnType = document.getElementById('expectedReturnType').value;
            
            try {
                const response = await fetch('/api/test-moq-return', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ mockType, testInput, expectedReturnType })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test Moq Return Values - SUCCESS', 
                        '<strong>Mock Type:</strong> ' + mockType + '<br>' +
                        '<strong>Input:</strong> ' + testInput + '<br>' +
                        '<strong>Expected Return Type:</strong> ' + expectedReturnType + '<br>' +
                        '<strong>Actual Return Type:</strong> ' + result.returnType + '<br>' +
                        '<strong>Return Value:</strong> ' + JSON.stringify(result.returnValue) + '<br>' +
                        '<strong>Match:</strong> ' + (result.typeMatch ? '✅ Match' : '❌ Mismatch') + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString()
                    );
                } else {
                    showResult('❌ Test Moq Return Values - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Moq Return Values - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testStubData() {
            const stubType = document.getElementById('stubType').value;
            const stubId = parseInt(document.getElementById('stubId').value);
            const stubDate = document.getElementById('stubDate').value;
            
            try {
                const response = await fetch('/api/test-stub', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ stubType, stubId, stubDate })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    showResult('✅ Test Stub - SUCCESS', 
                        '<strong>Stub Type:</strong> ' + stubType + '<br>' +
                        '<strong>ID:</strong> ' + stubId + '<br>' +
                        '<strong>Data:</strong> ' + stubDate + '<br>' +
                        '<strong>Stub Data:</strong> ' + JSON.stringify(result.data) + '<br>' +
                        '<strong>Timestamp:</strong> ' + new Date().toLocaleString()
                    );
                } else {
                    showResult('❌ Test Stub - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Stub - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        async function testPerformanceMock() {
            const perfTestType = document.getElementById('perfTestType').value;
            const perfDelay = parseInt(document.getElementById('perfDelay').value);
            const perfOperations = parseInt(document.getElementById('perfOperations').value);
            
            try {
                showResult('🔄 Test Performance - ÎN PROGRES', 'Se testează performanța cu ' + perfOperations + ' operații...');
                
                const startTime = Date.now();
                const response = await fetch('/api/test-performance', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ perfTestType, perfDelay, perfOperations })
                });
                
                const result = await response.json();
                const endTime = Date.now();
                const totalTime = endTime - startTime;
                
                if (result.success) {
                    showResult('✅ Test Performance - SUCCESS', 
                        '<strong>Test Type:</strong> ' + perfTestType + '<br>' +
                        '<strong>Operații:</strong> ' + perfOperations + '<br>' +
                        '<strong>Delay configurat:</strong> ' + perfDelay + 'ms<br>' +
                        '<strong>Timp total:</strong> ' + totalTime + 'ms<br>' +
                        '<strong>Performanță:</strong> ' + result.performance + '<br>' +
                        '<strong>Status:</strong> ' + (totalTime < 5000 ? 'RAPID' : 'LENT')
                    );
                } else {
                    showResult('❌ Test Performance - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Performance - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        function updateValidationFields() {
            const validationType = document.getElementById('validationType').value;
            const daysField = document.getElementById('daysField');
            const amountField = document.getElementById('amountField');
            const amountLabel = document.getElementById('amountLabel');
            const amountHelp = document.getElementById('amountHelp');
            const amountInput = document.getElementById('validationAmount');
            daysField.style.display = 'none';
            amountField.style.display = 'none';
            switch(validationType) {
                case 'age_email':
                    amountField.style.display = 'none';
                    break;
                    
                case 'donation_frequency':
                    amountField.style.display = 'block';
                    amountLabel.textContent = 'Număr de donații pe zi:';
                    amountHelp.textContent = '📊 Maxim 5 donații pe zi, suma totală max 1000 RON';
                    amountInput.value = 3;
                    amountInput.max = 10;
                    break;
                    
                case 'appointment_availability':
                    daysField.style.display = 'block';
                    amountField.style.display = 'none';
                    break;
                    
                case 'multi_field':
                    amountField.style.display = 'block';
                    amountLabel.textContent = 'Sumă pentru validare:';
                    amountHelp.textContent = '📊 Suma trebuie să fie între 1 și 1000';
                    amountInput.value = 100;
                    amountInput.max = 10000;
                    break;
            }
        }
        
        window.addEventListener('load', function() {
            updateValidationFields();
        });
        
        async function testComplexValidation() {
            const validationType = document.getElementById('validationType').value;
            const email = document.getElementById('validationEmail').value;
            const age = parseInt(document.getElementById('validationAge').value);
            let amount;
            if (validationType === 'appointment_availability') {
                amount = parseInt(document.getElementById('validationDays').value);
            } else {
                amount = parseInt(document.getElementById('validationAmount').value) || 0;
            }
            try {
                const response = await fetch('/api/test-complex-validation', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ validationType, email, age, amount })
                });
                
                const result = await response.json();
                
                if (result.success) {
                    let amountLabel = 'Sumă/Frecvență';
                    if (validationType === 'appointment_availability') {
                        amountLabel = 'Zile pentru programare';
                    } else if (validationType === 'donation_frequency') {
                        amountLabel = 'Donații pe zi';
                    }
                    const score = result.score || 0;
                    const isSuccess = score >= 75; // SUCCESS doar dacă scorul e >= 75
                    
                    let title = '';
                    let statusEmoji = '';
                    
                    if (score === 100) {
                        title = '✅ Test Validare Complexă - PERFECT';
                        statusEmoji = '🎯';
                    } else if (score >= 75) {
                        title = '✅ Test Validare Complexă - SUCCESS';
                        statusEmoji = '✔️';
                    } else if (score >= 50) {
                        title = '⚠️ Test Validare Complexă - PARȚIAL (SUB 75%)';
                        statusEmoji = '⚠️';
                    } else if (score > 0) {
                        title = '❌ Test Validare Complexă - FAILED (SUB 50%)';
                        statusEmoji = '❌';
                    } else {
                        title = '❌ Test Validare Complexă - FAILED COMPLET (0%)';
                        statusEmoji = '💥';
                    }
                    
                    showResult(title, 
                        statusEmoji + ' <strong>Scor validare:</strong> ' + score + '/100<br>' +
                        '<strong>Tip Validare:</strong> ' + validationType + '<br>' +
                        '<strong>Email:</strong> ' + email + '<br>' +
                        '<strong>Vârstă:</strong> ' + age + ' ani<br>' +
                        '<strong>' + amountLabel + ':</strong> ' + amount + '<br>' +
                        '<strong>Validări trecute:</strong> ' + (result.validations.length > 0 ? result.validations.join(', ') : 'NICIO validare') + '<br>',
                        isSuccess
                    );
                } else {
                    showResult('❌ Test Validare Complexă - FAILED', result.error, false);
                }
            } catch (error) {
                showResult('❌ Test Validare Complexă - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
        
        function toggleEntityFields() {
            const entityType = document.getElementById('entityType').value;
            const dogFields = document.getElementById('dogFields');
            const adopterFields = document.getElementById('adopterFields');
            
            if (entityType === 'dog') {
                dogFields.style.display = 'block';
                adopterFields.style.display = 'none';
            } else {
                dogFields.style.display = 'none';
                adopterFields.style.display = 'block';
            }
        }
        
        async function testAddEntity() {
            const entityType = document.getElementById('entityType').value;
            
            let requestData = { type: entityType };
            
            if (entityType === 'dog') {
                const name = document.getElementById('dogName').value;
                const age = parseInt(document.getElementById('dogAge').value);
                const weight = parseFloat(document.getElementById('dogWeight').value);
                requestData = { ...requestData, name, age, weight };
            } else {
                const name = document.getElementById('adopterName').value;
                const email = document.getElementById('adopterEmail').value;
                const phone = document.getElementById('adopterPhone').value;
                requestData = { ...requestData, name, email, phone };
            }
            
            try {
                const response = await fetch('/api/test-add-entity', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify(requestData)
                });
                
                const result = await response.json();
                
                if (result.success) {
                    const entityIcon = entityType === 'dog' ? '🐕' : '👤';
                    const dataDetails = Object.entries(result.data)
                        .map(([key, value]) => '<strong>' + key.charAt(0).toUpperCase() + key.slice(1) + ':</strong> ' + value)
                        .join('<br>');
                    
                    showResult(
                        '✅ Test Adăugare ' + result.entity + ' - SUCCESS',
                        entityIcon + ' ' + result.message + '<br><br>' +
                        '<div style=""background: rgba(40, 167, 69, 0.1); padding: 15px; border-radius: 8px; margin-top: 10px;"">' +
                        '<strong>📋 Date validate:</strong><br>' + dataDetails +
                        '</div>',
                        true
                    );
                } else {
                    const entityIcon = entityType === 'dog' ? '🐕' : '👤';
                    const errorsList = result.errors
                        .map(err => '• ' + err)
                        .join('<br>');
                    
                    showResult(
                        '❌ Test Adăugare ' + result.entity + ' - FAILED',
                        entityIcon + ' ' + result.message + '<br><br>' +
                        '<div style=""background: rgba(220, 53, 69, 0.1); padding: 15px; border-radius: 8px; margin-top: 10px; border-left: 4px solid #dc3545;"">' +
                        '<strong style=""color: #dc3545;"">🚫 Erori de validare (' + result.errors.length + '):</strong><br>' +
                        errorsList +
                        '</div>' +
                        '<div style=""background: rgba(108, 117, 125, 0.1); padding: 15px; border-radius: 8px; margin-top: 10px;"">' +
                        '<strong>📋 Date trimise:</strong><br>' +
                        Object.entries(result.data)
                            .map(([key, value]) => '<strong>' + key + ':</strong> ' + value)
                            .join('<br>') +
                        '</div>',
                        false
                    );
                }
            } catch (error) {
                showResult('❌ Test Adăugare Entitate - ERROR', 'Eroare de conexiune: ' + error.message, false);
            }
        }
    </script>";
    }
}

