using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using persistence;

namespace persistence
{
	[XmlRoot("UserCollection")]
	public class UserContainer
	{

		[XmlArray("Users")]
		[XmlArrayItem("User")]
		public List<User> Users = new List<User>();


		public UserContainer (int cantUsers)
		{
			Random rand = new Random (10);

			for ( int i = 0; i < cantUsers; i++) {
				Users.Add (new User (rand.Next()));
			}
		}

		public UserContainer () {}
	}
}

