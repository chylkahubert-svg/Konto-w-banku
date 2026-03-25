using System;

namespace Bank
{
    public class KontoPlus : Konto
    {
        private decimal limitDebetu;        // jednorazowy limit debetowy
        private bool debetWykorzystany;     // czy debet został już użyty

        public KontoPlus(string klient, decimal bilansNaStart = 0, decimal limitDebetu =0)
            : base(klient, bilansNaStart)
        {
            if (limitDebetu < 0)
                throw new ArgumentException("Limit debetu nie może być ujemny.");

            this.limitDebetu = limitDebetu;
            this.debetWykorzystany = false;
        }

        // Property zwracające środki dostępne dla klienta
        public decimal DostępneSrodki
        {
            get
            {
                if (debetWykorzystany)
                    return Bilans; // po wykorzystaniu debetu limit znika

                return Bilans + limitDebetu;
            }
        }

        // Możliwość zmiany limitu debetu
        public void UstawLimitDebetu(decimal nowyLimit)
        {
            if (nowyLimit < 0)
                throw new ArgumentException("Limit debetu nie może być ujemny.");

            limitDebetu = nowyLimit;
        }

        public override void Wplata(decimal kwota)
        {
            base.Wplata(kwota);

            if (Bilans > 0)
            {
                debetWykorzystany = false;
                OdblokujKonto();
            }
        }

        public override void Wyplata(decimal kwota)
        {
            if (Zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");

            if (kwota <= 0)
                throw new ArgumentException("Kwota wypłaty musi być dodatnia.");

            //Normalna wypłata - jeśli wystarczy środków
            if (kwota <= Bilans)
            {
                base.Wyplata(kwota);
                return;
            }

            //Jeśli nie ma środków, ale można użyć jednorazowego debetu
            if (!debetWykorzystany && kwota <= Bilans + limitDebetu)
            {
                decimal roznica = kwota - Bilans;
                base.Wyplata(Bilans); // wyzerowanie konta
                debetWykorzystany = true;
                BlokujKonto(); // po debecie konto jest blokowane
                return;
            }

            throw new InvalidOperationException("Brak środków i brak możliwości użycia debetu.");
        }
    }
}
