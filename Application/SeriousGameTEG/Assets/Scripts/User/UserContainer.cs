using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Persistence;

namespace Persistence
{
	[XmlRoot("UserCollection")]
	public class UserContainer
	{
		/*Path location to User Resources*/
		public static string FILE_NAME = "Users.xml";

		[XmlArray("Users")]
		[XmlArrayItem("User")]
		public List<User> Users = new List<User>();

		public UserContainer () {}
	}
}

