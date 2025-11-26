using System.Text.Json;
using Microsoft.AspNetCore.Http;
using DogShelter.Interfaces;
using DogShelter.Data;
using DogShelter.Stubs;

namespace DogShelter.Tests;

public static class TestsApi
{
    public static void MapTestsEndpoints(this WebApplication app)
    {
        app.MapPost("/api/test-email", async (HttpContext context, IEmailService email, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var emailAddr = json.GetProperty("email").GetString();
                var subject = json.GetProperty("subject").GetString();
                var message = json.GetProperty("message").GetString();
                
                if (string.IsNullOrEmpty(emailAddr) || !emailAddr.Contains("@")) {
                    logger.LogError($"Test EmailService FAILED: Email invalid - {emailAddr}");
                    return Results.Json(new { success = false, error = "Email invalid! Trebuie să conțină @" });
                }
                
                if (string.IsNullOrEmpty(subject) || subject.Length < 3) {
                    logger.LogError($"Test EmailService FAILED: Subiect prea scurt - {subject}");
                    return Results.Json(new { success = false, error = "Subiectul trebuie să aibă cel puțin 3 caractere!" });
                }
                
                if (emailAddr.Contains("fail") || subject.Contains("fail")) {
                    logger.LogError($"Test EmailService FAILED: Conține 'fail' - {emailAddr}");
                    return Results.Json(new { success = false, error = "Testul a eșuat intenționat! (conține 'fail')" });
                }
                
                logger.LogInfo($"Test EmailService: {emailAddr} - {subject}");
                var result = email.SendEmail(emailAddr, subject, message);
                
                return Results.Json(new { success = result, timestamp = DateTime.Now });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-veterinary", async (HttpContext context, IVeterinaryService veterinary, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var dogId = json.GetProperty("dogId").GetInt32();
                var type = json.GetProperty("type").GetString();
                
                var dog = GlobalData.Dogs.FirstOrDefault(d => d.id == dogId);
                if (dog == null) {
                    return Results.Json(new { success = false, error = "Câinele nu a fost găsit" });
                }
                
                logger.LogInfo($"Test VeterinaryService: {dog.name} - {type}");
                var result = veterinary.ScheduleAppointment(dogId.ToString(), DateTime.Now.AddDays(7), type);
                
                return Results.Json(new { success = result, dogName = dog.name, timestamp = DateTime.Now });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-donation", async (HttpContext context, IDonationService donation, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var donorName = json.GetProperty("donorName").GetString();
                var amount = json.GetProperty("amount").GetDecimal();
                var purpose = json.GetProperty("purpose").GetString();
                
                if (string.IsNullOrEmpty(donorName) || donorName.Length < 2) {
                    logger.LogError($"Test DonationService FAILED: Nume donator invalid - {donorName}");
                    return Results.Json(new { success = false, error = "Numele donatorului trebuie să aibă cel puțin 2 caractere!" });
                }
                
                if (amount <= 0) {
                    logger.LogError($"Test DonationService FAILED: Sumă invalidă - {amount}");
                    return Results.Json(new { success = false, error = "Suma trebuie să fie mai mare decât 0!" });
                }
                
                if (amount > 10000) {
                    logger.LogError($"Test DonationService FAILED: Sumă prea mare - {amount}");
                    return Results.Json(new { success = false, error = "Suma nu poate fi mai mare de 10.000 RON!" });
                }
                
                if (donorName.ToLower().Contains("fail") || purpose.ToLower().Contains("fail")) {
                    logger.LogError($"Test DonationService FAILED: Conține 'fail' - {donorName}");
                    return Results.Json(new { success = false, error = "Testul a eșuat intenționat! (conține 'fail')" });
                }
                
                logger.LogInfo($"Test DonationService: {donorName} - {amount} RON");
                var result = donation.ProcessDonation(donorName, amount, purpose);
                
                return Results.Json(new { success = result, timestamp = DateTime.Now });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-logger", async (HttpContext context, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var message = json.GetProperty("message").GetString();
                var logType = json.GetProperty("logType").GetString();
                
                if (string.IsNullOrEmpty(message) || message.Length < 5) {
                    logger.LogError($"Test Logger FAILED: Mesaj prea scurt - {message}");
                    return Results.Json(new { success = false, error = "Mesajul trebuie să aibă cel puțin 5 caractere!" });
                }
                
                if (message.ToLower().Contains("fail") || message.ToLower().Contains("error test")) {
                    logger.LogError($"Test Logger FAILED: Mesaj interzis - {message}");
                    return Results.Json(new { success = false, error = "Testul a eșuat intenționat! (conține cuvinte interzise)" });
                }
                
                if (message.Length > 100) {
                    logger.LogError($"Test Logger FAILED: Mesaj prea lung - {message.Length} caractere");
                    return Results.Json(new { success = false, error = "Mesajul nu poate avea mai mult de 100 de caractere!" });
                }
                
                if (logType == "info") {
                    logger.LogInfo($"TEST: {message}");
                } else {
                    logger.LogError($"TEST: {message}");
                }
                
                return Results.Json(new { success = true, timestamp = DateTime.Now });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-moq", async (HttpContext context, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var mockType = json.GetProperty("mockType").GetString();
                var scenario = json.GetProperty("scenario").GetString();
                var parameter = json.GetProperty("parameter").GetString();
                
                logger.LogInfo($"Test Moq: {mockType} - {scenario} - {parameter}");
                
                switch (scenario) {
                    case "fail":
                        logger.LogError($"Moq {mockType} FAILED intenționat pentru {parameter}");
                        return Results.Json(new { success = false, error = $"Mock {mockType} a returnat false pentru scenario 'fail'" });
                        
                    case "error":
                        logger.LogError($"Moq {mockType} EXCEPTION pentru {parameter}");
                        return Results.Json(new { success = false, error = $"Mock {mockType} a aruncat excepție: Simulated exception for testing" });
                        
                    case "timeout":
                        logger.LogError($"Moq {mockType} TIMEOUT pentru {parameter}");
                        await Task.Delay(6000);
                        return Results.Json(new { success = false, error = $"Mock {mockType} timeout după 6 secunde" });
                        
                    default:
                        return Results.Json(new { 
                            success = true, 
                            behavior = $"Mock {mockType} executed successfully",
                            timestamp = DateTime.Now 
                        });
                }
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-moq-verify", async (HttpContext context, ILoggerService logger, IEmailService email, 
                                                     IVeterinaryService vet, IDonationService donation) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var serviceType = json.GetProperty("serviceType").GetString();
                var scenario = json.GetProperty("scenario").GetString();
                var expectedCalls = json.GetProperty("expectedCalls").GetInt32();
                var verifyMode = json.GetProperty("verifyMode").GetString();
                
                int actualCalls = (serviceType, scenario) switch {
                    ("email", "add_dog") => 1,
                    ("email", "process_adoption") => 2,
                    ("email", "simple_donation") => 1,
                    ("email", "complete_adoption") => 3,
                    ("email", "complex_operation") => 4,
                    ("veterinary", "add_dog") => 1,
                    ("veterinary", "process_adoption") => 1,
                    ("veterinary", "simple_donation") => 0,
                    ("veterinary", "complete_adoption") => 2,
                    ("veterinary", "complex_operation") => 3,
                    ("donation", "add_dog") => 0,
                    ("donation", "process_adoption") => 0,
                    ("donation", "simple_donation") => 1,
                    ("donation", "complete_adoption") => 2,
                    ("donation", "complex_operation") => 3,
                    ("logger", "add_dog") => 2,
                    ("logger", "process_adoption") => 3,
                    ("logger", "simple_donation") => 2,
                    ("logger", "complete_adoption") => 4,
                    ("logger", "complex_operation") => 5,
                    _ => 0
                };
                
                logger.LogInfo($"Test Moq Verify: {serviceType} - Scenario: {scenario} - Will make: {actualCalls} calls - Expected: {expectedCalls} - Mode: {verifyMode}");
                
                for (int i = 0; i < actualCalls; i++) {
                    switch (serviceType) {
                        case "email":
                            email.SendEmail($"test{i}@example.com", "Test Subject", $"Email #{i+1} from scenario: {scenario}");
                            break;
                        case "veterinary":
                            vet.ScheduleAppointment($"dog{i}", DateTime.Now.AddDays(i+1), $"Appointment #{i+1} from scenario: {scenario}");
                            break;
                        case "donation":
                            donation.ProcessDonation($"Donor{i}", 100 + (i * 50), $"Donation #{i+1} from scenario: {scenario}");
                            break;
                        case "logger":
                            logger.LogInfo($"Log message #{i+1} from scenario: {scenario}");
                            break;
                    }
                }
                
                var isVerified = verifyMode switch {
                    "exactly" => actualCalls == expectedCalls,
                    "atLeastOnce" => actualCalls >= 1,
                    "never" => actualCalls == 0,
                    "atMost" => actualCalls <= expectedCalls,
                    _ => false
                };
                
                if (!isVerified) {
                    logger.LogError($"Moq Verify FAILED: Scenario '{scenario}' made {actualCalls} calls, expected {verifyMode} {expectedCalls}");
                    return Results.Json(new { 
                        success = false, 
                        error = $"Verify failed! Scenario '{scenario}' made {actualCalls} {serviceType} calls, but expected {verifyMode}: {expectedCalls}" 
                    });
                }
                
                return Results.Json(new { 
                    success = true, 
                    scenario = scenario,
                    actualCalls = actualCalls,
                    verifyStatus = $"Verified {verifyMode}: Scenario made {actualCalls} calls, matched expectation of {expectedCalls}",
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-moq-return", async (HttpContext context, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var mockType = json.GetProperty("mockType").GetString();
                var testInput = json.GetProperty("testInput").GetString();
                var expectedReturnType = json.GetProperty("expectedReturnType").GetString();
                
                logger.LogInfo($"Test Moq Return: {mockType} - Input: {testInput} - Expected: {expectedReturnType}");
                
                if (testInput == "invalid") {
                    logger.LogError($"Moq Return FAILED: Invalid input - {testInput}");
                    return Results.Json(new { 
                        success = false, 
                        error = "Input 'invalid' causes mock to return null!" 
                    });
                }
                
                if (testInput == "error") {
                    logger.LogError($"Moq Return FAILED: Error input - {testInput}");
                    return Results.Json(new { 
                        success = false, 
                        error = "Input 'error' causes mock to throw exception!" 
                    });
                }
                
                if (int.TryParse(testInput, out var numericInput) && numericInput < 0) {
                    logger.LogError($"Moq Return FAILED: Negative input - {numericInput}");
                    return Results.Json(new { 
                        success = false, 
                        error = "Negative numeric input is not allowed!" 
                    });
                }
                
                object returnValue;
                string returnType;
                
                switch (mockType) {
                    case "getUserById":
                        returnValue = new { 
                            id = int.TryParse(testInput, out var id) ? id : 1, 
                            name = $"User_{testInput}", 
                            email = $"user{testInput}@test.com" 
                        };
                        returnType = "object";
                        break;
                        
                    case "calculateDiscount":
                        returnValue = int.TryParse(testInput, out var amount) 
                            ? (amount > 100 ? amount * 0.1 : 0) 
                            : 0;
                        returnType = "number";
                        break;
                        
                    case "getAppointmentStatus":
                        returnValue = int.TryParse(testInput, out var apptId) && apptId > 0 
                            ? "Confirmed" 
                            : "Pending";
                        returnType = "string";
                        break;
                        
                    case "formatMessage":
                        returnValue = $"Formatted: {testInput.ToUpper()}";
                        returnType = "string";
                        break;
                        
                    default:
                        returnValue = testInput;
                        returnType = "string";
                        break;
                }
                
                var typeMatch = returnType == expectedReturnType;
                
                if (!typeMatch) {
                    logger.LogError($"Moq Return TYPE MISMATCH: Expected {expectedReturnType}, got {returnType}");
                    return Results.Json(new { 
                        success = false, 
                        error = $"Type mismatch! Expected {expectedReturnType}, but mock returned {returnType}" 
                    });
                }
                
                return Results.Json(new { 
                    success = true, 
                    returnValue = returnValue,
                    returnType = returnType,
                    typeMatch = typeMatch,
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-stub", async (HttpContext context, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var stubType = json.GetProperty("stubType").GetString();
                var stubId = json.GetProperty("stubId").GetInt32();
                var stubDate = json.GetProperty("stubDate").GetString();
                
                logger.LogInfo($"Test Stub: {stubType} - ID: {stubId}");
                
                if (stubId < 0) {
                    logger.LogError($"Stub FAILED: ID invalid {stubId}");
                    return Results.Json(new { success = false, error = "ID-ul nu poate fi negativ!" });
                }
                
                if (stubId > 100) {
                    logger.LogError($"Stub FAILED: ID prea mare {stubId}");
                    return Results.Json(new { success = false, error = "ID-ul nu poate fi mai mare de 100!" });
                }
                
                if (DateTime.TryParse(stubDate, out var date) && date < DateTime.Now.Date) {
                    logger.LogError($"Stub FAILED: Data în trecut {stubDate}");
                    return Results.Json(new { success = false, error = "Data nu poate fi în trecut!" });
                }
                
                object stubData = stubType switch {
                    "user" => StubDataProvider.GetUserStub(stubId),
                    "appointment" => StubDataProvider.GetAppointmentStub(stubId, stubDate),
                    "payment" => StubDataProvider.GetPaymentStub(stubId),
                    "notification" => StubDataProvider.GetNotificationStub(stubId),
                    _ => StubDataProvider.GetGenericStub(stubId, stubType)
                };
                
                return Results.Json(new { 
                    success = true, 
                    data = stubData,
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-performance", async (HttpContext context, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var perfTestType = json.GetProperty("perfTestType").GetString();
                var perfDelay = json.GetProperty("perfDelay").GetInt32();
                var perfOperations = json.GetProperty("perfOperations").GetInt32();
                
                logger.LogInfo($"Test Performance: {perfTestType} - {perfOperations} operații cu {perfDelay}ms delay");
                
                if (perfDelay > 5000) {
                    logger.LogError($"Performance FAILED: Delay prea mare {perfDelay}ms");
                    return Results.Json(new { success = false, error = "Delay-ul nu poate fi mai mare de 5000ms (timeout)!" });
                }
                
                if (perfOperations > 100) {
                    logger.LogError($"Performance FAILED: Prea multe operații {perfOperations}");
                    return Results.Json(new { success = false, error = "Nu se pot executa mai mult de 100 de operații (overload)!" });
                }
                
                var startTime = DateTime.Now;
                for (int i = 0; i < perfOperations; i++) {
                    await Task.Delay(perfDelay / perfOperations);
                }
                var endTime = DateTime.Now;
                var totalTime = (endTime - startTime).TotalMilliseconds;
                
                var performance = perfTestType switch {
                    "latency" => $"Latență medie: {totalTime / perfOperations:F2}ms per operație",
                    "throughput" => $"Throughput: {perfOperations / (totalTime / 1000):F2} operații/secundă",
                    "memory" => $"Memory usage: {perfOperations * 10}MB (simulat)",
                    "concurrent" => $"Operații concurente: {perfOperations} finalizate în {totalTime:F0}ms",
                    _ => $"Test generic: {perfOperations} operații în {totalTime:F0}ms"
                };
                
                return Results.Json(new { 
                    success = true, 
                    performance = performance,
                    totalTime = totalTime,
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-complex-validation", async (HttpContext context, ILoggerService logger) => {
            try {
                var json = await context.Request.ReadFromJsonAsync<JsonElement>();
                var validationType = json.GetProperty("validationType").GetString();
                var email = json.GetProperty("email").GetString();
                var age = json.GetProperty("age").GetInt32();
                var amount = json.GetProperty("amount").GetInt32();
                
                logger.LogInfo($"Test Complex Validation: {validationType} - {email} - {age} - {amount}");
                
                var validations = new List<string>();
                var score = 0;
                
                switch (validationType) {
                    case "age_email":
                        if (age < 18) {
                            logger.LogError($"Validation FAILED: Vârstă sub 18 - {age}");
                            return Results.Json(new { success = false, error = "Vârsta trebuie să fie cel puțin 18 ani!" });
                        }
                        validations.Add("Vârstă validă");
                        score += 50;
                        
                        if (!email.Contains("@") || email.Length < 5) {
                            logger.LogError($"Validation FAILED: Email invalid - {email}");
                            return Results.Json(new { success = false, error = "Email-ul nu este valid!" });
                        }
                        validations.Add("Email valid");
                        score += 50;
                        break;
                        
                    case "donation_frequency":
                        if (amount > 5) {
                            logger.LogError($"Validation FAILED: Prea multe donații - {amount}");
                            return Results.Json(new { success = false, error = "Nu se pot face mai mult de 5 donații pe zi!" });
                        }
                        validations.Add("Frecvență donații OK");
                        score += 60;
                        
                        if (amount * 100 > 1000) {
                            logger.LogError($"Validation FAILED: Sumă prea mare - {amount * 100}");
                            return Results.Json(new { success = false, error = "Suma totală nu poate depăși 1000 RON pe zi!" });
                        }
                        validations.Add("Sumă în limite");
                        score += 40;
                        break;
                        
                    case "appointment_availability":
                        var testDate = DateTime.Now.AddDays(amount);
                        if (testDate.DayOfWeek == DayOfWeek.Saturday || testDate.DayOfWeek == DayOfWeek.Sunday) {
                            logger.LogError($"Validation FAILED: Weekend - {testDate.DayOfWeek}");
                            return Results.Json(new { success = false, error = "Nu se pot programa consultații în weekend!" });
                        }
                        validations.Add("Zi lucrătoare");
                        score += 70;
                        
                        if (age > 80) {
                            validations.Add("Prioritate vârstă");
                            score += 30;
                        } else {
                            validations.Add("Programare normală");
                            score += 20;
                        }
                        break;
                        
                    default:
                        if (age >= 18) { validations.Add("Vârstă OK"); score += 25; }
                        if (email.Contains("@")) { validations.Add("Email OK"); score += 25; }
                        if (amount > 0 && amount <= 1000) { validations.Add("Sumă OK"); score += 25; }
                        if (validations.Count == 3) { validations.Add("Toate câmpurile valide"); score += 25; }
                        
                        if (score < 75) {
                            logger.LogError($"Validation FAILED: Scor prea mic - {score}/100");
                            return Results.Json(new { 
                                success = false, 
                                error = $"Validare eșuată! Scor: {score}/100 (minim 75 necesar). Validări trecute: {string.Join(", ", validations)}" 
                            });
                        }
                        break;
                }
                
                return Results.Json(new { 
                    success = true, 
                    validations = validations,
                    score = score,
                    timestamp = DateTime.Now 
                });
            }
            catch (Exception ex) {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });

        app.MapPost("/api/test-add-entity", async (HttpRequest request, ILoggerService logger) =>
        {
            try
            {
                var body = await request.ReadFromJsonAsync<JsonElement>();
                if (body.ValueKind == JsonValueKind.Undefined || body.ValueKind == JsonValueKind.Null)
                    return Results.Json(new { success = false, error = "Body cannot be null" });

                string? type = body.GetProperty("type").GetString();
                
                if (type == "dog")
                {
                    string? name = body.GetProperty("name").GetString();
                    int age = body.GetProperty("age").GetInt32();
                    double weight = body.GetProperty("weight").GetDouble();

                    var validationErrors = new List<string>();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        validationErrors.Add("❌ Numele câinelui nu poate fi gol");
                    }

                    if (age > 20)
                    {
                        validationErrors.Add($"❌ Vârsta ({age} ani) depășește limita maximă de 20 ani");
                    }
                    else if (age < 0)
                    {
                        validationErrors.Add($"❌ Vârsta nu poate fi negativă ({age} ani)");
                    }

                    if (weight > 100)
                    {
                        validationErrors.Add($"❌ Greutatea ({weight} kg) depășește limita maximă de 100 kg");
                    }
                    else if (weight <= 0)
                    {
                        validationErrors.Add($"❌ Greutatea trebuie să fie mai mare decât 0 ({weight} kg)");
                    }

                    if (validationErrors.Count > 0)
                    {
                        logger.LogError($"Test Add Dog FAILED: {string.Join(", ", validationErrors)}");
                        return Results.Json(new 
                        { 
                            success = false, 
                            type = "dog",
                            entity = "Câine",
                            errors = validationErrors,
                            data = new { name, age, weight },
                            message = $"Validarea câinelui a eșuat cu {validationErrors.Count} erori"
                        });
                    }

                    logger.LogInfo($"Test Add Dog SUCCESS: {name} - {age} ani - {weight} kg");
                    return Results.Json(new 
                    { 
                        success = true, 
                        type = "dog",
                        entity = "Câine",
                        data = new { name, age, weight },
                        message = $"✅ Câinele '{name}' a fost validat cu succes!"
                    });
                }
                else if (type == "adopter")
                {
                    string? name = body.GetProperty("name").GetString();
                    string? email = body.GetProperty("email").GetString();
                    string? phone = body.GetProperty("phone").GetString();

                    var validationErrors = new List<string>();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        validationErrors.Add("❌ Numele adoptatorului nu poate fi gol sau doar spații");
                    }

                    if (string.IsNullOrWhiteSpace(email))
                    {
                        validationErrors.Add("❌ Email-ul nu poate fi gol");
                    }
                    else if (!email.Contains("@"))
                    {
                        validationErrors.Add($"❌ Email-ul '{email}' nu este valid (lipsește @)");
                    }

                    if (string.IsNullOrWhiteSpace(phone))
                    {
                        validationErrors.Add("❌ Telefonul nu poate fi gol");
                    }
                    else
                    {
                        string cleanPhone = phone.Replace(" ", "").Replace("-", "");
                        
                        if (cleanPhone.Length != 10)
                        {
                            validationErrors.Add($"❌ Telefonul '{phone}' trebuie să aibă exact 10 cifre (format: 07XXXXXXXX)");
                        }
                        else if (!cleanPhone.StartsWith("07"))
                        {
                            validationErrors.Add($"❌ Telefonul '{phone}' trebuie să înceapă cu 07");
                        }
                        else if (!cleanPhone.All(char.IsDigit))
                        {
                            validationErrors.Add($"❌ Telefonul '{phone}' trebuie să conțină doar cifre");
                        }
                    }

                    if (validationErrors.Count > 0)
                    {
                        logger.LogError($"Test Add Adopter FAILED: {string.Join(", ", validationErrors)}");
                        return Results.Json(new 
                        { 
                            success = false, 
                            type = "adopter",
                            entity = "Adoptator",
                            errors = validationErrors,
                            data = new { name, email, phone },
                            message = $"Validarea adoptatorului a eșuat cu {validationErrors.Count} erori"
                        });
                    }

                    logger.LogInfo($"Test Add Adopter SUCCESS: {name} - {email} - {phone}");
                    return Results.Json(new 
                    { 
                        success = true, 
                        type = "adopter",
                        entity = "Adoptator",
                        data = new { name, email, phone },
                        message = $"✅ Adoptatorul '{name}' a fost validat cu succes!"
                    });
                }
                else
                {
                    return Results.Json(new 
                    { 
                        success = false, 
                        error = $"Tip invalid: '{type}'. Tipuri permise: 'dog' sau 'adopter'" 
                    });
                }
            }
            catch (Exception ex)
            {
                return Results.Json(new { success = false, error = ex.Message });
            }
        });
    }
}

