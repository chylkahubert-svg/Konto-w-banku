using Bank;

namespace BankDemo
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("=== DEMO: Konto ===");
            var k1 = new Konto("Hubert", 100);
            Console.WriteLine($"Utworzono konto: {k1.Nazwa}, bilans: {k1.Bilans}");

            k1.Wplata(50);
            Console.WriteLine($"Po wpłacie 50: {k1.Bilans}");

            k1.Wyplata(30);
            Console.WriteLine($"Po wypłacie 30: {k1.Bilans}");

            k1.BlokujKonto();
            Console.WriteLine($"Konto zablokowane: {k1.Zablokowane}");

            try
            {
                k1.Wplata(10);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd przy wpłacie: {ex.Message}");
            }

            Console.WriteLine("\n=== DEMO: KontoPlus ===");
            var kp = new KontoPlus("Anna", 100, 50);
            Console.WriteLine($"KontoPlus: bilans: {kp.Bilans}, dostępne środki: {kp.DostepneSrodki}");

            kp.Wyplata(130); // użycie debetu
            Console.WriteLine($"Po wypłacie 130: bilans: {kp.Bilans}, zablokowane: {kp.Zablokowane}");

            kp.Wplata(200); // odblokowanie i reset debetu
            Console.WriteLine($"Po wpłacie 200: bilans: {kp.Bilans}, dostępne środki: {kp.DostepneSrodki}, zablokowane: {kp.Zablokowane}");

            Console.WriteLine("\n=== DEMO: KontoLimit ===");
            var kl = new KontoLimit("Marek", 50, 100);
            Console.WriteLine($"KontoLimit: bilans: {kl.Bilans}, dostępne środki: {kl.DostepneSrodki}");

            kl.Wyplata(120); // użycie debetu
            Console.WriteLine($"Po wypłacie 120: bilans: {kl.Bilans}, zablokowane: {kl.Zablokowane}");

            kl.Wplata(200); // odblokowanie
            Console.WriteLine($"Po wpłacie 200: bilans: {kl.Bilans}, dostępne środki: {kl.DostepneSrodki}, zablokowane: {kl.Zablokowane}");

            Console.WriteLine("\n=== KONIEC DEMO ===");
            Console.ReadKey();
        }
    }
}
