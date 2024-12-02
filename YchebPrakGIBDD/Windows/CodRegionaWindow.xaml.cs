using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для CodRegionaWindow.xaml
    /// </summary>
    public partial class CodRegionaWindow : Window
    {
        public CodRegionaWindow()
        {
            InitializeComponent();
            PopulateCodRegiona();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Код региона'");
        }
        public void PopulateCodRegiona()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var code_Regiona = db.Code_Regiona.ToList();
                CodRegionaDG.ItemsSource = code_Regiona;
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
