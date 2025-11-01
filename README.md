# Projekt-Siłownia
## 📘 Opis projektu

**Projekt-Siłownia** to aplikacja napisana w języku **C# (.NET)**, mająca na celu symulację systemu obsługi siłowni.
Projekt został stworzony w ramach nauki programowania obiektowego, architektury aplikacji desktopowych oraz pracy zespołowej w środowisku GitHub.

Aplikacja umożliwia m.in.:
- zarządzanie klientami, trenerami,
- tworzenie oraz edycję planów treningowych,
- planowanie zajęć,
- rejestrowanie użytkowników,

---

## 🧩 Funkcjonalności

| Kategoria | Opis |
|------------|------|
| 👤 Użytkownicy | Logowanie, rejestracja, role (klient, trener, admin) |
| 🏋️‍♀️ Treningi | Tworzenie i edycja planów treningowych |
| 🧾 Raporty | Generowanie raportów z aktywności |
| 🏢 Zarządzanie | Dodawanie ćwiczeń |
| 💾 Dane | Zapisywanie informacji w bazie danych MySQL |
| ⚙️ GUI | Intuicyjny interfejs desktopowy stworzony w C# (.net MAUI) |

---


## ⚙️ Instalacja i uruchomienie

1. **Sklonuj repozytorium:**
   git clone https://github.com/MrKondziX/Gym-School-Project-coauthored.git

2. **Otwórz projekt w Visual Studio**
   Uruchom plik `Projekt-Siłownia.sln`.

3. **Zainstaluj wymagane zależności** (jeśli projekt ich wymaga).
   Sprawdź zakładkę *Dependencies* w Visual Studio.

4. **Zbuduj projekt**
   Wybierz `Build → Build Solution` lub naciśnij **Ctrl + Shift + B**.

5. **Uruchom aplikację**
   Kliknij **F5** lub `Debug → Start Debugging`.

---

## 🧠 Wymagania systemowe

- **System operacyjny:** Windows 10 lub nowszy
- **.NET Framework / .NET Core:** .NET 8.0
- **IDE:** Visual Studio 2022
- **Baza danych:** SQL Server / SQLite

---

## 🚀 Użycie aplikacji

Po uruchomieniu aplikacji możesz:
1. Utworzyć konto użytkownika (lub zalogować się, jeśli konto już istnieje).
2. Zarządzać listą klientów i trenerów.
3. Dodawać lub usuwać ćwiczenia.
4. Tworzyć plany treningowe dla klientów.
5. Przeglądać raporty i statystyki.


---

## 🧑‍💻 Współpraca

Chcesz pomóc w rozwoju projektu? Oto jak możesz to zrobić:

1. **Fork** repozytorium
2. Utwórz nowy branch:
   git checkout -b feature/nazwa-funkcji
3. Wprowadź zmiany i przetestuj je
4. Wygeneruj **Pull Request** z opisem zmian

---

## 🧱 Technologie

- **Język:** C#
- **Framework:** .NET
- **Środowisko:** Visual Studio
- **GUI:** .NET MAUI
- **Kontrola wersji:** Git + GitHub
- **Baza danych:** MySQL

---

# Schemat bazy danych

## Zawartość
- `carnets`
- `exercises`
- `exercises_muscles`
- `users`
- `users_coach`
- `users_klient`
- `users_klient_carnets`
- `users_klient_treningday`
- `users_klient_treningplans`
- `users_klient_trenings`
- `users_type`

---

## Tabela: `carnets`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `carnet_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `carnet_name` | `text` | NO | |
| `carnet_cost` | `double` | NO | |

---

## Tabela: `exercises`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `exs_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `exs_muscle_id` | `int` | NO | INDEX — `exercises_muscles.exs_muscle_id` |
| `exs_name` | `text` | NO | |
| `exs_description` | `text` | NO | |

---

