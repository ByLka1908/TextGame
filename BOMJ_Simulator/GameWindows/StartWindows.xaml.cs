using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BOMJ_Simulator.BusinessLogic;

namespace BOMJ_Simulator.GameWindows
{
    
    /// <summary>
    /// Логика взаимодействия для StartWindows.xaml
    /// </summary>
    public partial class StartWindows : Window
    {
        public Game Game;
        public Person Person;

        // Нужно для последующего закрытия при выходе в главное меню
        public MainWindow MainWindow;

        // Вывести сообщение на экран
        void DisplayMessage(string message) => tblDisplay.Text = message;

        // Стандартный конструктор
        public StartWindows(MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;

            InitGame();
            InitPerson();
            InitButtons();
        }
        // Конструктор, использующийся для загрузки сохранений
        public StartWindows(Game newGame, Person newPerson, MainWindow mainWindow)
        {
            InitializeComponent();
            MainWindow = mainWindow;

            InitGame(newGame);
            InitPerson(newPerson);
            InitButtons();
        }
        // Инициализация игры
        public void InitGame(Game game = null)
        {
            if (game == null)
            {
                Game = new Game
                {
                    GameHoursPerOneRealMinute = 4,
                    DisplayText = "Добро пожаловать в Симулятор Дворника!" +
                    "\n\nВ правом верхнем углу вы видите такие характеристики, как: еда, сон, деньги и образование." +
                    "\n\nСлева от дисплея находятся действия, которые влияют на персонажа." +
                    "\n\nСпарва от дисплея находится уровни образования"+
                    "\n\nСмысл игры заработать 30.000 за 30 игровых дней" +
                    "\n\nПовышая уровень образования вы получаете доступ к более лучшей работе" +
                    "\nВсего в игре 5 уровней образования:Средне общие(дается сразу после начала игры)"+
                    "\n\nСреднее профиссиональное, Высшее образование(бакалавриат), Высшее образование(магистратура) и Высшее(высшей квалификации)" +
                    "\n\nОБразование получают после его  покупки." +
                    "\n\nПриятной игры."
                };
            }
            else
            {
                Game = game;
            }

            DisplayMessage(Game.DisplayText);
        }
        // Инициализация игрока
        public void InitPerson(Person person = null)
        {
            if (person == null)
            {
                Person = new Person 
                {
                    Eda = 100,
                    Son = 100,
                    Money = 1000,
                    Rank = new Rank(Rank.GetRankNameById(1)),

                    CurrentTime = new TimeSpan(0, 0, 0, 0)
                };
            }
            else
            {
                Person = person;
            }
            // Person.Notify -> DisplayMessage
            Person.Notify += DisplayMessage;

            // Методы класса Game, вызов которых возможен только после инициализация игрока
            Game.SetPersonAndHomeWindow(this, Person);
            Game.RefreshCharacteristics();
            Game.ProcessTime();
        }
        // Кнопка "Пауза"
        void BtnPauseGame_Click(object sender, RoutedEventArgs e)
        {
            PauseWindows pauseWindow = new PauseWindows(this);

            WPF_Misc.FocusWindow(pauseWindow);
            WPF_Misc.OpenPauseWindow(this, pauseWindow);
        }

        // Метод, отвечающий за выполнение других методов при нажатии на кнопки
        public void BtnActions_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btEat": Person.Eat(); break;
                case "btSon": Person.Sleep(); break;
                case "btProfessionalOBJ":
                    {
                        if (Person.Rank.Id >= 2)
                        {
                            tblDisplay.Text = "Вы уже имеете это ранг или лучше";
                        }
                        else 
                        {
                            if (Person.SubstractMoney(1000) == true)
                            {
                                Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(3));
                                Person.Rank = new Rank(Rank.GetRankNameById(2));
                            }
                        }
                    }break;

