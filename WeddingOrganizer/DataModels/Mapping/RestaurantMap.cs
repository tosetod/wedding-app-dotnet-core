using System;
using System.Collections.Generic;
using System.Text;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Mapping
{
    public class RestaurantMap : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Details).IsRequired();
            builder.Property(p => p.Phone).IsRequired();
        }
    }
}
