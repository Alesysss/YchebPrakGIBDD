using System.Linq;
using System.Windows;
using YchebPrakGIBDD.Entities;

namespace YchebPrakGIBDD.Windows
{
    /// <summary>
    /// Логика взаимодействия для ShtrafiWindow.xaml
    /// </summary>
    public partial class ShtrafiWindow : Window
    {
        private int shtrafiVY_ID;
        private int shtrafiAvtomobil_ID;
        private string shtrafiStatys;
        private string shtrafiSymma;
        public ShtrafiWindow()
        {
            InitializeComponent();
            PopulateShtrafi();
        }
        public ShtrafiWindow(int VY_ID, int Avtomobil_ID, string Statys, string Symma)
        {
            InitializeComponent();
            shtrafiVY_ID = VY_ID;
            shtrafiAvtomobil_ID = Avtomobil_ID;
            shtrafiStatys = Statys;
            shtrafiSymma = Symma;
            PopulateShtrafi();
        }
        public string Statys_Name { get; set; }

        public void PopulateShtrafi()
        {
            using (var db = new GIBDDEntities())  
            {
                var shtrafi = db.Shtrafi.ToList();
                var transformedShtrafi = shtrafi.Select(s => new
                {
                    s.ID,
                    s.VY_ID,
                    s.Avtomobil_ID,
                    StatysDescription = GetStatysDescription(s.Statys_ID), 
                    s.Symma
                }).ToList();

                ShtrafiDG.ItemsSource = transformedShtrafi;
            }
        }
        private string GetStatysDescription(int statysId)
        {
            switch (statysId)
            {
                case 1: return "Не оплачен";
                case 2: return "Оплачен";
                default: return "Неизвестно";
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы перешли на окно 'Штрафы'");
        }
        private void ButtonAvtorizacia(object sender, RoutedEventArgs e)
        {
            Avtorizacia avtorizacia = new Avtorizacia();
            avtorizacia.Show();
            this.Close();
        }        
        private void AddButton(object sender, RoutedEventArgs e)
        {
            ShtrafiAdd shtrafiAdd = new ShtrafiAdd();
            shtrafiAdd.Show();
            PopulateShtrafi();
            this.Close();
        }
        private void ChangeButton(object sender, RoutedEventArgs e)
        {
            if (ShtrafiDG.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите штраф для изменения",
                                "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            dynamic selectedItem = ShtrafiDG.SelectedItem;

            if (selectedItem == null || selectedItem.ID == null)
            {
                MessageBox.Show("Не удалось определить выбранный штраф.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int selectedId = selectedItem.ID;

            using (var db = new GIBDDEntities())
            {
                var entityToEdit = db.Shtrafi.FirstOrDefault(x => x.ID == selectedId);

                if (entityToEdit == null)
                {
                    MessageBox.Show("Штраф не найден в базе данных.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ShtrafiChange shtrafiChangeWindow = new ShtrafiChange(entityToEdit);
                shtrafiChangeWindow.Show();
                this.Close();
            }
        }

        private void DeleteButton(object sender, RoutedEventArgs e)
        {
            if (ShtrafiDG.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите штраф для удаления",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Преобразуем выбранный элемент к dynamic
            dynamic selectedItem = ShtrafiDG.SelectedItem;

            if (selectedItem == null || selectedItem.ID == null)
            {
                MessageBox.Show("Не удалось определить выбранный штраф.",
                                "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            using (var db = new GIBDDEntities())
            {
                int selectedId = selectedItem.ID;
                var entityToDelete = db.Shtrafi.FirstOrDefault(x => x.ID == selectedId);

                if (entityToDelete == null)
                {
                    MessageBox.Show("Штраф не найден в базе данных.",
                                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранный штраф?",
                                                          "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    db.Shtrafi.Remove(entityToDelete);
                    db.SaveChanges();
                    PopulateShtrafi();
                    MessageBox.Show("Штраф успешно удалён.",
                                    "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}
