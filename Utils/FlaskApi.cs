using AGB_Bank.Migrations;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text.Json;


namespace AGB_Bank.Utils
{
    public class FlaskApi
    {
        private readonly HttpClient _client;

        public FlaskApi()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5000"); // Adresse de votre API Flask
        }
        public async Task<AGB_Bank.Controllers.PythonController.Prediction> GetApiDataAsync()
        {
            // end point 

            HttpResponseMessage response = await _client.GetAsync("/api/utilisateurs");
            

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();

                // Désérialiser le contenu JSON dans un objet C# avec System.Text.Json
                AGB_Bank.Controllers.PythonController.Prediction prediction = JsonSerializer.Deserialize<AGB_Bank.Controllers.PythonController.Prediction>(jsonContent);

                return prediction;
            }
            
            else
            {
                //return "Error";
                AGB_Bank.Controllers.PythonController.Prediction prediction = new AGB_Bank.Controllers.PythonController.Prediction()
                {
                    predictionCarte = 1,
                    predictionCredit = [1, 1],
                    predictionPack = 20,
                };
               
                return prediction;
            }
            
        }
        // Accès aux propriétés de l'utilisateur actuel
        public async Task<string> SetApiDataAsync(int typeClient, int EtatCivil, int Gender ,int codePostal, int agence, int gl, float Revenu, int age)
        {
            var data = new {
                typeClient = typeClient,
                EtatCivil = EtatCivil,
                codePostal = codePostal,
                agence = agence,
                gl = gl,
                Revenu = Revenu,
                age = age,
                gender = Gender,
            };
            HttpResponseMessage response = await _client.PostAsJsonAsync("/api/data", data);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Error";
            }
        }
    }
}
