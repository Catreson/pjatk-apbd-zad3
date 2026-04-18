using LinqConsoleLab.PL.Data;

namespace LinqConsoleLab.PL.Exercises;

public sealed class ZadaniaLinq
{
    /// <summary>
    /// Zadanie:
    /// Wyszukaj wszystkich studentów mieszkających w Warsaw.
    /// Zwróć numer indeksu, pełne imię i nazwisko oraz miasto.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko, Miasto
    /// FROM Studenci
    /// WHERE Miasto = 'Warsaw';
    /// </summary>
    public IEnumerable<string> Zadanie01_StudenciZWarszawy()
    {
        var query = from s in DaneUczelni.Studenci
            where s.Miasto.Equals("Warsaw")
            select $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}, {s.Miasto}";

        var method = DaneUczelni.Studenci
            .Where(s => s.Miasto.Equals("Warsaw"))
            .Select(s => $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}, {s.Miasto}");
        
        return method;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj listę adresów e-mail wszystkich studentów.
    /// Użyj projekcji, tak aby w wyniku nie zwracać całych obiektów.
    ///
    /// SQL:
    /// SELECT Email
    /// FROM Studenci;
    /// </summary>
    public IEnumerable<string> Zadanie02_AdresyEmailStudentow()
    {
        var emails = DaneUczelni.Studenci
            .Select(s => $"{s.Email}");
        return emails;
    }

    /// <summary>
    /// Zadanie:
    /// Posortuj studentów alfabetycznie po nazwisku, a następnie po imieniu.
    /// Zwróć numer indeksu i pełne imię i nazwisko.
    ///
    /// SQL:
    /// SELECT NumerIndeksu, Imie, Nazwisko
    /// FROM Studenci
    /// ORDER BY Nazwisko, Imie;
    /// </summary>
    public IEnumerable<string> Zadanie03_StudenciPosortowani()
    {
        var studentsSorted = DaneUczelni.Studenci
            .OrderBy(s => s.Nazwisko)
            .OrderBy(s => s.Imie)
            .Select(s => $"{s.NumerIndeksu}, {s.Imie}, {s.Nazwisko}");
        return studentsSorted;
    }

