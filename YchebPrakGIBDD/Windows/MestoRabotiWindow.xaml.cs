using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для MestoRabotiWindow.xaml
    /// </summary>
    public partial class MestoRabotiWindow : Window
    {
        public MestoRabotiWindow()
        {
            InitializeComponent();
            PopulateMestoRaboti();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Место работы'");
        }
        public void PopulateMestoRaboti()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var mestoRaboti = db.Mesto_raboti.ToList();
                MestoRabotiDG.ItemsSource = mestoRaboti;
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
