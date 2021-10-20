using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMJ_Simulator.BusinessLogic
{
    public class Person
    {
        // Делегат и событие для отправки сообщений во внешнюю среду
        public delegate void NotificationHandler(string message);
        public event NotificationHandler Notify;

        //Основные характеристики игрока
        public int Eda { get; set; }
        public int Son { get; set; }
        public int Money { get; set; }
        public Rank Rank { get; set; }


		public TimeSpan CurrentTime { get; set; }
		private static Random random;
		public Person()
		{
			random = new Random();
		}

		// Косвенные траты по-прохождению hours часов
		private bool SpendResourcesByHours(double hours)
		{
			return SubstractEda((int)(2 * hours)) && SubstractSon((int)(6 * hours));
		}


        //Получить строку с текущим временем в формате: текущий день | часы:минуты
        public string GetTimeString()
        {
            // DayOfYear можно использовать для счета кол-ва возможных дней без учета кол-ва месяцев
            return $"{CurrentTime.Days} день | {CurrentTime.Hours}:{CurrentTime.Minutes}";
        }
		public void Sleep()
		{
			//if (SpendResourcesByHours(8))
			if (SubstractEda(3 * 8))
			{
				AddSon(1000);
				CurrentTime = CurrentTime.Add(TimeSpan.FromHours(8));
				Notify?.Invoke("Вы поспали =)" +
					"\nЗапас жизненных сил пополен до 100 \nПрошло 8 часов");
			}
		}

		public void Eat()
		{
			if (SpendResourcesByHours(0.5))
			{
				string messageText = "Вы поели \n+50 к сытости \n-10 ко сну  \n-50 денег \nПрошло 30 минут";
				if (SubstractSon(10))
                {
					if (SubstractMoney(50))
                    {
						AddEda(50);
						CurrentTime = CurrentTime.Add(TimeSpan.FromMinutes(30));
					}
				}
				Notify?.Invoke(messageText);
			}
		}

		//Методы добавдение и вычитание  характеристик игрока
		/// <summary>
		/// Вычитание денег
		/// </summary>
		/// <param name="money"></param>
		/// <returns></returns>
		public bool SubstractMoney(int money)
		{
			if (money < 0)
				return false;

			if (Money - money < 0)
			{
				Notify?.Invoke("У вас слишком мало денег");
				return false;
			}
			else
			{
				Money -= money;
				return true;
			}
		}
		/// <summary>
		/// Вычитание сна
		/// </summary>
		/// <param name="son"></param>
		/// <returns></returns>
		public bool SubstractSon(int son)
		{
			if (son < 0)
				return false;

			if (Son - son < 0)
			{
				Notify?.Invoke("Вы слишком устали");
				return false;
			}
			else
			{
				Son -= son;
				return true;
			}
		}
		// public только из-за юнит-тестов
		/// <summary>
		/// Вычитание еды
		/// </summary>
		/// <param name="eda"></param>
		/// <returns></returns>
		public bool SubstractEda(int eda)
		{
			if (eda < 0)
				return false;

			if (Eda - eda < 0)
			{
				Notify?.Invoke("Вы слишком голодны");
				return false;
			}
			else
			{
				Eda -= eda;
				return true;
			}
		}
		//Добавления сна
		private void AddSon(int son)
		{
			if (Son + son >= 100)
				Son = 100;
			else
				Son += son;
		}
		public void AddMoney(int money) 
		{
			Money += money;
		}

		//Добавление Еды
		private void AddEda(int eda)
		{
			if (Eda + eda >= 100)
				Eda = 100;
			else
				Eda += eda;
		}
	}
}