    /// <summary>
    /// Zadanie:
    /// Znajdź pierwszy przedmiot z kategorii Analytics.
    /// Jeżeli taki przedmiot nie istnieje, zwróć komunikat tekstowy.
    ///
    /// SQL:
    /// SELECT TOP 1 Nazwa, DataStartu
    /// FROM Przedmioty
    /// WHERE Kategoria = 'Analytics';
    /// </summary>
    public IEnumerable<string> Zadanie04_PierwszyPrzedmiotAnalityczny()
    {
        var firstAnalSubject = DaneUczelni.Przedmioty
            .FirstOrDefault(s => s.Kategoria.Equals("Analytics"));
        return firstAnalSubject is null ? [$"{firstAnalSubject?.Nazwa}, {firstAnalSubject?.DataStartu}"] : ["Nie znaleziono przedmiotu analitycznego."];
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy w danych istnieje przynajmniej jeden nieaktywny zapis.
    /// Zwróć jedno zdanie z odpowiedzią True/False albo Tak/Nie.
    ///
    /// SQL:
    /// SELECT CASE WHEN EXISTS (
    ///     SELECT 1
    ///     FROM Zapisy
    ///     WHERE CzyAktywny = 0
    /// ) THEN 1 ELSE 0 END;
    /// </summary>
    public IEnumerable<string> Zadanie05_CzyIstniejeNieaktywneZapisanie()
    {
        var isPreset = DaneUczelni.Zapisy
            .Select(z => z.CzyAktywny)
            .Contains(false);
        return isPreset ? ["TAK"] : ["NIE"];
    }

    /// <summary>
    /// Zadanie:
    /// Sprawdź, czy każdy prowadzący ma uzupełnioną nazwę katedry.
    /// Warto użyć metody, która weryfikuje warunek dla całej kolekcji.
    ///
    /// SQL:
    /// SELECT CASE WHEN COUNT(*) = COUNT(Katedra)
    /// THEN 1 ELSE 0 END
    /// FROM Prowadzacy;
    /// </summary>
    public IEnumerable<string> Zadanie06_CzyWszyscyProwadzacyMajaKatedre()
    {
        var allHave = DaneUczelni.Prowadzacy
            .All(p => p.Katedra is not null);
        return allHave ? ["TAK"] : ["NIE"];
    }

    /// <summary>
    /// Zadanie:
    /// Policz, ile aktywnych zapisów znajduje się w systemie.
    ///
    /// SQL:
    /// SELECT COUNT(*)
    /// FROM Zapisy
    /// WHERE CzyAktywny = 1;
    /// </summary>
    public IEnumerable<string> Zadanie07_LiczbaAktywnychZapisow()
    {
        var countActive = DaneUczelni.Zapisy
            .Count(z => z.CzyAktywny);
        return [$"{countActive}"];
    }

    /// <summary>
    /// Zadanie:
    /// Pobierz listę unikalnych miast studentów i posortuj ją rosnąco.
    ///
    /// SQL:
    /// SELECT DISTINCT Miasto
    /// FROM Studenci
    /// ORDER BY Miasto;
    /// </summary>
    public IEnumerable<string> Zadanie08_UnikalneMiastaStudentow()
    {
        var cities = DaneUczelni.Studenci
            .Select(s => s.Miasto)
            .Distinct()
            .Order();
        return cities;
    }

    /// <summary>
    /// Zadanie:
    /// Zwróć trzy najnowsze zapisy na przedmioty.
    /// W wyniku pokaż datę zapisu, identyfikator studenta i identyfikator przedmiotu.
    ///
    /// SQL:
    /// SELECT TOP 3 DataZapisu, StudentId, PrzedmiotId
    /// FROM Zapisy
    /// ORDER BY DataZapisu DESC;
    /// </summary>
    public IEnumerable<string> Zadanie09_TrzyNajnowszeZapisy()
    {
        var topThree = DaneUczelni.Zapisy
            .OrderByDescending(z => z.DataZapisu)
            .Take(3)
            .Select(z => $"{z.DataZapisu}, {z.StudentId}, {z.PrzedmiotId}");
        return topThree;
    }

    /// <summary>
    /// Zadanie:
    /// Zaimplementuj prostą paginację dla listy przedmiotów.
    /// Załóż stronę o rozmiarze 2 i zwróć drugą stronę danych.
    ///
    /// SQL:
    /// SELECT Nazwa, Kategoria
    /// FROM Przedmioty
    /// ORDER BY Nazwa
    /// OFFSET 2 ROWS FETCH NEXT 2 ROWS ONLY;
    /// </summary>
    public IEnumerable<string> Zadanie10_DrugaStronaPrzedmiotow()
    {
        var pages = DaneUczelni.Przedmioty
            .Skip(2)
            .Take(2)
            .Select(p => $"{p.Nazwa}, {p.Kategoria}");
        return pages;
    }

    /// <summary>
    /// Zadanie:
    /// Połącz studentów z zapisami po StudentId.
    /// Zwróć pełne imię i nazwisko studenta oraz datę zapisu.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, z.DataZapisu
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId;
    /// </summary>
    public IEnumerable<string> Zadanie11_PolaczStudentowIZapisy()
    {
        var joined = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy,
                    s => s.Id,
                    z => z.StudentId,
                    (s, z) => new
                    {
                        s.Imie,
                        s.Nazwisko,
                        z.DataZapisu
                    })
            .Select(s => $"{s.Imie}, {s.Nazwisko}, {s.DataZapisu}");
        return joined;
    }

