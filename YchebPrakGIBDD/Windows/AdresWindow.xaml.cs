using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdresWindow.xaml
    /// </summary>
    public partial class AdresWindow : Window
    {
        public AdresWindow()
        {
            InitializeComponent();
            PopulateAdres();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Адрес'");
        }
        public void PopulateAdres()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var adres = db.Adres.ToList();
                AdresDG.ItemsSource = adres;
            }
        }
        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void ButtonNazad(object sender, RoutedEventArgs e)
        {
            VoditelWindow voditelWindow = new VoditelWindow();
            voditelWindow.Show();
            this.Close();
        }
    }
}
