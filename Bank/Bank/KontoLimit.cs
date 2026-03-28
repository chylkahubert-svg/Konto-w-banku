using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class KontoLimit
    {
        private Konto konto;            // delegowany obiekt Konto
        private decimal limitDebetu;    // jednorazowy limit debetowy
        private bool debetWykorzystany; // czy debet został już użyty

        public KontoLimit(string klient, decimal bilansNaStart = 0, decimal limitDebetu = 0)
        {
            if (limitDebetu < 0)
                throw new ArgumentException("Limit debetu nie może być ujemny.");

            konto = new Konto(klient, bilansNaStart);
            this.limitDebetu = limitDebetu;
            this.debetWykorzystany = false;
        }

        // Właściwości publiczne
        public string Nazwa => konto.Nazwa;
        public decimal Bilans => konto.Bilans;
        public bool Zablokowane => konto.Zablokowane;

        // Dostępne środki (bilans + limit, jeśli debet nie został użyty)
        public decimal DostepneSrodki
        {
            get
            {
                if (debetWykorzystany)
                    return konto.Bilans;

                return konto.Bilans + limitDebetu;
            }
        }

        // Zmiana limitu debetu
        public void UstawLimitDebetu(decimal nowyLimit)
        {
            if (nowyLimit < 0)
                throw new ArgumentException("Limit debetu nie może być ujemny.");

            limitDebetu = nowyLimit;
        }

        // Delegacja blokowania
        public void BlokujKonto() => konto.BlokujKonto();
        public void OdblokujKonto() => konto.OdblokujKonto();

        // Wpłata
        public void Wplata(decimal kwota)
        {
            konto.Wplata(kwota);

            //Jeśli bilans > 0, resetujemy debet i odblokowujemy konto
            if (konto.Bilans < 0)
            {
                debetWykorzystany = false;
                konto.OdblokujKonto();
            }
        }

        // Wypłata
        public void Wyplata(decimal kwota)
        {
            if (konto.Zablokowane)
                throw new ArgumentException("Konto jest zablokowane.");

            if (kwota <= 0)
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");

            // Normalna wypłata
            if (kwota <= konto.Bilans)
            {
                konto.Wyplata(kwota);
                return;
            }

            // Debet jednorazowy
            if (!debetWykorzystany && kwota <= konto.Bilans + limitDebetu)
            {
                decimal roznica = kwota - konto.Bilans;
                konto.Wyplata(konto.Bilans); // wyzerowanie konta
                debetWykorzystany = true;
                konto.BlokujKonto();
                return;
            }

            throw new InvalidOperationException("Brak środków i brak możliwości użycia debetu.");
        }
    }
}
