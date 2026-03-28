using Bank;

namespace TestKontoLimit
{
    [TestClass]
    public class Test1
    {
        [TestMethod]
        public void Konstruktor_PoprawneDane_TworzyKontoLimit()
        {
            var konto = new KontoLimit("Hubert", 100, 50);

            Assert.AreEqual("Hubert", konto.Nazwa);
            Assert.AreEqual(100m, konto.Bilans);
            Assert.AreEqual(150m, konto.DostepneSrodki);
            Assert.IsFalse(konto.Zablokowane);

        }

        [TestMethod]
        public void Konstruktor_UjemnyLimit_Throw()
        {
            var konto = new KontoLimit("Hubert", 100, -10);
        }

        [TestMethod]
        public void Wplata_PodnosiBilans_ResetujeDebetIOdblokowuje()
        {
            var konto = new KontoLimit("Hubert", 0, 100);

            konto.Wyplata(50);  // użycie debetu -> blokada

            Assert.IsTrue(konto.Zablokowane);

            konto.Wplata(60);   // bilans = 10 -> odblokowanie i reset debetu

            Assert.AreEqual(10m, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
            Assert.AreEqual(110m, konto.DostepneSrodki);
        }

        [TestMethod]
        public void Wyplata_NormalnaZmniejszaBilans()
        {
            var konto = new KontoLimit("Hubert", 100, 50);

            konto.Wyplata(40);

            Assert.AreEqual(60m, konto.Bilans);
            Assert.IsFalse(konto.Zablokowane);
        }

        [TestMethod]
        public void Wyplata_ZDebetem_BlokujeKonto()
        {
            var konto = new KontoLimit("Hubert", 100, 50);

            konto.Wyplata(130); // 100 + 50 limit

            Assert.AreEqual(0m, konto.Bilans);
            Assert.IsTrue(konto.Zablokowane);
            Assert.AreEqual(0m, konto.DostepneSrodki);
        }

        [TestMethod]
        public void Wyplata_PoWykorzystaniuDebetu_Throw()
        {
            var konto = new KontoLimit("Hubert", 100, 50);

            konto.Wyplata(130); // debet wykorzystany

            konto.Wyplata(10);  // nie można już wypłacać
        }

        [TestMethod]
        public void Wyplata_ZaDuzo_Throw()
        {
            var konto = new KontoLimit("Hubert", 100, 50);

            konto.Wyplata(200); // 100 + 50 < 200
        }

        [TestMethod]
        public void UstawLimitDebetu_ZmieniaLimit()
        {
            var konto = new KontoLimit("Hubert", 100, 50);
            
            konto.UstawLimitDebetu(200);

            Assert.AreEqual(300m, konto.DostepneSrodki);
        }

        [TestMethod]
        public void UstawLimitDebetu_Ujemny_Throw()
        {
            var konto = new KontoLimit("Hubert", 100, 50);

            konto.UstawLimitDebetu(-10);
        }

        [TestMethod]
        public void Wplata_GdyZablokowaneNieResetujDebetuJesliBilansNiePrzekraczaZera()
        {
            var konto = new KontoLimit("Hubert", 0, 100);

            konto.Wyplata(50);  // debet -> blokada

            konto.Wplata(40);   // bilans = -10 -> nadal zablokowane

            konto.Wyplata(1);   // nadal nie można wypłacać
        }




    }
}
