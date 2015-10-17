using System;
using System.Xml.Serialization;
//using System.Web.Http;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Persistence
{
	public class Persister<T>
	{
		private string _dataFileName;
		public string dataFileName
		{
			get { return (_dataFileName); }
			set { _dataFileName = value;  }
		}

		private T _objToPersist;
		public T objToPersist
		{
			get { return (_objToPersist); }
			set { _objToPersist = value;  }
		}

		public Persister(string dataFileName)
		{
			this._dataFileName = Path.Combine(Application.persistentDataPath,dataFileName);
			Debug.Log(this._dataFileName);
				//AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + dataFileName ;
			//this._objList = Activator.CreateInstance<List<T>>();
		}

		public void save()
		{
			lock (_dataFileName)
			{
				using (FileStream writer = File.Create(_dataFileName))
				{
					Debug.Log (_objToPersist.GetType ());
					XmlSerializer serializer = new XmlSerializer(_objToPersist.GetType());
					serializer.Serialize(writer, _objToPersist);
					writer.Close();
				}
			}
		}

		public void load()
		{
			if (File.Exists(_dataFileName))
			{
				lock (_dataFileName)
				{
					using (FileStream reader = File.Open(_dataFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						Debug.Log (typeof(List<T>));
						XmlSerializer serializer = new XmlSerializer(typeof(T));
						_objToPersist = (T)serializer.Deserialize(reader);
						reader.Close();
					}

				}
			}
			else
			{

			}
		}
	}
}

