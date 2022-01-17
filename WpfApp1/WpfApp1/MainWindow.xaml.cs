using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //текущее положение кота
        static Button currentCat;
        static Button currentDog;

        //извлечение изображения и создание кистей
        Uri dogUri = new Uri("pack://application:,,,/пес.png");
        ImageBrush dog = new ImageBrush();

        Uri catUri = new Uri("pack://application:,,,/кот.png");
        ImageBrush cat = new ImageBrush();

        Uri emptyUri = new Uri("pack://application:,,,/прозрачный.png");
        ImageBrush emty = new ImageBrush();

        public MainWindow()
        {
            InitializeComponent();

            //приветственный текст
            MessageBox.Show("Игра: \"КОТ И СОБАКИ\" " +
                "\nЦель собак: поймать кота " +
                "\nЦель кота: перейти на другой берег " +
                "\nПравила: собаки ходят на одну клетку вперед по диагонали " +
                "\nкот ходит на одну клетку вперед/назад по диагонали");

            Text.Text = "Ходит кот";

            //задаем кнопку с помощью имя клетки
            currentCat = Startcat;

            //указываем значение ImageSourc у кистей
            dog.ImageSource = new BitmapImage(dogUri);
            cat.ImageSource = new BitmapImage(catUri);
            emty.ImageSource = new BitmapImage(emptyUri);

            //проходимся по всем элементам сетки
            foreach (UIElement sell in Sells.Children)
            {
                //если элемент - кнопка, добавляем к нему фунцкию Button_Click при нажатии и прозрачный фон 
                if (sell is Button btn)
                {
                    btn.Click += Button_Click;
                    if (btn.Content != null) btn.Background = dog;
                    else btn.Background = emty;
                }
            }

            currentCat.Background = cat;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            //прописываем условия игры и если они совпадают - выполняем описанные методы

            if (Move.catMove 
                && ((Grid.GetRow(button) == Grid.GetRow(currentCat) - 1 
                || Grid.GetRow(button) == Grid.GetRow(currentCat) + 1) 
                && (Grid.GetColumn(button) == Grid.GetColumn(currentCat) - 1 
                || Grid.GetColumn(button) == Grid.GetColumn(currentCat) + 1)))
            {
                CatMove(button);
                Text.Text = "Выберите собаку";
            }
            else if(Move.choiceDog && button.Background == dog)
            {
                ChoiceDog(button);
                Text.Text = "Ходит выбранная собака";
            }
            else if (Move.dogMove 
                &&Grid.GetRow(button) == Grid.GetRow(currentDog) + 1
                && (Grid.GetColumn(button) == Grid.GetColumn(currentDog) - 1
                || Grid.GetColumn(button) == Grid.GetColumn(currentDog) + 1))
            {
                DogMove(button);
                Text.Text = "Ходит кот";
            }
            else if(Move.dogMove && button.Background == dog)
            {
                ChoiceDog(button);
            }
        }

        private void ChoiceDog(Button button)
        {
            currentDog = button;
            Move.choiceDog = false;
            Move.dogMove = true;
        }

        private void CatMove(Button button)
        {
            if (button.Background == dog)
                DogsWin();

            button.Background = cat;
            currentCat.Background = emty;

            currentCat = button;

            Move.catMove = false;
            Move.choiceDog = true;

            if (currentCat.Content != null)
                CatWin();
        }

        private void DogMove(Button button)
        {
            button.Background = dog;
            currentDog.Background = emty;

            Move.catMove = true;
            Move.dogMove = false;
            
            if (button == currentCat)
                DogsWin();
        }

        private void CatWin()
        {
            MessageBox.Show("Кот победил");
            Move.choiceDog = false;
            Move.dogMove= false;
            Move.catMove= false;
            Text.Text = "Игра окончена";       
        }

        private void DogsWin()
        {
            MessageBox.Show("Собаки победили");
            Move.choiceDog = false;
            Move.dogMove = false;
            Move.catMove = false;
            Text.Text = "Игра окончена";
        }

    }
}
