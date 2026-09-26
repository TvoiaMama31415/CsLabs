using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;

namespace lapka2.test
{
    public class ProgramTests
    {
        [Test]
        public void CheckConfiguration_ValidConfiguration_AllGood()
        {
            var result = Program.CheckConfiguration(50, 8, true, false);
            Assert.That(result, Is.EqualTo("Все хорошо, приятной игры <3"));
        }

        [Test]
        public void CheckConfiguration_ZeroPlayers_LaunchImpossible()
        {
            var result = Program.CheckConfiguration(0, 8, true, false);
            Assert.That(result, Is.EqualTo("Запуск невозможен, некому играть (._.)"));
        }

        [Test]
        public void CheckConfiguration_NotEnoughMemory_LaunchImpossible()
        {
            var result = Program.CheckConfiguration(50, 1, true, false);
            Assert.That(result, Is.EqualTo("Запуск невозможен, недостаточно оперативной памяти (;_;)"));
        }

        [Test]
        public void CheckConfiguration_TooManyPlayers_TooMuch()
        {
            var result = Program.CheckConfiguration(150, 8, true, false);
            Assert.That(result, Is.EqualTo("Че так дофига `Σ(°△°"));
        }

        [Test]
        public void CheckConfiguration_PublicWithPassword_Anarchy()
        {
            var result = Program.CheckConfiguration(50, 8, true, true);
            Assert.That(result, Is.EqualTo("Анархия нафик (╯°▽°)╯︵ ┻━┻"));
        }

        [Test]
        public void CheckConfiguration_PrivateWithPassword_Cheburnet()
        {
            var result = Program.CheckConfiguration(50, 8, false, true);
            Assert.That(result, Is.EqualTo("Мы в Чебурнете ＾◡＾"));
        }
    }
}