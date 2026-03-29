using System;
using System.Collections.Generic;
using System.Text;

namespace Bank
{
    public class Konto
    {
        private string klient;              //nazwa klienta
        private decimal bilans;             // aktualny stan środków
        private bool zablokowane = false;   // status konta

        //Konstruktor domyślny byłby zbędny - nie chcemy kont bez nazwy klienta

        public Konto(string klient, decimal bilansNaStart = 0)
        {
            if (string.IsNullOrWhiteSpace(klient))
                throw new ArgumentException("Nazwa klienta nie może być pusta.");

            if (bilansNaStart < 0)
                throw new ArgumentException("Bilans początkowy nie może być ujemny.");

            this.klient = klient;
            this.bilans = bilansNaStart;
        }

        //Właściwości tylko do odczytu
        public string Nazwa => klient;
        public decimal Bilans => bilans;
        public bool Zablokowane => zablokowane;

        //Blokowanie i odblokowywanie konta
        public void BlokujKonto()
        {
            zablokowane = true;
        }

        public void OdblokujKonto()
        {
            zablokowane = false;
        }

        //Metoda wpłaty
        public virtual void Wplata(decimal kwota)
        {
            if (kwota <= 0)
                throw new ArgumentException("Kwota wpłaty musi być dodatnia.");

            
            //if (zablokowane)
            //    throw new InvalidOperationException("Konto jest zablokowane.");
            bilans += kwota;
            return;
        }

        // Metoda wypłaty
        public virtual void Wyplata(decimal kwota)
        {
            if (zablokowane)
                throw new InvalidOperationException("Konto jest zablokowane.");

            if (kwota <= 0)
                throw new ArgumentException("Kwota wpłaty musi być dodatnia.");

            if (kwota > bilans)
                throw new InvalidOperationException("Brak wystarczających środków na koncie.");

            bilans -= kwota;
            return;
        }
    }
}
