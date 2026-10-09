using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ISII.Web.Domain.BrandAggregate;

namespace ISII.Web.Infrastructure.Data.Config;
public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
  public void Configure(EntityTypeBuilder<Brand> builder)
  {
    
    builder.Property(entity => entity.Id)
      .HasValueGenerator<VogenIntIdValueGenerator<AppDbContext, Brand, BrandId>>()
      .HasVogenConversion()
      .IsRequired();

   
    builder.Property(entity => entity.Name)
      .HasMaxLength(Brand.NameMaxLength)
      .IsRequired();

    
    builder.Property(entity => entity.Country)
      .HasMaxLength(100)
      .IsRequired();

    
    builder.Property(entity => entity.FoundedYear)
      .IsRequired();

    
    builder.HasData(
      new Brand(BrandId.From(1), "Acme", "Argentina", 1990),
      new Brand(BrandId.From(2), "Globex", "Brasil", 2005));
  }
}