                case "btHightBakalavr":
                    {
                        if (Person.Rank.Id >= 3)
                        {
                            tblDisplay.Text = "Вы уже имеете это ранг или лучше";
                        }
                        else
                        {
                            if(Person.SubstractMoney(1500) == true)
                            {
                                Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(5));
                                Person.Rank = new Rank(Rank.GetRankNameById(3));
                            }
                        }
                    }
                    break;

                case "btHightMagistr":
                    {
                        if (Person.Rank.Id >= 4)
                        {
                            tblDisplay.Text = "Вы уже имеете это ранг или лучше";
                        }
                        else
                        {
                            if (Person.SubstractMoney(2000) == true)
                            {
                                Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(7));
                                Person.Rank = new Rank(Rank.GetRankNameById(4));
                            }
                        }
                    }break;

                case "btHightHight":
                {
                        if (Person.Rank.Id >= 5)
                        {
                            tblDisplay.Text = "Вы уже имеете это ранг или лучше";
                        }
                        else
                        {
                            if (Person.SubstractMoney(2500) == true)
                            {
                                Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(10));
                                Person.Rank = new Rank(Rank.GetRankNameById(5));
                            }
                        }
                }break;                

                //Работа 
                case "btDvornik":
                    { 
                        if(Person.Rank.Id >= 1 && Person.SubstractSon(15) == true
                            && Person.SubstractEda(10) == true)
                        {
                            Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(2));
                            Person.AddMoney(100);
                            tblDisplay.Text = "Вы проработали 2 часа + 100р\n-15 ед.сна\n -10 еды";
                        }
                        else
                        {
                            tblDisplay.Text = "Вы еще не получили нужны ранг";
                        }
                    }break;

                case "btPatcorocha":
                    {
                        if (Person.Rank.Id >= 2 && Person.SubstractSon(10) == true
                            && Person.SubstractEda(10) == true)
                        {
                            Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(4));
                            Person.AddMoney(250);
                            tblDisplay.Text = "Вы проработали 4 часа + 250\n-10 ед.сна\n -10 еды";
                        }
                        else
                        {
                            tblDisplay.Text = "Вы еще не получили нужны ранг";
                        }
                    }
                    break;                
                
                case "btKlining":
                    {
                        if (Person.Rank.Id >= 3 && Person.SubstractSon(10) == true
                            && Person.SubstractEda(7) == true)
                        {
                            Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(6));
                            Person.AddMoney(500);
                            tblDisplay.Text = "Вы проработали 6 часов + 500\n-10 ед.сна\n -7 еды";
                        }
                        else
                        {
                            tblDisplay.Text = "Вы еще не получили нужны ранг";
                        }
                    }
                    break;                
                
                case "btDepytat":
                    {
                        if (Person.Rank.Id >= 4 && Person.SubstractSon(5) == true
                            && Person.SubstractEda(5) == true)
                        {
                            Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(8));
                            Person.AddMoney(1000);
                            tblDisplay.Text = "Вы проработали 8 часов + 1000\n-5 ед.сна\n -5 еды";
                        }
                        else
                        {
                            tblDisplay.Text = "Вы еще не получили нужны ранг";
                        }
                    }
                    break;                
                
                case "btPresident":
                    {
                        if (Person.Rank.Id >= 5 && Person.SubstractSon(2) == true
                            && Person.SubstractEda(2) == true)
                        {
                            Person.CurrentTime = Person.CurrentTime.Add(TimeSpan.FromHours(10));
                            Person.AddMoney(2000);
                            tblDisplay.Text = "Вы проработали 10 часов + 2000\n-2 ед.сна\n -2 еды";
                        }
                        else
                        {
                            tblDisplay.Text = "Вы еще не получили нужны ранг";
                        }
                    }
                    break;
            }        
            Game.RefreshCharacteristics();
        }

        
        // Инициализация кнопок
        void InitButtons()
        {          
            btnPauseGame.Click += BtnPauseGame_Click;
            btEat.Click += BtnActions_Click;
            btSon.Click += BtnActions_Click;
            btProfessionalOBJ.Click += BtnActions_Click;
            btHightBakalavr.Click += BtnActions_Click;
            btHightMagistr.Click += BtnActions_Click;
            btHightHight.Click += BtnActions_Click;

            btDvornik.Click += BtnActions_Click;
            btPatcorocha.Click += BtnActions_Click;
            btKlining.Click += BtnActions_Click;
            btDepytat.Click += BtnActions_Click;
            btPresident.Click += BtnActions_Click;
        }
    }
}
