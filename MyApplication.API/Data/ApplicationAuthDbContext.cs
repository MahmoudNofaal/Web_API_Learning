using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyApplication.API.Data;

public class ApplicationAuthDbContext : IdentityDbContext
{

   public ApplicationAuthDbContext(DbContextOptions<ApplicationAuthDbContext> options) : base(options)
   {

   }

   protected override void OnModelCreating(ModelBuilder builder)
   {
      base.OnModelCreating(builder);

      var readerRoleId = "b1a1f6e8-3c4d-4e5f-9a6b-7c8d9e0f1a2b";
      var writerRoleId = "c2b2f7e9-4d5e-5f6g-0h7i-8j9k0l1m2n3o";

      var roles = new List<IdentityRole>
      {
         new IdentityRole
         {
            Id = readerRoleId,
            ConcurrencyStamp = readerRoleId,
            Name = "Reader",
            NormalizedName = "READER"
         },
         new IdentityRole
         {
            Id = writerRoleId,
            ConcurrencyStamp = writerRoleId,
            Name = "Writer",
            NormalizedName = "WRITER"
         }
      };

      builder.Entity<IdentityRole>().HasData(roles);


   }

}
