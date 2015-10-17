using System;
using persistence;
using System.Collections.Generic;

public class main {

	public static void Main (){

		if(!true){
			//save flow
			UserContainer uc = new UserContainer (2);
			Persister<UserContainer> usersPersister = new Persister<UserContainer> (User.LOCATION_PATH);
			usersPersister.objToPersist = uc;
			usersPersister.save ();
		}else{
			//load flow
			Persister<UserContainer> usersPersister = new Persister<UserContainer> (User.LOCATION_PATH);
			usersPersister.load ();
			foreach(User current in usersPersister.objToPersist.Users){
				Console.WriteLine (current.Name);
				Console.WriteLine (current.BirthDay);
			}
		}



	}

}