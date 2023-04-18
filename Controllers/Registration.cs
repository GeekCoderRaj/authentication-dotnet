using Microsoft.AspNetCore.Mvc;
using LoginRegistrationApp.Models;
using MySql.Data.MySqlClient;
namespace LoginRegistrationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }      

        [HttpPost]
        [Route("registration")]
        public IEnumerable<object> registration(Registration registration)
        {
            List<object> data = new List<object>();
            MySqlConnection con = new MySqlConnection(_configuration.GetConnectionString("Test").ToString());
            MySqlCommand cmd = new MySqlCommand("Select * From StudentTable",con);
            con.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
       
             while (reader.Read())
                {
                    object row = new
                    {
                        userName = reader.GetString(0),
                        email = reader.GetString(1),
                        Password = reader.GetString(2)   
                    };
                    data.Add(row);
                }
                  con.Close();   
            return data;
        }
    }
}