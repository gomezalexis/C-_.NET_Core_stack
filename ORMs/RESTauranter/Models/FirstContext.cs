using Microsoft.EntityFrameworkCore;

namespace RESTauranter.Models
{
    public class FirstContext : DbContext
    {
        public FirstContext(DbContextOptions<FirstContext> options) : base(options){}

        public DbSet<Review> Reviews {get; set;}

    }
}