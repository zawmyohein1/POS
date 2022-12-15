using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.EntityModels;
using System;

namespace POS.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasData(

                    new User
                    {
                        User_ID = 1,
                        User_Name = "admin",
                        Email = "admin@gmail.com",
                        Password = "yngWIE500",
                        Phone = "9484774",
                        Gender = 1,
                        Role = "",
                        Created = DateTime.Now.Date,
                        IsDeleted = false
                    },
                    new User
                    {
                        User_ID = 2,
                        User_Name = "user",
                        Email = "user@gmail.com",
                        Password = "yngWIE500",
                        Phone = "7575664",
                        Gender = 1,
                        Role = "",
                        Created = DateTime.Now.Date,
                        IsDeleted = false
                    }
                    );
        }
    }
}