    /// <summary>
    /// Zadanie:
    /// Przygotuj wszystkie pary student-przedmiot na podstawie zapisów.
    /// Użyj podejścia, które pozwoli spłaszczyć dane do jednej sekwencji wyników.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, p.Nazwa
    /// FROM Zapisy z
    /// JOIN Studenci s ON s.Id = z.StudentId
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId;
    /// </summary>
    public IEnumerable<string> Zadanie12_ParyStudentPrzedmiot()
    {
        var studentPairs = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, z) => new
                {
                    s.Imie,
                    s.Nazwisko,
                    z.PrzedmiotId
                })
            .Join(DaneUczelni.Przedmioty,
                s => s.PrzedmiotId,
                p => p.Id,
                (s, p) => new
                {
                    s.Imie,
                    s.Nazwisko,
                    p.Nazwa
                })
            .Select(s => $"{s.Imie}, {s.Nazwisko}, {s.Nazwa}");
        return studentPairs;
    }

    /// <summary>
    /// Zadanie:
    /// Pogrupuj zapisy według przedmiotu i zwróć nazwę przedmiotu oraz liczbę zapisów.
    ///
    /// SQL:
    /// SELECT p.Nazwa, COUNT(*)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie13_GrupowanieZapisowWedlugPrzedmiotu()
    {
        var grupy = DaneUczelni.Przedmioty
            .GroupJoin(DaneUczelni.Zapisy,
                z => z.Id,
                z => z.PrzedmiotId,
                (p, gr) => new
                {
                    p.Nazwa
                })
            .GroupBy(p => p.Nazwa)
            .Select(g => $"{g.Key}, {g.Count()}");
        return grupy;
    }

    /// <summary>
    /// Zadanie:
    /// Oblicz średnią ocenę końcową dla każdego przedmiotu.
    /// Pomiń rekordy, w których ocena końcowa ma wartość null.
    ///
    /// SQL:
    /// SELECT p.Nazwa, AVG(z.OcenaKoncowa)
    /// FROM Zapisy z
    /// JOIN Przedmioty p ON p.Id = z.PrzedmiotId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY p.Nazwa;
    /// </summary>
    public IEnumerable<string> Zadanie14_SredniaOcenaNaPrzedmiot()
    {
        var srednie = DaneUczelni.Przedmioty
            .Join(DaneUczelni.Zapisy,
                p => p.Id,
                z => z.PrzedmiotId,
                (p, z) => new
                {
                    p.Nazwa,
                    z.OcenaKoncowa
                })
            .Where(p => p.OcenaKoncowa is not null)
            .GroupBy(p => p.Nazwa)
            .Select(g => $"{g.Key}, {g.Average(z => z.OcenaKoncowa)}");

        return srednie;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego prowadzącego policz liczbę przypisanych przedmiotów.
    /// W wyniku zwróć pełne imię i nazwisko oraz liczbę przedmiotów.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, COUNT(p.Id)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie15_ProwadzacyILiczbaPrzedmiotow()
    {
        var liczbyPrzedmiotow = DaneUczelni.Prowadzacy
            .Join(DaneUczelni.Przedmioty,
                p => p.Id,
                p => p.ProwadzacyId,
                (pro, prz) => new
                {
                    pro.Id,
                    pro.Imie,
                    pro.Nazwisko
                })
            .GroupBy(p => p.Id)
            .Select(g => $"{g.Select(g => g.Imie)}, {g.Select(g => g.Imie)}, {g.Count()}");
        return liczbyPrzedmiotow;
    }

    /// <summary>
    /// Zadanie:
    /// Dla każdego studenta znajdź jego najwyższą ocenę końcową.
    /// Pomiń studentów, którzy nie mają jeszcze żadnej oceny.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, MAX(z.OcenaKoncowa)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY s.Imie, s.Nazwisko;
    /// </summary>
    public IEnumerable<string> Zadanie16_NajwyzszaOcenaKazdegoStudenta()
    {
        var maxOcena = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy,
                s => s.Id,
                z => z.StudentId,
                (s, p) => new
                {
                    s.Id,
                    s.Imie,
                    s.Nazwisko,
                    p.OcenaKoncowa,
                    p.PrzedmiotId
                })
            .Where(p => p.OcenaKoncowa is not null)
            .GroupBy(s => s.Id)
            .Select(s => $"{s.Select(g => g.Imie)}, {s.Select(g => g.Nazwisko)}, {s.Max(g => g.OcenaKoncowa)}");
        return maxOcena; 
    }

    /// <summary>
    /// Wyzwanie:
    /// Znajdź studentów, którzy mają więcej niż jeden aktywny zapis.
    /// Zwróć pełne imię i nazwisko oraz liczbę aktywnych przedmiotów.
    ///
    /// SQL:
    /// SELECT s.Imie, s.Nazwisko, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Imie, s.Nazwisko
    /// HAVING COUNT(*) > 1;
    /// </summary>
    public IEnumerable<string> Wyzwanie01_StudenciZWiecejNizJednymAktywnymPrzedmiotem()
    {
        var osobyAktywne = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy.Where(z => z.CzyAktywny),
                s => s.Id,
                z => z.StudentId,
                (s, z) => new
                {
                    s.Imie,
                    s.Nazwisko
                })
            .GroupBy(x => new
            {
                x.Imie,
                x.Nazwisko
            })
            .Where(g => g.Count() > 1)
            .Select(g => $"Imię {g.Key.Imie}, nazwisko {g.Key.Nazwisko}, liczba aktywnych przedmiotów {g.Count()}");

        return osobyAktywne;
    }

    /// <summary>
    /// Wyzwanie:
    /// Wypisz przedmioty startujące w kwietniu 2026, dla których żaden zapis nie ma jeszcze oceny końcowej.
    ///
    /// SQL:
    /// SELECT p.Nazwa
    /// FROM Przedmioty p
    /// JOIN Zapisy z ON p.Id = z.PrzedmiotId
    /// WHERE MONTH(p.DataStartu) = 4 AND YEAR(p.DataStartu) = 2026
    /// GROUP BY p.Nazwa
    /// HAVING SUM(CASE WHEN z.OcenaKoncowa IS NOT NULL THEN 1 ELSE 0 END) = 0;
    /// </summary>
    public IEnumerable<string> Wyzwanie02_PrzedmiotyStartujaceWKwietniuBezOcenKoncowych()
    {
        var przedmiotyKwietniowe = DaneUczelni.Przedmioty
            .Where(p => p.DataStartu.Month == 4 && p.DataStartu.Year == 2026)
            .Join(DaneUczelni.Zapisy,
                p => p.Id,
                z => z.PrzedmiotId,
                (p, z) => new
                {
                    p.Nazwa,
                    z.OcenaKoncowa
                })
            .GroupBy(x => x.Nazwa)
            .Where(g => g.Count(x => x.OcenaKoncowa != null) == 0)
            .Select(g => g.Key);

        return przedmiotyKwietniowe;
    }

    /// <summary>
    /// Wyzwanie:
    /// Oblicz średnią ocen końcowych dla każdego prowadzącego na podstawie wszystkich jego przedmiotów.
    /// Pomiń brakujące oceny, ale pozostaw samych prowadzących w wyniku.
    ///
    /// SQL:
    /// SELECT pr.Imie, pr.Nazwisko, AVG(z.OcenaKoncowa)
    /// FROM Prowadzacy pr
    /// LEFT JOIN Przedmioty p ON p.ProwadzacyId = pr.Id
    /// LEFT JOIN Zapisy z ON z.PrzedmiotId = p.Id
    /// WHERE z.OcenaKoncowa IS NOT NULL
    /// GROUP BY pr.Imie, pr.Nazwisko;
    /// </summary>
    public IEnumerable<string> Wyzwanie03_ProwadzacyISredniaOcenNaIchPrzedmiotach()
    {
        var sredniaOcenProwadzacych = DaneUczelni.Prowadzacy
            .GroupJoin(DaneUczelni.Przedmioty,
                pr => pr.Id,
                p => p.ProwadzacyId,
                (pr, przedmioty) => new
                {
                    pr.Imie,
                    pr.Nazwisko,
                    Przedmioty = przedmioty
                })
            .SelectMany(
                ps => ps.Przedmioty.DefaultIfEmpty(),
                (ps, p) => new
                {
                    ps.Imie,
                    ps.Nazwisko,
                    Przedmiot = p
                })
            .GroupJoin(DaneUczelni.Zapisy,
                ps => ps.Przedmiot != null ? ps.Przedmiot.Id : 0,
                z => z.PrzedmiotId,
                (ps, zapisy) => new
                {
                    ps.Imie,
                    ps.Nazwisko,
                    Zapisy = zapisy
                })
            .SelectMany(
                pr => pr.Zapisy.DefaultIfEmpty(),
                (pr, z) => new
                {
                    pr.Imie,
                    pr.Nazwisko,
                    OcenaKoncowa = z?.OcenaKoncowa
                })
            .Where(x => x.OcenaKoncowa != null)
            .GroupBy(x => new
            {
                x.Imie,
                x.Nazwisko
            })
            .Select(g => $"Imię {g.Key.Imie} Nazwisko {g.Key.Nazwisko} Średnia {g.Average(x => x.OcenaKoncowa)}");

        return sredniaOcenProwadzacych;
    }

    /// <summary>
    /// Wyzwanie:
    /// Pokaż miasta studentów oraz liczbę aktywnych zapisów wykonanych przez studentów z danego miasta.
    /// Posortuj wynik malejąco po liczbie aktywnych zapisów.
    ///
    /// SQL:
    /// SELECT s.Miasto, COUNT(*)
    /// FROM Studenci s
    /// JOIN Zapisy z ON s.Id = z.StudentId
    /// WHERE z.CzyAktywny = 1
    /// GROUP BY s.Miasto
    /// ORDER BY COUNT(*) DESC;
    /// </summary>
    public IEnumerable<string> Wyzwanie04_MiastaILiczbaAktywnychZapisow()
    {
        var miastaZapisy = DaneUczelni.Studenci
            .Join(DaneUczelni.Zapisy.Where(z => z.CzyAktywny),
                s => s.Id,
                z => z.StudentId,
                (s, z) => new
                {
                    s.Miasto
                })
            .GroupBy(z => z.Miasto)
            .Select(g => new
            {
                Miasto = g.Key,
                Liczba = g.Count()
            })
            .OrderByDescending(m => m.Liczba)
            .Select(m => $"Miasto {m.Miasto} liczba zapisów {m.Liczba}");

        return miastaZapisy;
    }

    private static NotImplementedException Niezaimplementowano(string nazwaMetody)
    {
        return new NotImplementedException(
            $"Uzupełnij metodę {nazwaMetody} w pliku Exercises/ZadaniaLinq.cs i uruchom polecenie ponownie.");
    }
}
