using Vogen;

namespace ISII.Web.Domain.BrandAggregate;

[ValueObject<int>]
public readonly partial struct BrandId
{
  public static BrandId New => From(0);

  private static Validation Validate(int value)
      => value >= 0 ? Validation.Ok : Validation.Invalid("BrandId must be non-negative.");
}