using System;
using System.Collections.Generic;
using System.Text;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Email).IsRequired();
            builder.Property(p => p.FirstName).IsRequired();
            builder.Property(p => p.Password).IsRequired();
        }
    }
}
