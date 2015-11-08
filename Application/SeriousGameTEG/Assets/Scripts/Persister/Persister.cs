using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace Persistence
{
	public class Persister
	{
		public void Save<T>(string relativeFilePath, T objToPersist)
		{
			lock (relativeFilePath)
			{
				relativeFilePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + relativeFilePath;
				using (FileStream writer = File.Create(relativeFilePath))
				{
					Debug.Log (objToPersist.GetType ());
					XmlSerializer serializer = new XmlSerializer(objToPersist.GetType());
					serializer.Serialize(writer, objToPersist);
					writer.Close();
				}
				Debug.Log (relativeFilePath);
			}
		}

		public T Load <T>(string relativeFilePath)
		{
			T objToPersist = default(T);
			relativeFilePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + relativeFilePath;
			if (File.Exists(relativeFilePath))
			{
				lock (relativeFilePath)
				{
					using (FileStream reader = File.Open(relativeFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
					{
						XmlSerializer serializer = new XmlSerializer(typeof(T));
						objToPersist = (T)serializer.Deserialize(reader);
						reader.Close();
					}
				}
			}
			else
			{
				Debug.Log ("Trying to load "+relativeFilePath+ " but the file does not exist");
			}
			return objToPersist;
		}
	}
}

