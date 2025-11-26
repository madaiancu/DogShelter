namespace DogShelter.Pages;

public static class MockDemoPage
{
    public static string GenerateHtml()
    {
        return @"
<!DOCTYPE html>
<html>
<head>
    <title>🎯 Demo Mock-uri Avansate - DogShelter</title>
    <meta charset='utf-8'>
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
            max-width: 1400px;
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
        .demo-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
            gap: 25px;
            margin: 30px 0;
        }
        .demo-card {
            background: white;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.1);
            border-left: 5px solid #6f42c1;
        }
        .demo-card h3 {
            color: #6f42c1;
            margin-top: 0;
        }
        .test-btn {
            background: linear-gradient(135deg, #6f42c1, #9b59b6);
            color: white;
            padding: 12px 24px;
            border: none;
            border-radius: 10px;
            font-weight: bold;
            cursor: pointer;
            width: 100%;
            margin: 10px 0;
            transition: all 0.3s ease;
        }
        .test-btn:hover {
            transform: translateY(-2px);
            box-shadow: 0 10px 25px rgba(111, 66, 193, 0.3);
        }
        .result-box {
            background: rgba(0, 0, 0, 0.05);
            padding: 15px;
            border-radius: 10px;
            margin: 15px 0;
            font-family: 'Courier New', monospace;
            font-size: 0.9em;
            max-height: 300px;
            overflow-y: auto;
        }
        .success {
            border-left: 4px solid #28a745;
            background: rgba(40, 167, 69, 0.1);
        }
        .error {
            border-left: 4px solid #dc3545;
            background: rgba(220, 53, 69, 0.1);
        }
        .feature-list {
            background: rgba(111, 66, 193, 0.1);
            padding: 20px;
            border-radius: 10px;
            margin: 20px 0;
        }
        .feature-list li {
            margin: 10px 0;
        }
        .code-example {
            background: #2d2d2d;
            color: #f8f8f2;
            padding: 15px;
            border-radius: 10px;
            overflow-x: auto;
            margin: 15px 0;
        }
    </style>
</head>
<body>
    <div class='container'>
        <a href='/test-mocks' class='back-btn'>← Înapoi la Teste Mock</a>
        <h1>🎯 Demo Mock-uri Avansate</h1>
        
        <div class='feature-list'>
            <h3>✨ Funcționalități Noi ale Mock-urilor:</h3>
            <ul>
                <li><strong>📊 State Tracking:</strong> Monitorizează numărul de apeluri și istoricul complet</li>
                <li><strong>✅ Validări Realiste:</strong> Verifică parametrii de intrare (email format, amount range, etc.)</li>
                <li><strong>🎭 Comportament Configurabil:</strong> Simulează erori, timeout-uri, limite</li>
                <li><strong>📝 Istoricul Apelurilor:</strong> Păstrează toate detaliile fiecărui apel</li>
                <li><strong>🔄 Reset Functionality:</strong> Resetează starea pentru teste fresh</li>
                <li><strong>🔍 Query Methods:</strong> Interogări detaliate despre starea mock-ului</li>
            </ul>
        </div>

        <div class='demo-grid'>
            <!-- EmailServiceMock Demo -->
            <div class='demo-card'>
                <h3>📧 EmailServiceMock - Validări</h3>
                <p>Mock-ul de email acum validează:</p>
                <ul>
                    <li>Format email valid (@)</li>
                    <li>Subject nu este gol</li>
                    <li>Simulare failure</li>
                </ul>
                <button class='test-btn' onclick='testEmailValidation()'>🧪 Testează Validare Email</button>
                <button class='test-btn' onclick='testEmailHistory()'>📊 Vezi Istoric Apeluri</button>
                <div id='emailResult' class='result-box' style='display: none;'></div>
            </div>

            <!-- VeterinaryServiceMock Demo -->
            <div class='demo-card'>
                <h3>🏥 VeterinaryServiceMock - Limite</h3>
                <p>Mock-ul veterinar are:</p>
                <ul>
                    <li>✅ Maxim <strong style='color:#e74c3c'>10 programări/zi</strong></li>
                    <li>✅ Nu permite programări în trecut</li>
                    <li>✅ Simulare program complet</li>
                </ul>
                <button class='test-btn' onclick='testVetLimits()'>🧪 Testează Limite Standard</button>
                <button class='test-btn' onclick='testVetPast()'>⏰ Testează Data în Trecut</button>
                <button class='test-btn' style='background: linear-gradient(135deg, #e74c3c, #c0392b); font-size: 1.05em;' onclick='testMaxAppointments()'>🔥 DEMONSTRAȚIE: Max 10 Programări/Zi</button>
                <div id='vetResult' class='result-box' style='display: none;'></div>
            </div>

            <!-- DonationServiceMock Demo -->
            <div class='demo-card'>
                <h3>💰 DonationServiceMock - Range & Total</h3>
                <p>Mock-ul de donații:</p>
                <ul>
                    <li>Min: 1 RON, Max: 10,000 RON</li>
                    <li>Validare nume donator (min 2 char)</li>
                    <li>Tracking total donații</li>
                </ul>
                <button class='test-btn' onclick='testDonationRange()'>🧪 Testează Range Donații</button>
                <button class='test-btn' onclick='testDonationTotal()'>💰 Vezi Total Donații</button>
                <div id='donationResult' class='result-box' style='display: none;'></div>
            </div>

            <!-- LoggerServiceMock Demo -->
            <div class='demo-card'>
                <h3>📝 LoggerServiceMock - Filtrare</h3>
                <p>Mock-ul de logger oferă:</p>
                <ul>
                    <li>Contorizare separată INFO/ERROR</li>
                    <li>Filtrare log-uri după level</li>
                    <li>Căutare în mesaje</li>
                </ul>
                <button class='test-btn' onclick='testLoggerCounts()'>🧪 Testează Contorizare Log-uri</button>
                <button class='test-btn' onclick='testLoggerSearch()'>🔍 Caută în Log-uri</button>
                <div id='loggerResult' class='result-box' style='display: none;'></div>
            </div>

            <!-- Mock Comparison -->
            <div class='demo-card' style='grid-column: 1 / -1;'>
                <h3>📊 Comparație: Mock Simplu vs Mock Avansat</h3>
                <div style='display: grid; grid-template-columns: 1fr 1fr; gap: 20px;'>
                    <div>
                        <h4 style='color: #dc3545;'>❌ Mock Simplu (Înainte):</h4>
                        <div class='code-example'>
public bool SendEmail(string to, string subject, string body)
{
    Console.WriteLine($""Email: {to}"");
    return true; // ÎNTOTDEAUNA SUCCESS
}
                        </div>
                        <p><strong>Probleme:</strong></p>
                        <ul>
                            <li>❌ Nu validează parametrii</li>
                            <li>❌ Nu poate simula erori</li>
                            <li>❌ Nu păstrează istoricul</li>
                            <li>❌ Dificil de verificat în teste</li>
                        </ul>
                    </div>
                    <div>
                        <h4 style='color: #28a745;'>✅ Mock Avansat (Acum):</h4>
                        <div class='code-example'>
public bool SendEmail(string to, string subject, string body)
{
    _callCount++;
    
    if (!to.Contains(""@"")) return false;  // VALIDARE
    if (SimulateFailure) return false;      // CONFIGURABIL
    
    _callHistory.Add(new EmailCall { ... }); // ISTORIC
    return true;
}

int GetCallCount();
List<EmailCall> GetCallHistory();
void ResetMock();
                        </div>
                        <p><strong>Beneficii:</strong></p>
                        <ul>
                            <li>✅ Validări realiste</li>
                            <li>✅ Comportament configurabil</li>
                            <li>✅ Istoric complet apeluri</li>
                            <li>✅ Perfect pentru unit tests</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>
        function showResult(elementId, content, isSuccess = true) {
            const element = document.getElementById(elementId);
            element.style.display = 'block';
            element.className = isSuccess ? 'result-box success' : 'result-box error';
            element.innerHTML = content;
        }

        async function testEmailValidation() {
            showResult('emailResult', '⏳ Se testează validări email...', true);
            
            try {
                const test1 = await fetch('/api/demo/email-validation', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email: 'invalid-email', subject: 'Test', body: 'Test body' })
                }).then(r => r.json());

                const test2 = await fetch('/api/demo/email-validation', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email: 'valid@email.com', subject: '', body: 'Test body' })
                }).then(r => r.json());

                const test3 = await fetch('/api/demo/email-validation', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' },
                    body: JSON.stringify({ email: 'valid@email.com', subject: 'Valid Test', body: 'Test body' })
                }).then(r => r.json());

                const results = `
<strong>Test 1: Email fără @</strong>
Result: ${test1.success ? '✅ SUCCESS' : '❌ FAILED (CORECT!)'}
Reason: ${test1.reason || 'N/A'}

<strong>Test 2: Subject gol</strong>
Result: ${test2.success ? '✅ SUCCESS' : '❌ FAILED (CORECT!)'}
Reason: ${test2.reason || 'N/A'}

<strong>Test 3: Email valid</strong>
Result: ${test3.success ? '✅ SUCCESS (CORECT!)' : '❌ FAILED'}
Reason: ${test3.reason || 'N/A'}
                `;
                
                showResult('emailResult', results, true);
            } catch (error) {
                showResult('emailResult', '❌ Error: ' + error.message, false);
            }
        }

        async function testEmailHistory() {
            showResult('emailResult', '⏳ Se încarcă istoric email...', true);
            
            try {
                const response = await fetch('/api/demo/email-history').then(r => r.json());
                
                let html = `<strong>📊 Istoric Apeluri Email (Total: ${response.totalCalls})</strong><br><br>`;
                
                response.history.forEach((call, index) => {
                    html += `
<strong>#${index + 1}:</strong> 
To: ${call.to} | Subject: ${call.subject}
Success: ${call.success ? '✅' : '❌'} | Time: ${new Date(call.calledAt).toLocaleTimeString()}<br>
                    `;
                });
                
                showResult('emailResult', html, true);
            } catch (error) {
                showResult('emailResult', '❌ Error: ' + error.message, false);
            }
        }

        async function testVetLimits() {
            showResult('vetResult', '⏳ Se testează limite programări...', true);
        }

        async function testVetPast() {
            showResult('vetResult', '⏳ Se testează data în trecut...', true);
        }

        async function testDonationRange() {
            showResult('donationResult', '⏳ Se testează range donații...', true);
        }

        async function testDonationTotal() {
            showResult('donationResult', '⏳ Se încarcă total donații...', true);
        }

        async function testLoggerCounts() {
            showResult('loggerResult', '⏳ Se încarcă statistici log-uri...', true);
        }

        async function testLoggerSearch() {
            showResult('loggerResult', '⏳ Se caută în log-uri...', true);
        }

        async function testMaxAppointments() {
            showResult('vetResult', '⏳ Se testează limita de 10 programări pe zi...', true);
            
            try {
                const response = await fetch('/api/demo/vet-max-appointments-test', {
                    method: 'POST',
                    headers: { 'Content-Type': 'application/json' }
                });
                
                const result = await response.json();
                
                if (!result.success) {
                    showResult('vetResult', '❌ Error: ' + result.error, false);
                    return;
                }
                
                let output = '<strong style=""font-size:1.2em;"">' + result.message + '</strong><br><br>' +
                    '📅 <strong>Data testată:</strong> ' + result.testDate + '<br>' +
                    '🔢 <strong>Încercări totale:</strong> ' + result.totalAttempts + '<br>' +
                    '✅ <strong>Programări acceptate:</strong> ' + result.successfulAppointments + '<br>' +
                    '❌ <strong>Programări respinse:</strong> ' + result.failedAppointments + '<br>' +
                    '🚫 <strong>Limită/zi:</strong> ' + result.maxAppointmentsPerDay + '<br>' +
                    (result.limitReached ? '🎯 <strong style=""color:#28a745"">LIMITA A FUNCȚIONAT CORECT!</strong>' : '⚠️ <strong style=""color:#e74c3c"">ATENȚIE: Limita nu a funcționat!</strong>') + '<br><br>' +
                    '<hr style=""margin:15px 0;border:1px solid #ddd;"">' +
                    '<strong>📋 Detalii Programări (primele 11):</strong><br>';
                
                result.details.forEach((detail, index) => {
                    const icon = detail.success ? '✅' : '❌';
                    const status = detail.success ? 'SUCCESS' : 'FAILED';
                    const color = detail.success ? '#28a745' : '#e74c3c';
                    
                    output += icon + ' <strong>Programare ' + detail.appointmentNumber + ':</strong> ' +
                        '<span style=""color:' + color + ';font-weight:bold;"">' + status + '</span>' +
                        (detail.success ? '' : ' - ' + detail.reason) +
                        ' (' + detail.dogId + ', ' + detail.time + ')<br>';
                });
                
                showResult('vetResult', output, result.limitReached);
            } catch (error) {
                showResult('vetResult', '❌ Error: ' + error.message, false);
            }
        }
    </script>
</body>
</html>";
    }
}

