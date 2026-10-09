using ISII.Web.Domain.BrandAggregate;
using ISII.Web.Domain.BrandAggregate.Specifications;

namespace ISII.Web.BrandFeatures.GetById;

public record GetBrandQuery(BrandId BrandId) : IQuery<Result<BrandDto>>;

public class GetBrandHandler(IReadRepository<Brand> _repository)
  : IQueryHandler<GetBrandQuery, Result<BrandDto>>
{
  public async ValueTask<Result<BrandDto>> Handle(GetBrandQuery request, CancellationToken cancellationToken)
  {
    var spec = new BrandByIdSpec(request.BrandId);
    var entity = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

    if (entity == null) return Result.NotFound();

    return new BrandDto(entity.Id, entity.Name, entity.Country, entity.FoundedYear);
  }
}