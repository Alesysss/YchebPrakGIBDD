// В gibbd_tests/UnitTest1.cs

using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using YchebPrakGIBDD.Windows;

namespace gibbd_tests
{
    [TestClass]
    public class AvtorizaciaTests
    {
        // 1. Тестирование успешного входа
        [TestMethod]
        public void TestButtonAvtoriz_SuccessfulLogin()
        {
            var window = new Avtorizacia();
            window.TBlogin.Text = "inspector001";
            window.TBparol.Text = "inspector001";

            MethodInfo method = typeof(Avtorizacia).GetMethod("ButtonAvtoriz", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(window, new object[] { null, null });

            Assert.AreEqual("Вход в систему", window.LastMessage);
        }

        // 2. Тестирование пустых логина и пароля
        [TestMethod]
        public void TestButtonAvtoriz_EmptyLoginAndPassword()
        {
            var window = new Avtorizacia();
            window.TBlogin.Text = "";
            window.TBparol.Text = "";

            MethodInfo method = typeof(Avtorizacia).GetMethod("ButtonAvtoriz", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(window, new object[] { null, null });

            Assert.AreEqual("Заполните все поля", window.LastMessage);
        }

        // 3. Тестирование пустого пароля
        [TestMethod]
        public void TestButtonAvtoriz_EmptyPassword()
        {
            var window = new Avtorizacia();
            window.TBlogin.Text = "inspector001";
            window.TBparol.Text = "";

            MethodInfo method = typeof(Avtorizacia).GetMethod("ButtonAvtoriz", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(window, new object[] { null, null });

            Assert.AreEqual("Заполните все поля", window.LastMessage);
        }

        // 4. Тестирование пустого логина
        [TestMethod]
        public void TestButtonAvtoriz_EmptyLogin()
        {
            var window = new Avtorizacia();
            window.TBlogin.Text = "";
            window.TBparol.Text = "inspector001";

            MethodInfo method = typeof(Avtorizacia).GetMethod("ButtonAvtoriz", BindingFlags.NonPublic | BindingFlags.Instance);
            method.Invoke(window, new object[] { null, null });

            Assert.AreEqual("Заполните все поля", window.LastMessage);
        }
    }
}
