using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOMJ_Simulator.GameWindows;

namespace BOMJ_Simulator.BusinessLogic
{
    public class Game
    {
        // Сколько игровых часов проходит за одну реальную минуту
        public int GameHoursPerOneRealMinute { get; set; }

        // Текст на дисплее
        public string DisplayText { get; set; }

        private bool breakCurrentTime;

        private Person person;

        private StartWindows startWindow;

        public void SetPersonAndHomeWindow(StartWindows startWindow, Person person)
        {
            this.startWindow = startWindow;
            this.person = person;
        }
        // Метод, который прекращает течение текущего времени
        public void BreakCurrentTime()
        {
            breakCurrentTime = true;
        }
		// Течение игрового времени
		public async void ProcessTime()
		{

			startWindow.tblTime.Text = person.GetTimeString();
			int i = 0;

			while (true)
			{
				if (startWindow.IsActive)
				{
					if (breakCurrentTime)
					{
						breakCurrentTime = false;
						break;
					}

					person.CurrentTime = person.CurrentTime.Add(TimeSpan.FromMinutes(this.GameHoursPerOneRealMinute));
					startWindow.tblTime.Text = person.GetTimeString();
					i++;

					if (i == (60 / this.GameHoursPerOneRealMinute))
					{
						if (person.Eda < 3 || person.Son < 10)
						{
							// Поражение
							LosingWindows losingWindow = new LosingWindows(startWindow);

							WPF_Misc.FocusWindow(losingWindow);
							WPF_Misc.OpenPauseWindow(startWindow, losingWindow, false);
						}

						person.Eda -= 3;
						person.Son -= 10;
						i = 0;
					}
					if (person.CurrentTime.Days == 30)
                    {
						LosingWindows losingWindows = new LosingWindows(startWindow);
						WPF_Misc.FocusWindow(losingWindows);
						WPF_Misc.OpenPauseWindow(startWindow, losingWindows, false);
					}

					RefreshCharacteristics();
					RefreshGame();

					await Task.Delay(1000);
				}
				else
				{
					await Task.Delay(1000);
				}
			}
		}
		public void RefreshCharacteristics()
		{
			startWindow.tblEda.Text = person.Eda.ToString();

			startWindow.tblSon.Text = person.Son.ToString();
			startWindow.tblMoney.Text = person.Money.ToString();
			startWindow.tblRank.Text = person.Rank.Name;

			startWindow.tblTime.Text = person.GetTimeString();
			if (person.Money >= 1500)
            {
				WinWindows winWindows = new WinWindows(startWindow);
				WPF_Misc.FocusWindow(winWindows);
			} 
		}
		public void RefreshGame()
		{
			if (person.Money >= 30000)
            {
				WinWindows winWindows = new WinWindows(startWindow);
				WPF_Misc.FocusWindow(winWindows);
				WPF_Misc.OpenPauseWindow(startWindow, winWindows, false);
			}
		}
	}
}
