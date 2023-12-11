using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace NSTask.Models
{
    public class DataBaseInitializer
    {



        private readonly ILogger<DataBaseInitializer> _logger;
        private readonly NSTaskDataBase _context;


        public DataBaseInitializer(ILogger<DataBaseInitializer> logger, NSTaskDataBase context)
        {
            _logger = logger;
            _context = context;
        }


        public void Initialize()
        {
            try
            {
                _context.Database.Migrate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }
    }
}
