using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace VI_Project_Lib
{
    public class Preset
    {
        public string PresetName { get; set; }

        public List<ROI> ROIs { get; set; }

        [JsonConstructor]
        public Preset(string Name, List<ROI> rois)
        {
            PresetName = string.Empty;
            ROIs = new List<ROI>();
            PresetName = Name;
            ROIs = rois;
        }

        public Preset(string Name = "PresetNo.XX")
        {
            PresetName = Name;
            ROIs = new List<ROI>();
        }

        public void SaveJson()
        {
            JavaScriptSerializer scriptor = new JavaScriptSerializer();
            string json = scriptor.Serialize(this);

            string path = $@"..\\..\\JSON\\{PresetName}.json";


            using (var tw = new StreamWriter(path, false))
            {
                tw.WriteLine(json);
                tw.Close();
            }
        }

        public static void LoadJson(List<Preset> presets)
        {
            var jsonfiles = Directory.GetFiles(@"..\\..\\JSON\\", "*.json");


            foreach (var json in jsonfiles)
            {
                StreamReader reader = new StreamReader(json);
                presets.Add(JsonConvert.DeserializeObject<Preset>(reader.ReadToEnd()));
                reader.Dispose();
            }
        }
    }
}
