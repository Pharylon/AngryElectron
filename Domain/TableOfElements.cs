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
    public sealed class TableOfElements : List<Element>
    {
        //These two lines combine to make the TableOfElements a lazily-constructed Singleton and avoid the expensive process
        //of calling the constructor over and over. Or at least, what will be expensive once it's reading
        //from a database instead of a json. Info here: http://csharpindepth.com/Articles/General/Singleton.aspx
        private static readonly Lazy<TableOfElements> lazy = new Lazy<TableOfElements>(() => new TableOfElements());
        public static TableOfElements Instance { get { return lazy.Value; } }

        string path = AppDomain.CurrentDomain.BaseDirectory + @"\App_Data\TableOfElements.json";

        private TableOfElements()
        {
            InitializeTableOfElements(path);
        }

        public string Json 
        { 
            get
            {
                
                StreamReader streamReader = new StreamReader(path);
                return streamReader.ReadToEnd().ToString();
            }
        }

        private void InitializeTableOfElements(string path)
        {
            this.AddRange(JsonConvert.DeserializeObject<List<Element>>(Json));
        }
        
        public void Save()
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(this));
        }
        public string getFilePath()
        {
            throw new NotImplementedException();
        }
    }
}
