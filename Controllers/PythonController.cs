using Microsoft.AspNetCore.Mvc;
using AGB_Bank.Data;
using Microsoft.AspNetCore.Identity;
using AGB_Bank.Utils;
using AGB_Bank.Models;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using AGB_Bank.Models.product;



namespace AGB_Bank.Controllers
{
    public class PythonController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // pour la base de données
        private readonly AppDbContext _context;
        // pour les users
        private readonly UserManager<AppUser> _userManager;

        public class Prediction
        {
            public int predictionCarte { get; set; }
            public int[]? predictionCredit { get; set; }
            public int predictionPack { get; set; }
        }


        private Prediction _cachedPrediction;

        public PythonController(AppDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Prediction> GetData()
        {
            FlaskApi client = new FlaskApi();
            Prediction prediction = await client.GetApiDataAsync();

            return prediction;
        }

        public static Dictionary<string, int> agences = new Dictionary<string, int>
        {
            {"DELY IBRAHIM", 1}, {"EL MOURADIA", 2}, {"LES SOURCES", 3}, {"BABA HASSEN", 4},
            {"BLIDA", 5}, {"ZERALDA", 6}, {"Les Vergers", 7}, {"BAINEM", 8}, {"BAB EL OUED", 9},
            {"ROUIBA", 10}, {"BIRTOUTA", 11}, {"BORDJ EL BAHRI", 12}, {"BOIS DES CARS", 13},
            {"KOUBA", 14}, {"DIDOUCHE MOURAD", 15}, {"TIZI OUZOU", 120}, {"BOUIRA", 17},
            {"DJELFA", 18}, {"LAGHOUAT", 19}, {"GHARDAIA", 20}, {"MEDEA", 21}, {"KOLEA", 22},
            {"BELOUIZDED", 23}, {"BARAKI", 24}, {"BOUMERDES", 25}, {"Blida Ben Boulaid", 26},
            {"SIDI YAHHIA", 28}, {"EL BIAR", 30}, {"SETIF", 100}, {"SKIKDA", 101}, {"ANNABA", 102},
            {"BATNA", 103}, {"BISKRA", 104}, {"BORDJ BOU ARRERIDJ", 105}, {"EL EULMA", 106},
            {"BEJAIA", 107}, {"AKBOU", 108}, {"CONSTANTINE", 109}, {"AIN M LILA", 110},
            {"TEBESSA", 111}, {"Annaba Seddik Ben YAHIA", 112}, {"SKIKDA 2", 113}, {"GUELMA", 114},
            {"El Oued", 115}, {"JIJEL", 116}, {"MSILA", 117}, {"KHENCHELA", 118}, {"Constantine2", 119},
            {"AZAZGA", 121}, {"Hassi Messaoud", 122}, {"Oran Sidi Chahmi", 200}, {"CHLEF", 201},
            {"TLEMCEN", 202}, {"SIDI BEL ABBES", 203}, {"MOSTAGANEM", 204}, {"Oran Seddikia", 205},
            {"MASCARA", 206}, {"TIARET", 207}, {"SAIDA", 208}, {"AIN TEMOUCHENT", 209}, {"AIN DEFLA", 210},
            {"RELIZANE", 211}, {"ADRAR", 212}, {"Chlef El Mokrani", 213}, {"BECHAR", 214}, {"SAIDA ARCHIVE", 215},
            {"OUARGLA", 300}
        };

        // Création du dictionnaire
        public static Dictionary<string, int> gl = new Dictionary<string, int>
        {
            { "Comptes Courants Dinars", 2500 },
            { "Comptes de Chèques clientèle", 2504 },
            { "Comptes de chèques personnel AGB", 2505 },
            { "Comptes des professions libérales", 2506 },
            { "Comptes courants commercants", 2507 },
            { "Comptes Chèques finance Islamique", 2514 },
            { "Livret d'épargne et d'investissement F.Islamique", 2574 },
            { "Compte épargne non rémunéré", 2576 },
            { "Livret d’épargne Classique", 2599 }
        };


        private static int ConvertGlToInt(string gl_string)
        {
            if (gl.TryGetValue(gl_string, out int code))
            {
                return code;
            }
            else
            {
                throw new Exception("GL non trouvée");
            }
        }

        private static int ConvertAgenceToInt(string agence)
        {
            if (agences.TryGetValue(agence, out int code))
            {
                return code;
            }
            else
            {
                throw new Exception("Agence non trouvée");
            }
        }
        private int ConvertTypeClientToInt(string typeClient)
        {
            switch (typeClient)
            {
                case "particulier":
                    return 1;
                case "professionel":
                    return 2;
                default:
                    return 3;
            }
        }
        private int GetEtatCivil(string etatCivil)
        {
            switch (etatCivil)
            {
                case "celibataire":
                    return 0;
                case "marie(e)":
                    return 1;
                //case "divorce(e)":
                //    return 3;
                //case "veuf(ve)":
                //    return 4;
                default:
                    return 0;
            }
        }
        private int GetGender(string gender)
        {
            switch (gender)
            {
                case "Homme":
                    return 1;
                case "Femme":
                    return 0;
                default:
                    return 1;
            }
        }
        public async Task SetData()
        //public async Task<ActionResult> SetData()
        {
            FlaskApi client = new FlaskApi();
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                // Accès aux propriétés de l'utilisateur actuel
                var typeClient = ConvertTypeClientToInt(user.typeClient);
                var EtatCivil = GetEtatCivil(user.EtatCivil);
                var Gender = GetGender(user.Gender);
                var codePostal = (int)user.codePostal;
                var Revenu = (float)user.Revenu;
                var DateNaissance = user.dateNaissance;
                DateTime dateActuelle = DateTime.Today;
                int age = dateActuelle.Year - DateNaissance.Value.Year;
                int codeAgence = (int)ConvertAgenceToInt(user.agence);
                int codeGL = (int)ConvertGlToInt(user.gl);
                // et ainsi de suite pour d'autres propriétés
                string response = await client.SetApiDataAsync(typeClient,
                    EtatCivil, Gender, codePostal, codeAgence, codeGL, Revenu, age);
            }

        }

