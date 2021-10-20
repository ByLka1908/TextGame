using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOMJ_Simulator.BusinessLogic
{
    public class Rank
    {
		public int Id;
		public string Name;

		public Rank(string name)
		{
			Name = name;

			switch (name)
			{
				case "Среднее общие": Id = 1; break;
				case "Средние профиссиональное": Id = 2; break;
				case "Высшее образование(бакалавриат)": Id = 3; break;
				case "Высшее образование(магистратура)": Id = 4; break;
				case "Высшее(высшей квалификации)": Id = 5; break;
				default: throw new ArgumentException($"Нет ранга {name}");
			}
		}
		public static string GetRankNameById(int id)
		{
			switch (id)
			{
				case 1: return "Среднее общие";
				case 2: return "Средние профиссиональное";
				case 3: return "Высшее образование(бакалавриат)";
				case 4: return "Высшее образование(магистратура)";
				case 5: return "Высшее(высшей квалификации)";
				default: throw new ArgumentException($"Нет ранга с id {id}");
			}
		}
	}
}
