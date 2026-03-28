using Bank;

namespace TestKonto
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void Konstruktor_PoprawneDane_TworzyKonto()
        {
            var konto = new Konto("Hubert", 100);

            Assert.AreEqual("Hubert", konto.Nazwa);
            Assert.AreEqual(100m, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void Konstruktor_PustaNazwa_Throw()
        {
            var konto = new Konto("", 0);
        }

        [TestMethod]
        public void Konstruktor_UjemnyBilans_Throw()
        {
            var konto = new Konto("Hubert", -10);
        }

        [TestMethod]
        public void Wplata_PrawidlowaKwota_ZwiekszaBilans()
        {
            var konto = new Konto("Hubert", 100);

            konto.Wplata(50);

            Assert.AreEqual(150m, konto.Bilans);
        }

        [TestMethod]
        public void Wplata_NieprawidlowaKwota_Throw()
        {
            var konto = new Konto("Hubert", 100);

            konto.Wplata(0);
        }

        [TestMethod]
        public void Wyplata_PrawidlowaKwota_ZmniejszaBilans()
        {
            var konto = new Konto("Hubert", 100);

            konto.Wyplata(40);

            Assert.AreEqual(60m, konto.Bilans);
        }

        [TestMethod]
        public void Wyplata_ZaDuzo_Throw()
        {
            var konto = new Konto("Hubert", 100);

            konto.Wyplata(150);
        }

        [TestMethod]
        public void BlokujKonto_UstawieniaZablokowaneNaTrue()
        {
            var konto = new Konto("Hubert", 100);
            konto.BlokujKonto();

            konto.OdblokujKonto();

            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void OdblokujKonto_UstawieniaZablokowaneFalse()
        {
            var konto = new Konto("Hubert", 100);
            konto.BlokujKonto();

            konto.OdblokujKonto();

            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void Wplata_GdyZablokowane_Throw()
        {
            var konto = new Konto("Hubert", 100);
            konto.BlokujKonto();

            konto.Wplata(50);
        }

        [TestMethod]
        public void Wyplata_GdyZablokowane_Throw()
        {
            var konto = new Konto("Hubert", 100);
            konto.BlokujKonto();

            konto.Wyplata(50);
        }
    }
}
