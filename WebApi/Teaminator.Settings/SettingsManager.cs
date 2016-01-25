using System.IO;
using Newtonsoft.Json;

namespace Teaminator.Settings
{
    public class SettingsManager
    {
        private static string _filename = "settings.json";

        public static Domain.Models.Settings Settings { get; set; }

        public static void Init()
        {
            Settings = new Domain.Models.Settings();
            if(File.Exists(_filename))Load();
            else Save();
        }
        public static void Load()
        {
            var sr = new StreamReader(new FileStream(_filename, FileMode.Open), System.Text.Encoding.UTF8);
            var json = sr.ReadToEnd();
            sr.Close();

            Settings = JsonConvert.DeserializeObject<Domain.Models.Settings>(json);
        }
        public static void Save()
        {
            var data = JsonConvert.SerializeObject(Settings);
            var sw = new StreamWriter(_filename, false);
            sw.Write(data);
            sw.Close();
        }

    }

}
