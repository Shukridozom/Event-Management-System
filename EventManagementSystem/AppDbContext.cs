using Microsoft.EntityFrameworkCore;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace EventManagementSystem
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
    }
}
