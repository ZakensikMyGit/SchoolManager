# 🏫 System Zarządzania Przedszkolem

System zarządzania danymi dla publicznego przedszkola, zbudowany w technologii C# (.NET) z wykorzystaniem podejścia **Entity Framework Code First**. Projekt umożliwia zarządzanie pracownikami, nauczycielami, grupami oraz dziećmi.

---

## 📦 Zawartość projektu

### Kluczowe klasy i funkcjonalności:

| Klasa        | Opis |
|--------------|------|
| `Position`   | Stanowiska pracowników (nauczyciele i inni) z podziałem na kategorie |
| `Employee`   | Pracownicy przedszkola (imię, nazwisko, etat, zatrudnienie, stanowisko) |
| `Teacher`    | Nauczyciele, dziedziczący po `Employee`, przypisani do jednej grupy |
| `Group`      | Grupy dzieci w przedszkolu z przypisanym wychowawcą |
| `Child`      | Dzieci zapisane do przedszkola z danymi osobowymi i rodzicami |
| `Declaration`| Orzeczenia, opinie, diagnozy i dokumenty poradni psychologiczno-pedagogicznej |
| `Education`  | Wykształcenie i szkolenia pracowników |

---

## 📘 Szczegóły klas i ich właściwości

### 🧑‍🏫 Teacher

| Właściwość        | Typ         | Opis |
|-------------------|-------------|------|
| `TypeTeacher`     | string      | Typ nauczyciela (np. wychowawca, pomoc nauczyciela) |
| `IsDirector`      | bool        | Czy nauczyciel pełni funkcję dyrektora |
| `PensumHours`     | int         | Liczba godzin dydaktycznych |
| `Group`           | Group       | Grupa, za którą nauczyciel odpowiada jako wychowawca |

### 👨‍💼 Employee

| Właściwość        | Typ         | Opis |
|-------------------|-------------|------|
| `Name`            | string      | Imię pracownika |
| `LastName`        | string      | Nazwisko pracownika |
| `WorkingHours`    | double      | Etat (np. 1.0 = pełny etat) |
| `EmploymentDate`  | DateTime    | Data zatrudnienia |
| `IsActive`        | bool        | Czy pracownik jest aktualnie zatrudniony |
| `Position`        | Position    | Stanowisko pracownika |
| `Education`       | ICollection<Education> | Lista ukończonych szkoleń i studiów |

### 👶 Child

| Właściwość           | Typ         | Opis |
|----------------------|-------------|------|
| `Name`               | string      | Imię dziecka |
| `LastName`           | string      | Nazwisko dziecka |
| `BirthDate`          | DateTime    | Data urodzenia |
| `Address`            | string      | Adres zamieszkania |
| `BirthPlace`         | string      | Miejsce urodzenia |
| `Nationality`        | string      | Narodowość dziecka |
| `PESEL`              | string      | Numer PESEL dziecka |
| `ParentNames`        | string      | Imiona rodziców |
| `ParentPhoneNumber`  | string      | Telefon kontaktowy |
| `Group`              | Group       | Grupa, do której przypisane jest dziecko |
| `Declarations`       | ICollection<Declaration> | Lista orzeczeń i opinii |

### 📄 Declaration

| Właściwość          | Typ         | Opis |
|---------------------|-------------|------|
| `Type`              | string      | Typ dokumentu (np. orzeczenie, opinia z poradni) |
| `IssueDate`         | DateTime?   | Data wystawienia dokumentu |
| `ValidUntil`        | DateTime?   | Data ważności dokumentu |
| `IssuingAuthority`  | string      | Nazwa organu, który wydał dokument |
| `Diagnosis`         | string      | Rozpoznanie, np. autyzm, asperger |
| `IsVoluntaryRequest`| bool        | Czy dokument został wydany na wniosek rodziców |
| `Description`       | string      | Dodatkowe informacje |

### 🎓 Education

| Właściwość   | Typ    | Opis |
|--------------|--------|------|
| `Name`       | string | Nazwa wykształcenia lub kursu |
| `Description`| string | Opis szczegółowy |
| `Type`       | string | Typ: "Studia", "Szkolenie" |

---

## 🛠️ Technologie

- C# / .NET 8
- Entity Framework Core
- SQL Server
- Visual Studio 2022 

---

## 📬 Kontakt

Autor: **AZ**  
E-mail: `zakensik@gmail.com`