## Tabela: `exercises_muscles`
| Kolumna | Typ | NULL? | Dodatki |
|---|---:|:---:|---|
| `exs_muscle_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `exs_muscle_name` | `text` | NO | |

---

## Tabela: `users`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `users_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_name` | `text` | NO | |
| `users_surname` | `text` | NO | |
| `users_email` | `text` | NO | |
| `users_login` | `text` | NO | |
| `users_password` | `varchar(255)` | NO | |
| `users_type_id` | `int` | NO | INDEX — `users_type.users_type_id` |

---

## Tabela: `users_coach`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `users_coach_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_id` | `int` | NO | INDEX — `users.users_id` |
| `users_coach_nott` | `text` | NO | |

---

## Tabela: `users_klient`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `users_klient_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_id` | `int` | NO | INDEX — `users.users_id` |
| `users_coach_id` | `int` | NO | INDEX — `users_coach.users_coach_id` |

---

## Tabela: `users_klient_carnets`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `users_klient_carnet_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_klient_id` | `int` | NO | INDEX — `users_klient.users_klient_id` |
| `carnet_id` | `int` | NO | INDEX — `carnets.carnet_id` |
| `carnet_startdate` | `date` | NO | |
| `carnet_enddate` | `date` | NO | |

---

## Tabela: `users_klient_treningday`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `users_treningday_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_klient_id` | `int` | NO | INDEX — `users_klient.users_klient_id` |
| `users_treningday_date` | `date` | NO | |
| `users_treningday_time` | `varchar(32)` | NO | |

---

## Tabela: `users_klient_treningplans`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `treningplan_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_klient_id` | `int` | NO | INDEX — `users_klient.users_klient_id` |
| `exs_id` | `int` | NO | INDEX — `exercises.exs_id` |
| `treningplan_note` | `text` | NO | |
| `treningplan_date` | `date` | NO | |
| `TreningDayWeek` | `int` | NO | |

---

## Tabela: `users_klient_trenings`
| Kolumna | Typ | NULL? | Dodatki / Indeksy |
|---|---:|:---:|---|
| `trening_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_klient_id` | `int` | NO | INDEX — `users_klient.users_klient_id` |
| `users_klient_trening_date` | `date` | NO | |
| `trening_weight` | `float` | NO | |
| `trening_series` | `int` | NO | |
| `exs_id` | `int` | NO | INDEX — `exercises.exs_id` |
| `users_treningday_id` | `int` | NO | INDEX — `users_klient_treningday.users_treningday_id` |
| `powtorzenia` | `float` | YES | |
| `rpe` | `float` | YES | |

---

## Tabela: `users_type`
| Kolumna | Typ | NULL? | Dodatki |
|---|---:|:---:|---|
| `users_type_id` | `int` | NO | `AUTO_INCREMENT`, PK |
| `users_type_name` | `text` | NO | |

---

## Relacje (logiczne)

```mermaid
erDiagram
    USERS ||--o{ USERS_COACH : has
    USERS ||--o{ USERS_KLIENT : has
    USERS_TYPE ||--o{ USERS : has_type
    USERS_COACH ||--o{ USERS_KLIENT : coach_of
    USERS_KLIENT ||--o{ USERS_KLIENT_CARNETS : has
    CARNETS ||--o{ USERS_KLIENT_CARNETS : assigned
    USERS_KLIENT ||--o{ USERS_KLIENT_TRENINGDAY : has
    USERS_KLIENT_TRENINGDAY ||--o{ USERS_KLIENT_TRENINGS : day_has
    USERS_KLIENT ||--o{ USERS_KLIENT_TRENINGS : did
    EXERCISES ||--o{ USERS_KLIENT_TRENINGS : performed
    EXERCISES_MUSCLES ||--o{ EXERCISES : group
    USERS_KLIENT ||--o{ USERS_KLIENT_TRENINGPLANS : plan_for
    EXERCISES ||--o{ USERS_KLIENT_TRENINGPLANS : planned
