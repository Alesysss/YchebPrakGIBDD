using System.Windows;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для W1.xaml
    /// </summary>
    public partial class Glavnaia : Window
    {
        public Glavnaia()
        {
            InitializeComponent();
        }
        private void ButtonAvtorizacia(object sender, RoutedEventArgs e)
        {
            Avtorizacia avtorizacia = new Avtorizacia();
            avtorizacia.Show();
            this.Close();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы находитесь в главном меню приложения");
        }
        private void ButtonVoditel(object sender, RoutedEventArgs e)
        {
            VoditelWindow voditel = new VoditelWindow();
            voditel.Show();
            this.Close();

        }
        private void ButtonVYIS(object sender, RoutedEventArgs e)
        {
            VYIstoriaStatysaWindow vyIstoriaStatysaWindow = new VYIstoriaStatysaWindow();
            vyIstoriaStatysaWindow.Show();
            this.Close();

        }
        private void ButtonAvtomobil(object sender, RoutedEventArgs e)
        {
            AvtomobilWindow avtomobil = new AvtomobilWindow();
            avtomobil.Show();
            this.Close();

        }
        private void ButtonVY(object sender, RoutedEventArgs e)
        {
            VYWindow vyWindow = new VYWindow();
            vyWindow.Show();
            this.Close();

        }
    }
}
