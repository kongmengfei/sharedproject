/*****************************************************************************
 * JsonHelper.cs
 * WCF REST service hosted in SharePoint 2013
 * 
 * Copyright (c) Jason Barkes.
 * All rights reserved.
 * http://jbarkes.blogspot.com
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *****************************************************************************/

using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace UnitTests
{
    public class JsonHelper
    {
        public static string Serialize<T>(T obj)
        {
            MemoryStream stream = new MemoryStream();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        public static T Deserialize<T>(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return default(T);
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(data));
            return (T)serializer.ReadObject(stream);
        }
    }
}
