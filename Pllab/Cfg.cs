using Microsoft.Extensions.Configuration;

namespace Pllab
{
    public class Cfg
    {
        private readonly IConfiguration _configuration;

        public Cfg(string filePath)
        {
            _configuration = new ConfigurationBuilder()
               .AddJsonFile(filePath)
               .Build();
        }

        public string GetMessage()
        {
            return _configuration["Message"];
        }
    }
}
