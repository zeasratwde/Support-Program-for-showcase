using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractImage
{
	public class State
	{
		public string name { get; set; }
		public string abbreviation { get; set; }
	}

	class LocalFile
	{
		List<State> listOfStates;
		public LocalFile()
		{
			using (StreamReader j = new StreamReader(@"C:\Users\Asrat\Desktop\StatesData\states_titlecase.json"))
			{
				string json = j.ReadToEnd();

				listOfStates = JsonConvert.DeserializeObject<List<State>>(json);
			}
		}



		/// <summary>
		///  takes a string that is a remote file name and  
		///  converts it to a string that looks like stateInitial_falg.jpg 
		///  example 'MA_flag.jpg' at the end of the list it returns 
		///  the string 'End'
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string CreateLocalFileName(string rFileName)
		{

			int size = listOfStates.Count;
			int count = 0;
			while (count < size)
			{
				if (rFileName.ToUpper().Contains(listOfStates[count].name.ToUpper()))
				{
					return listOfStates[count].abbreviation + "_flag.jpg";
				}
				count++;
			}
			return "End";
		}



	}
}

