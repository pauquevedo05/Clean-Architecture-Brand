using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using ISII.Web.Domain.BrandAggregate;
using ISII.Web.Extensions;

namespace ISII.Web.BrandFeatures.GetById;

public sealed class GetBrandByIdRequest
{
  public const string Route = "/Brands/{BrandId}";
  public int BrandId { get; init; }
}

public class GetBrandByIdEndpoint(IMediator mediator)
  : Endpoint<GetBrandByIdRequest,
             Results<Ok<BrandRecord>,
                     NotFound,
                     ProblemHttpResult>,
             GetBrandByIdMapper>
{
  public override void Configure()
  {
    Get(GetBrandByIdRequest.Route);   
    AllowAnonymous();

    Summary(s =>
    {
      s.Summary = "Get a brand by ID";
      s.Description = "Retrieves a specific brand by its unique identifier.";
      s.ExampleRequest = new GetBrandByIdRequest { BrandId = 1 };
      s.ResponseExamples[200] = new BrandRecord(1, "Acme", "Argentina", 1990);

      s.Responses[200] = "Brand found and returned successfully";
      s.Responses[404] = "Brand with specified ID not found";
    });

    Tags("Brands");

    Description(builder => builder
      .Accepts<GetBrandByIdRequest>()
      .Produces<BrandRecord>(200, "application/json")
      .ProducesProblem(404));
  }

  public override async Task<Results<Ok<BrandRecord>, NotFound, ProblemHttpResult>>
    ExecuteAsync(GetBrandByIdRequest request, CancellationToken ct)
  {
   
    var result = await mediator.Send(new GetBrandQuery(BrandId.From(request.BrandId)), ct);

    return result.ToGetByIdResult(Map.FromEntity);
  }
}


public sealed class GetBrandByIdValidator : Validator<GetBrandByIdRequest>
{
  public GetBrandByIdValidator()
  {
    RuleFor(x => x.BrandId)
      .GreaterThan(0)
      .WithMessage("El id de la marca debe ser mayor a 0");
  }
}


public sealed class GetBrandByIdMapper
  : Mapper<GetBrandByIdRequest, BrandRecord, BrandDto>
{
  public override BrandRecord FromEntity(BrandDto e)
    => new(e.Id.Value, e.Name, e.Country, e.FoundedYear);
}