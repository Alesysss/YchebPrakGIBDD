using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для TipDvigateliaWindow.xaml
    /// </summary>
    public partial class TipDvigateliaWindow : Window
    {
        public TipDvigateliaWindow()
        {
            InitializeComponent();
            PopulateTipDvigatelia();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Тип двигателя'");
        }
        public void PopulateTipDvigatelia()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var tipDvigatelia = db.Tip_Dvigatelia.ToList();
                TipDvigateliaDG.ItemsSource = tipDvigatelia;
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
