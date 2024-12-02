using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    public partial class Avtorizacia : Window
    {
        private int failedAttempts = 0;
        private DateTime? blockEndTime = null;
        private DispatcherTimer countdownTimer = new DispatcherTimer();
        public string LastMessage { get; private set; }

        public Avtorizacia()
        {
            InitializeComponent();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Добро пожаловать в приложение ГИБДД!");
        }
        // Метод для авторизации
        internal void ButtonAvtoriz(object sender, RoutedEventArgs e)
        {
            string login = TBlogin.Text;
            string password = TBparol.Text;

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                LastMessage = "Заполните все поля";
                return;
            }

            // Пример логики авторизации
            if (login == "inspector001" && password == "inspector001")
            {
                LastMessage = "Вход в систему";
            }
            else
            {
                LastMessage = "Логин или пароль введен неверно.";
            }
        }

        private void ButtonAvt(object sender, RoutedEventArgs e)
        {
            if (blockEndTime != null && blockEndTime > DateTime.Now)
            {
                MessageBox.Show("Подождите минуту прежде чем снова повторить попытку.");
                return;
            }

            if (TBlogin.Text != "" && TBparol.Text != "")
            {
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    bool isAuthenticated = false;

                    foreach (var u in db.Users)
                    {
                        if (u.Login == TBlogin.Text && u.Parol == TBparol.Text)
                        {
                            isAuthenticated = true;
                            if (u.Role == "Incpektor")
                            {
                                ShtrafiWindow shtrafi = new ShtrafiWindow();
                                TBlogin.Text = "";
                                TBparol.Text = "";
                                this.Close();
                                shtrafi.Show();
                                break;
                            }
                            if (u.Role == "Nachalnik")
                            {
                                Glavnaia glavnaia = new Glavnaia();
                                TBlogin.Text = "";
                                TBparol.Text = "";
                                this.Close();
                                glavnaia.Show();
                                break;
                            }
                        }
                    }

                    if (!isAuthenticated)
                    {
                        MessageBox.Show("Логин или пароль введен не верно");
                        TBlogin.Text = "";
                        TBparol.Text = "";

                        failedAttempts++;
                        if (failedAttempts >= 3)
                        {
                            blockEndTime = DateTime.Now.AddMinutes(1);
                            Properties.Settings.Default.BlockEndTime = blockEndTime.Value;
                            Properties.Settings.Default.Save();
                            StartBlock();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните поля");
            }
        }
        private void StartBlock()
        {
            TBlogin.IsEnabled = false;
            TBparol.IsEnabled = false;
            MyButton.IsEnabled = false;
            CountdownText.Visibility = Visibility.Visible;
            countdownTimer.Start();
        }

        private void EndBlock()
        {
            TBlogin.IsEnabled = true;
            TBparol.IsEnabled = true;
            MyButton.IsEnabled = true;
            CountdownText.Visibility = Visibility.Collapsed;
            failedAttempts = 0;
            blockEndTime = null;
            countdownTimer.Stop();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            if (blockEndTime != null)
            {
                TimeSpan remainingTime = blockEndTime.Value - DateTime.Now;
                if (remainingTime <= TimeSpan.Zero)
                {
                    EndBlock();
                }
                else
                {
                    CountdownText.Text = $"Подождите, чтобы повторить попытку {remainingTime:mm\\:ss}";
                }
            }
        }

    }
}
