namespace HistoriaApi.DataBase
{
    public class GeniosDataBase
    {

        public string DatabaseConnection()
        {
            var json = File.ReadAllText("appsettings.json");
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json)!;
            string connectionString = jsonObj!["ConnectionStrings"]["DatabaseConnection"];

            return connectionString;
        }

        public List<genios> Show()
    }
}
