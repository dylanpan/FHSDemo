using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Chess.Util
{
    public class ConfigUtil
    {
        // dotnet add package Newtonsoft.Json --version 13.0.2
        public static List<T> GetConfigData<T>(string fileName)
        {
            List<T> configList = new List<T>();
            using FileStream openStream = File.OpenRead(fileName);

            using (StreamReader file = File.OpenText(fileName))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JArray configArray = (JArray)JToken.ReadFrom(reader);
                    for (int i = 0; i < configArray.Count; i++)
                    {
                        string config = configArray[i].ToString();
                        T configData = JsonConvert.DeserializeObject<T>(config);
                        if (configData != null)
                        {
                            configList.Add(configData);
                        }
                    }
                }
            }
            return configList;
        }
    }
}