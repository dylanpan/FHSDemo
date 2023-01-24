using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class ConfigUtil
{
    // dotnet add package Newtonsoft.Json --version 13.0.2
    public static List<Dictionary<string, string>> GetPiecesConfig()
    {
        List<Dictionary<string, string>> piecesConfigList = new List<Dictionary<string, string>>();
        string fileName = "Config/PiecesConfig.json";
        using (StreamReader file = File.OpenText(fileName))
        {
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray configArray = (JArray)JToken.ReadFrom(reader);
                for (int i = 0; i < configArray.Count; i++)
                {
                    JObject config = JObject.Parse(configArray[i].ToString());
                    Dictionary<string, string> piecesConfig = new Dictionary<string, string>();
                    piecesConfig.Add("name", config["name"].ToString());
                    piecesConfig.Add("id", config["id"].ToString());
                    piecesConfig.Add("skin_name", config["skin_name"].ToString());
                    piecesConfig.Add("current_level", config["current_level"].ToString());
                    piecesConfig.Add("piece_cost", config["piece_cost"].ToString());
                    piecesConfig.Add("piece_recycle", config["piece_recycle"].ToString());
                    piecesConfig.Add("race", config["race"].ToString());
                    piecesConfig.Add("atk", config["atk"].ToString());
                    piecesConfig.Add("hp", config["hp"].ToString());
                    piecesConfigList.Add(piecesConfig);
                }
            }
        }
        return piecesConfigList;
    }

    public static List<Dictionary<string, string>> GetHeroesConfig()
    {
        List<Dictionary<string, string>> heroesConfigList = new List<Dictionary<string, string>>();
        string fileName = "Config/HeroesConfig.json";
        using (StreamReader file = File.OpenText(fileName))
        {
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JArray configArray = (JArray)JToken.ReadFrom(reader);
                for (int i = 0; i < configArray.Count; i++)
                {
                    JObject config = JObject.Parse(configArray[i].ToString());
                    Dictionary<string, string> heroesConfig = new Dictionary<string, string>();
                    heroesConfig.Add("name", config["name"].ToString());
                    heroesConfig.Add("id", config["id"].ToString());
                    heroesConfig.Add("skin_name", config["skin_name"].ToString());
                    heroesConfig.Add("skill_id", config["skill_id"].ToString());
                    heroesConfig.Add("atk", config["atk"].ToString());
                    heroesConfig.Add("hp", config["hp"].ToString());
                    heroesConfigList.Add(heroesConfig);
                }
            }
        }
        return heroesConfigList;
    }
}