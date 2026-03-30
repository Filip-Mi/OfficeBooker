# Lista Błędów do Naprawy 🔴

## 1. BŁĘDY KRITYCZNE (KOMPILACJA)

### ❌ Błąd 1: Brakujące Nawiasy w OfficeController.cs
**Plik:** `OfficeBooker/Controllers/OfficeController.cs`  
**Linia:** 25  
**Problem:** 
```csharp
var officesDTO = offices.Select(o => new OfficeDTO { ... }).ToList;  // ❌ Brakuje ()
```
**Rozwiązanie:**
```csharp
var officesDTO = offices.Select(o => new OfficeDTO { ... }).ToList();  // ✅ Dodaj ()
```
**Priorytet:** 🔴 KRYTYCZNY - Błąd kompilacji  
**Wersja:** .NET 10

---

### ❌ Błąd 2: Brakujący await w OfficeController.cs
**Plik:** `OfficeBooker/Controllers/OfficeController.cs`  
**Linia:** 41  
**Problem:**
```csharp
_unitOfWork.Save();  // ❌ Powinno być async
```
**Rozwiązanie:**
```csharp
await _unitOfWork.Save();  // ✅ Dodaj await
```
**Priorytet:** 🔴 KRYTYCZNY - Błąd compilacji (async method)  
**Uwaga:** Metoda `CreateOffice` jest async, więc musi używać `await`

---

## 2. BŁĘDY LOGIKI

### ❌ Błąd 3: Błędny Return Type w ReservationService.cs
**Plik:** `OfficeBooker.Services/ReservationService.cs`  
**Linia:** 15, 22  
**Problem:**
```csharp
public async Task<IEnumerable<ReservationCreateDTO>> GetMyReservationsAsync(string userId)
{
    var reservations = await _unitOfWork.reservationRepository.GetAll(...);
    
    return (IEnumerable<ReservationCreateDTO>)reservations.Select(r => new ReservationRespondDTO
    {
        // ...
    });
}
```

**Rozwiązanie:**
```csharp
public async Task<IEnumerable<ReservationRespondDTO>> GetMyReservationsAsync(string userId)
{
    var reservations = await _unitOfWork.reservationRepository.GetAll(...);
    
    return reservations.Select(r => new ReservationRespondDTO
    {
        // ...
    }).ToList();  // Dodaj ToList() dla safety
}
```

**Priorytet:** 🔴 KRYTYCZNY - Typ zwracanego obiektu się nie zgadza  
**Szczegóły:**
- Interface `IReservationService.cs` mówi, że metoda zwraca `IEnumerable<ReservationCreateDTO>`
- Metoda zwraca `IEnumerable<ReservationRespondDTO>`
- Należy zmienić interface i implementację na `ReservationRespondDTO`

---

### ❌ Błąd 4: Typo w AuthService.cs
**Plik:** `OfficeBooker.Services/AuthService.cs`  
**Linia:** 82  
**Problem:**
```csharp
return (true, "Registration Successfu");  // ❌ Typo: "Successfu"
```
**Rozwiązanie:**
```csharp
return (true, "Registration Successful");  // ✅ Poprawna pisownia
```
**Priorytet:** 🟡 ŚREDNI - Błąd tekstowy  
**Wpływ:** Zwracana wiadomość z błędem do klienta

---

## 3. BŁĘDY BEZPIECZEŃSTWA

### ⚠️ Błąd 5: Generic Exception Handling (ReservationService.cs)
**Plik:** `OfficeBooker.Services/ReservationService.cs`  
**Linie:** 40, 51  
**Problem:**
```csharp
if (office == null)
{
    throw new Exception();  // ❌ Brak opisu błędu
}

if (overlappingReservation != null)
{
    throw new Exception();  // ❌ Brak opisu błędu
}
```
**Rozwiązanie:**
```csharp
if (office == null)
{
    throw new ArgumentException("Office not found.");  // ✅ Konkretny typ
}

if (overlappingReservation != null)
{
    throw new InvalidOperationException("Office is already reserved for this time period.");  // ✅ Konkretny typ
}
```
**Priorytet:** 🟡 ŚREDNI - Security best practice  
**Wpływ:** Czytelność błędów, logging

---

