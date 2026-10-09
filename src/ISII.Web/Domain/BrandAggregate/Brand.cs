using Ardalis.GuardClauses;

namespace ISII.Web.Domain.BrandAggregate;

public class Brand : EntityBase<Brand, BrandId>, IAggregateRoot
{
  public const int NameMaxLength = 100;
  public const int MinFoundedYear = 1800;

   private Brand() { }

   public Brand(BrandId id, string name, string country, int foundedYear)
  {
    Id = id;
    Name = ValidateName(name);
    Country = Guard.Against.NullOrWhiteSpace(country, nameof(country));
    FoundedYear = Guard.Against.OutOfRange(
      foundedYear, nameof(foundedYear), MinFoundedYear, DateTime.UtcNow.Year);
  }

  public static Brand Create(BrandId id, string name, string country, int foundedYear)
    => new Brand(id, name, country, foundedYear);

  public string Name { get; private set; } = string.Empty;
  public string Country { get; private set; } = string.Empty;
  public int FoundedYear { get; private set; }

  private static string ValidateName(string name)
  {
    Guard.Against.NullOrWhiteSpace(name, nameof(name));
    if (name.Length > NameMaxLength)
      throw new ArgumentException($"Name must not exceed {NameMaxLength} characters.", nameof(name));
    return name;
  }
}