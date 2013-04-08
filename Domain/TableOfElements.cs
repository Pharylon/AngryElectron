using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace AngryElectron.Domain
{
    public class TableOfElements : List<Element>
    {
        private const string _tableOfElementsFilePath = @"Data\TableOfElements.json";
        public TableOfElements()
        {
                initializeTableOfElements();
        }
        public string Json 
        { 
            get
            {
                StreamReader streamReader = new StreamReader(_tableOfElementsFilePath);
                return streamReader.ReadToEnd().ToString();
            }
        }
        private void initializeTableOfElements()
        {
            StreamReader streamReader = new StreamReader(_tableOfElementsFilePath);
            this.AddRange(JsonConvert.DeserializeObject<List<Element>>(Json));
            streamReader.Close();
        }
        
        public void Save()
        {
            File.WriteAllText(_tableOfElementsFilePath, JsonConvert.SerializeObject(this));
        }
    }
}
