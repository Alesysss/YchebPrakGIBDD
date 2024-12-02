using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для VoditelWindow.xaml
    /// </summary>
    public partial class VoditelWindow : Window
    {
        private string voditelImia;
        private string voditelFamilia;
        private string voditelVtoroe_Imia;
        private int voditelSeria_pasporta;
        private int voditelNomer_Pasporta;
        private int voditelAdres_prop_ID;
        private int voditelAdres_proziv_ID;
        private int voditelMesto_raboti_ID;
        private string voditelTelefon;
        private string voditelPochta;
        private byte[] voditelFoto;
        private string voditelOpisanie;
        public VoditelWindow()
        {
            InitializeComponent();
            PopulateVoditel();
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Водитель'");
        }
        public VoditelWindow(string Imia, string Familia, string Vtoroe_Imia, int Seria_pasporta,
                             int Nomer_Pasporta, int Adres_prop_ID, int Adres_proziv_ID,
                             int Mesto_raboti_ID, string Telefon, string Pochta, byte[] Foto,
                             string Opisanie)
        {
            InitializeComponent();
            voditelImia = Imia;
            voditelFamilia = Familia;
            voditelVtoroe_Imia = Vtoroe_Imia;
            voditelSeria_pasporta = Seria_pasporta;
            voditelNomer_Pasporta = Nomer_Pasporta;
            voditelAdres_prop_ID = Adres_prop_ID;
            voditelAdres_proziv_ID = Adres_proziv_ID;
            voditelMesto_raboti_ID = Mesto_raboti_ID;
            voditelTelefon = Telefon;
            voditelPochta = Pochta;
            voditelFoto = Foto;
            voditelOpisanie = Opisanie;
            PopulateVoditel();
        }

        public void PopulateVoditel()
        {
            using (GIBDDEntities db = new GIBDDEntities())
            {
                var agent = db.Voditel.ToList();
                VoditelDG.ItemsSource = agent;
            }
        }

        private void ButtonGlavnaia(object sender, RoutedEventArgs e)
        {
            Glavnaia glavnaia = new Glavnaia();
            glavnaia.Show();
            this.Close();
        }
        private void MestoRabotiButton(object sender, RoutedEventArgs e)
        {
            MestoRabotiWindow mestoRabotiWindow = new MestoRabotiWindow();
            mestoRabotiWindow.Show();
            this.Close();
        }
        private void AdresButton(object sender, RoutedEventArgs e)
        {
            AdresWindow adresWindow = new AdresWindow();
            adresWindow.Show();
            this.Close();
        }
        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            var selectedVoditel = VoditelDG.SelectedItem as Voditel;


            if (selectedVoditel != null)
            {
                // Проверка наличия связей с водительским удостоверением
                using (GIBDDEntities db = new GIBDDEntities())
                {
                    // Проверка наличия связей
                    var hasVoditelskoe_ydostoverenie = db.Voditelskoe_ydostoverenie.Any(d => d.Voditel_ID == selectedVoditel.ID);

                    if (hasVoditelskoe_ydostoverenie)
                    {
                        MessageBoxResult result1 = MessageBox.Show("Вы уверены, что хотите удалить водителя, который связан с водительским удостоверением? Водительское удостоверение будет также удалено",
                                                                   "Подтверждение удаления", MessageBoxButton.YesNo,
                                                                   MessageBoxImage.Question);
                        if (result1 == MessageBoxResult.Yes)
                        {
                            db.Voditel.Attach(selectedVoditel);
                            db.Voditel.Remove(selectedVoditel);
                            db.SaveChanges();
                            PopulateVoditel();

                            MessageBox.Show("Водитель успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;

                        }
                        if (result1 == MessageBoxResult.No)
                        {
                            return;
                        }

                    }
                    MessageBoxResult result2 = MessageBox.Show("Вы уверены, что хотите удалить выбранного водителя?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result2 == MessageBoxResult.Yes)
                    {
                        db.Voditel.Attach(selectedVoditel);
                        db.Voditel.Remove(selectedVoditel);
                        db.SaveChanges();
                        PopulateVoditel();

                        MessageBox.Show("Водитель успешно удален", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    }

                    
                }

            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите водителя для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            VoditelAdd voditelAdd = new VoditelAdd();
            voditelAdd.Show();
            PopulateVoditel();
            this.Close();            
        }
        private void ChangeButton(object sender, RoutedEventArgs e)
        {
            var selectedVoditel = VoditelDG.SelectedItem as Voditel;
            if (selectedVoditel != null)
            {
                VoditelChange voditelChange = new VoditelChange(selectedVoditel);
                voditelChange.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите водителя для изменения", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