        public async Task<IActionResult> Run()
        {
            ISession session = _httpContextAccessor.HttpContext.Session;
            session.Clear();
            await SetData();
            return RedirectToAction("index", "home");
        }



        public async Task<IActionResult> Products()
        {
            ISession session = _httpContextAccessor.HttpContext.Session;

            if (session.GetInt32("carte_id") == null || session.GetString("credit_ids") == null || session.GetInt32("pack_id") == null)
            {
                _cachedPrediction = await GetData();

                // Mettre les données en cache dans la session avec une expiration après 10 minutes
                session.SetInt32("carte_id", _cachedPrediction.predictionCarte);
                session.SetInt32("pack_id", _cachedPrediction.predictionPack);

                // Cache des prédictions de crédit
                session.SetString("credit_ids", JsonConvert.SerializeObject(_cachedPrediction.predictionCredit));

                var product1 = await _context.carteProduct.FirstOrDefaultAsync(p => p.Id == _cachedPrediction.predictionCarte);
                var product2 = await _context.packProduct.FirstOrDefaultAsync(p => p.Id == _cachedPrediction.predictionPack);

                // Récupérer tous les produits de crédit
                List<credit_product> creditProducts = new List<credit_product>();
                foreach (var creditId in _cachedPrediction.predictionCredit)
                {
                    var product = await _context.creditProduct.FirstOrDefaultAsync(p => p.Id == creditId);
                    if (product != null)
                    {
                        creditProducts.Add(product);
                    }
                }

                // Vérifier si les produits existent
                if (product1 != null & product2 != null & creditProducts.Any())
                {
                    var data = new
                    {
                        product1,
                        product2,
                        creditProducts
                    };
                    string json = JsonConvert.SerializeObject(data);
                    return Content(json, "application/json");
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                var product1 = await _context.carteProduct.FirstOrDefaultAsync(p => p.Id == session.GetInt32("carte_id"));
                var product2 = await _context.packProduct.FirstOrDefaultAsync(p => p.Id == session.GetInt32("pack_id"));

                // Récupérer les IDs des prédictions de crédit à partir de la session
                var creditIdsJson = session.GetString("credit_ids");
                var creditIds = JsonConvert.DeserializeObject<List<int>>(creditIdsJson);

                // Récupérer tous les produits de crédit
                List<credit_product> creditProducts = new List<credit_product>();
                foreach (var creditId in creditIds)
                {
                    var product = await _context.creditProduct.FirstOrDefaultAsync(p => p.Id == creditId);
                    if (product != null)
                    {
                        creditProducts.Add(product);
                    }
                }

                // Vérifier si les produits existent
                if (product1 != null & product2 != null & creditProducts.Any())
                {
                    var data = new
                    {
                        product1,
                        product2,
                        creditProducts
                    };
                    string json = JsonConvert.SerializeObject(data);
                    return Content(json, "application/json");
                }
                else
                {
                    return NotFound();
                }
            }
        }


    }
}