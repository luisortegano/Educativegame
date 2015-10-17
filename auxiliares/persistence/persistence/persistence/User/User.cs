using System;
using System.Xml.Serialization;

namespace persistence
{
	[XmlRoot("User")]
	public class User
	{
		/*Path location to User Resources*/
		public static string LOCATION_PATH = "Users/Users.txt";


		private string name;
		[XmlElement("Name")]
		public string Name {
			get { return name; }
			set { name = value; }
		}


		private DateTime birthDate;
		[XmlElement("BirthDay")]
		public DateTime BirthDay {
			get { return birthDate; }
			set { birthDate = value; }
		}

		private int userId;
		[XmlElement("UserId")]
		public int UserId {
			get { return userId; }
			set { userId = value; }
		}

		public User (){
			this.name = "test";
		}

		public User (int i){
			this.name = "test"+i.ToString();
			birthDate = DateTime.Now;
		}
	}
}

