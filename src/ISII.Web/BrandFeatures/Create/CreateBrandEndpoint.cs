using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using ISII.Web.Domain.BrandAggregate;
using ISII.Web.BrandFeatures;

namespace ISII.Web.BrandFeatures.Create;

public sealed class CreateBrandRequest
{
  public string Name { get; init; } = string.Empty;
  public string Country { get; init; } = string.Empty;
  public int FoundedYear { get; init; }
}

public class CreateBrandEndpoint(IRepository<Brand> repository) :
  Endpoint<CreateBrandRequest,
           Results<Created<BrandRecord>, ValidationProblem, ProblemHttpResult>>
{
  private readonly IRepository<Brand> _repository = repository;
   public override void Configure()
  {
    Post("/Brands");       
    AllowAnonymous(); 
    Summary(s =>
    {
      s.Summary = "Create a new brand";
      s.Description = "Creates a new brand with the specified name, country and founded year.";
      s.ExampleRequest = new CreateBrandRequest { Name = "Acme", Country = "Argentina", FoundedYear = 1990 };
      s.ResponseExamples[201] = new BrandRecord(1, "Acme", "Argentina", 1990);

      s.Responses[201] = "Brand created successfully";
      s.Responses[400] = "Invalid request data";
    });

    Tags("Brands");

     Description(builder => builder
      .Accepts<CreateBrandRequest>()
      .Produces<BrandRecord>(201, "application/json")
      .ProducesProblem(400));
  }
  public override async Task<Results<Created<BrandRecord>, ValidationProblem, ProblemHttpResult>>
    ExecuteAsync(CreateBrandRequest request, CancellationToken cancellationToken)
  {
    var brand = Brand.Create(BrandId.New, request.Name, request.Country, request.FoundedYear);
    await _repository.AddAsync(brand, cancellationToken);
    await _repository.SaveChangesAsync(cancellationToken);

     var response = new BrandRecord(brand.Id.Value, brand.Name, brand.Country, brand.FoundedYear);
    return TypedResults.Created($"/Brands/{brand.Id.Value}", response);
  }
}

public sealed class CreateBrandValidator : Validator<CreateBrandRequest>
{
  public CreateBrandValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("El nombre de la marca es obligatorio")
      .MaximumLength(Brand.NameMaxLength)
      .WithMessage($"El nombre de la marca no puede superar los {Brand.NameMaxLength} caracteres");

    RuleFor(x => x.Country)
      .NotEmpty()
      .WithMessage("El país es obligatorio");

    RuleFor(x => x.FoundedYear)
      .Must(year => year >= Brand.MinFoundedYear && year <= DateTime.UtcNow.Year)
      .WithMessage($"El año de fundación debe estar entre {Brand.MinFoundedYear} y el año actual");
  }
}