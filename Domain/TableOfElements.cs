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
        private string _tableOfElementsFilePath = @"C:\Misc\TableOfElements.json";
        //Path _tableOfElementsPath = Path.GetDirectoryName("hello");
        public TableOfElements()
        {
            initializeTableOfElements(_tableOfElementsFilePath);
        }

        public TableOfElements(string path)
        {
            initializeTableOfElements(path);
        }

        public string Json 
        { 
            get
            {
                
                StreamReader streamReader = new StreamReader(_tableOfElementsFilePath);
                return streamReader.ReadToEnd().ToString();
            }
        }

        private void initializeTableOfElements(string path)
        {
            this.AddRange(JsonConvert.DeserializeObject<List<Element>>(Json));
        }
        
        public void Save()
        {
            File.WriteAllText(_tableOfElementsFilePath, JsonConvert.SerializeObject(this));
        }
        public string getFilePath()
        {
            throw new NotImplementedException();
        }
    }
}
