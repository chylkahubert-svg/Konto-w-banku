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

    }
}
