using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для PrivodWindow.xaml
    /// </summary>
    public partial class PrivodWindow : Window
    {
        public PrivodWindow()
        {
            InitializeComponent();
            PopulatePrivod();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Привод'");
        }
        public void PopulatePrivod()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var privod = db.Privod.ToList();
                PrivodDG.ItemsSource = privod;
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
            AvtomobilWindow avtomobilWindow = new AvtomobilWindow();
            avtomobilWindow.Show();
            this.Close();
        }
    }
}
