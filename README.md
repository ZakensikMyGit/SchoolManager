# SchoolManager
# System Zarządzania Przedszkolem

System zarządzania placówką oświaty (przedszkole), 
zbudowany w technologii C# (.NET) z wykorzystaniem podejścia **Entity Framework Code First**.
Projekt umożliwia zarządzanie:
  pracownikami, nauczycielami, grupami, dziećmi.

---

## Zawartość projektu

### Kluczowe klasy i funkcjonalności:

| Klasa        | Opis |
|--------------|------|
| `Employee`   | Pracownicy przedszkola (imię, nazwisko, etat, zatrudnienie, stanowisko) |
| `Teacher`    | Nauczyciele, dziedziczący po `Employee`, przypisani do jednej grupy |
| `Position`   | Stanowiska pracowników (nauczyciele i inni) z podziałem na kategorie |
| `Group`      | Grupy dzieci w przedszkolu z przypisanym wychowawcą |
| `Child`      | Dzieci zapisane do przedszkola z danymi osobowymi i rodzicami |
| `Declaration`| Orzeczenia, opinie, diagnozy i dokumenty poradni psychologiczno-pedagogicznej |
| `Education`  | Wykształcenie i szkolenia pracowników |

---

## Technologie

- C#
- Entity Framework Core (Code First)
- SQL ????
- Visual Studio 2022 

---

## Struktura bazy danych (model encji)

- Relacja `Employee -> Position` – wiele do jednego
- Relacja `Employee <-> Education` – wiele do wielu
- Relacja `Teacher -> Group` – jeden nauczyciel = jedna grupa
- Relacja `Group -> Child` – jedna grupa zawiera wiele dzieci
- Relacja `Child -> Declaration` – dziecko może mieć wiele dokumentów (orzeczeń, opinii)

---

## 🔧 Jak uruchomić projekt

1. Sklonuj repozytorium:
   ```bash
   git clone https://github.com/twoje-repozytorium/przedszkole.git
