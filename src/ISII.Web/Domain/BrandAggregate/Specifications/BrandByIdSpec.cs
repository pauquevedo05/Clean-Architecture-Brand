namespace ISII.Web.Domain.BrandAggregate.Specifications;

public class BrandByIdSpec : Specification<Brand>
{
  public BrandByIdSpec(BrandId brandId) =>
    Query.Where(brand => brand.Id == brandId);
}