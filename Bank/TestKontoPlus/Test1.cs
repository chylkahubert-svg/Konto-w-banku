using Bank;

namespace TestKontoPlus
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void Konstuktor_PoprawneDane_TworzyKontoPlus()
        {
            var konto = new KontoPlus("Hubert", 100, 50);

            Assert.AreEqual("Hubert", konto.Nazwa);
            Assert.AreEqual(100m, konto.Bilans);
            Assert.AreEqual(150m, konto.DostepneSrodki);
        }

        [TestMethod]
        public void Konstruktor_UjemnyLimit_Throw()
        {
            var konto = new KontoPlus("Hubert", 100, -10);
        }

        [TestMethod]
        public void Wplata_PodnosiBilans_ResetujeDebetIOdblokowuje()
        {
            var konto = new KontoPlus("Hubert", 0, 100);

            // Użycie debetu
            konto.Wyplata(50);

            Assert.IsTrue(konto.Zablokowane);

            konto.Wyplata(60);

            Assert.AreEqual(10m, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(110m, konto.DostepneSrodki);
        }

        [TestMethod]
        public void Wyplata_NormalnaZmniejszaBilans()
        {
            var konto = new KontoPlus("Hubert", 100, 50);
            
            konto.Wyplata(40);

            Assert.AreEqual(60m, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void Wyplata_ZDebetem_BlokujKonto()
        {
            var konto = new KontoPlus("Hubert", 100, 50);

            konto.Wyplata(130); // 100 + 50 limit

            Assert.AreEqual(0m, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
            Assert.AreEqual(0m, konto.DostepneSrodki);
        }

        [TestMethod]
        public void Wyplata_PoWykorzystaniuDebetu_Throw()
        {
            var konto = new KontoPlus("Hubert", 100, 50);

            konto.Wyplata(130); // debet wykorzystany

            konto.Wyplata(10); // nie można już wypłacać
        }


    }
}
