using System;
using System.Xml.Serialization;

namespace Persistence
{
	[XmlRoot("User")]
	public class User
	{

		private string name;
		[XmlElement("Name")]
		public string Name {
			get { return name; }
			set { name = value; }
		}

		private string lastName;
		[XmlElement("LastName")]
		public string LastName {
			get { return lastName; }
			set { lastName = value; }
		}

		private string birthDate;
		[XmlElement("BirthDay")]
		public string BirthDay {
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
			birthDate = DateTime.Now.ToString();
		}
	}
}