### ⚠️ Błąd 6: Placeholder JWT Key
**Plik:** `OfficeBooker/appsettings.json`  
**Problem:**
```json
"Jwt": {
  "Key": "YOUR_SECRET_KEY_MIN_32_CHARS"  // ❌ Placeholder
}
```
**Rozwiązanie:**
```json
"Jwt": {
  "Key": "MySecureSecretKeyForJWT12345678"  // Minimum 32 znaki
}
```
**Priorytet:** 🔴 KRYTYCZNY - Security Issue  
**Uwaga:** W produkcji użyć Secret Manager lub Azure Key Vault!

---

## 4. BŁĘDY ARCHITEKTURALNE

### ❌ Błąd 7: Redundantna Klasa OfficeService
**Plik:** `OfficeBooker.Services/OfficeService.cs`  
**Problem:**
- `OfficeService` istnieje, ale nie jest używana
- Logika tworzenia biura jest w `OfficeController.CreateOffice()`
- Łamie zasadę DRY (Don't Repeat Yourself)

**Rozwiązanie:**
- Używać `OfficeService` w kontrolerze zamiast bezpośrednio `IUnitOfWork`
- Lub usunąć `OfficeService` i użyć `IUnitOfWork` bezpośrednio

**Priorytet:** 🟡 ŚREDNI - Code organization  

---

### ⚠️ Błąd 8: Niespójne Namespace'y
**Problem:**
```
OfficeBooker.Models          ← Office.cs
OfficeBooker.Models.cs       ← Worker.cs, Reservation.cs
OfficeBooker.Models.DTOs     ← OfficeDTO.cs
OfficeBooker.Models.cs.DTOs  ← CreateWorkerDTO.cs, itd.
```

**Rozwiązanie:**
Ujednolicić na:
```
OfficeBooker.Models              ← Wszystkie modele
OfficeBooker.Models.DTOs         ← Wszystkie DTOs
```

**Priorytet:** 🟡 ŚREDNI - Code organization

---

### ⚠️ Błąd 9: Brak Walidacji Danych
**Problem:**
- Nie sprawdza się czy `ReservationEndTime > ReservationStartTime`
- Możliwa rezerwacja z czasem końcowym wcześniejszym niż początkowy

**Rozwiązanie:**
```csharp
if (dto.ReservationEndTime <= dto.ReservationStartTime)
{
    throw new ArgumentException("End time must be after start time.");
}
```

**Priorytet:** 🟡 ŚREDNI - Data validation

---

### ⚠️ Błąd 10: Brak CORS Configuration
**Plik:** `OfficeBooker/Program.cs`  
**Problem:**
- CORS nie jest skonfigurowany
- Frontend z innej domeny nie będzie mógł połączyć się z API

**Rozwiązanie:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// W app.UseCors()
app.UseCors("AllowAll");
```

**Priorytet:** 🟡 ŚREDNI - Required for frontend integration

---

## 📊 PODSUMOWANIE

| Priorytet | Liczba | Typ |
|-----------|--------|------|
| 🔴 KRYTYCZNY | 3 | Błędy kompilacji + Security |
| 🟡 ŚREDNI | 7 | Code quality + Architecture |
| **RAZEM** | **10** | |

---

## ✅ DZIAŁANIA

### Fase 1: Napraw BŁĘDY KRITYCZNE (5 min)
1. Dodaj `()` do `.ToList` w OfficeController.cs:25
2. Dodaj `await` w OfficeController.cs:41
3. Zmień JWT Key w appsettings.json

### Fase 2: Napraw BŁĘDY LOGIKI (10 min)
4. Zmień return type w ReservationService.cs:15
5. Popraw typo "Successfu" → "Successful" w AuthService.cs:82
6. Dodaj konkretne exception types

### Fase 3: REFACTORING (30 min)
7. Ujednolicić namespace'y
8. Usunąć redundantny kod
9. Dodać CORS
10. Dodać walidacje danych

---

## 🔍 TESTOWANIE

Po naprawie błędów:
```bash
# Zbuduj projekt
dotnet build

# Uruchom testy (jeśli istnieją)
dotnet test

# Sprawdź na localhost
https://localhost:7040/api/auth/login
```

---

**Ostatnia aktualizacja:** Styczeń 2025
