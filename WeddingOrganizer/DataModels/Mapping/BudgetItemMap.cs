using System;
using System.Collections.Generic;
using System.Text;
using DataModels.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataModels.Mapping
{
    public class BudgetItemMap : IEntityTypeConfiguration<BudgetItem>
    {
        public void Configure(EntityTypeBuilder<BudgetItem> builder)
        {
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.Budget).IsRequired();
        }
    }
}
