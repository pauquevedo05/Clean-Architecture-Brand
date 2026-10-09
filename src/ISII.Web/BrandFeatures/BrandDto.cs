using ISII.Web.Domain.BrandAggregate;

namespace ISII.Web.BrandFeatures;

public record BrandDto(BrandId Id, string Name, string Country, int FoundedYear);