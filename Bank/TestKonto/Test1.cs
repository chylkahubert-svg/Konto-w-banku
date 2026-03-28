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
    }
}
