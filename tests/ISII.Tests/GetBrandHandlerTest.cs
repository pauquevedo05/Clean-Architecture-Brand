
using Ardalis.Result;
using Ardalis.SharedKernel;
using Ardalis.Specification;
using ISII.Web.Domain.BrandAggregate;
using ISII.Web.BrandFeatures.GetById;
using NSubstitute;
using Xunit;

namespace ISII.Tests.BrandFeatures;

public class GetBrandHandlerTest
{
    private readonly IReadRepository<Brand> _repository =
        Substitute.For<IReadRepository<Brand>>();

    [Fact]
    public async Task Handle_Should_Return_Brand_When_Brand_Exists()
    {
        // Arrange
        var brand = Brand.Create(
            BrandId.From(1),
            "Acme",
            "Argentina",
            1990);

        _repository
            .FirstOrDefaultAsync(
                Arg.Any<ISpecification<Brand>>(),
                Arg.Any<CancellationToken>())
            .Returns(brand);

        var handler = new GetBrandHandler(_repository);
        var query = new GetBrandQuery(BrandId.From(1));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal("Acme", result.Value.Name);
        Assert.Equal("Argentina", result.Value.Country);
        Assert.Equal(1990, result.Value.FoundedYear);
    }

    [Fact]
    public async Task Handle_Should_Return_NotFound_When_Brand_Does_Not_Exist()
    {
        // Arrange
        _repository
            .FirstOrDefaultAsync(
                Arg.Any<ISpecification<Brand>>(),
                Arg.Any<CancellationToken>())
            .Returns((Brand?)null);

        var handler = new GetBrandHandler(_repository);
        var query = new GetBrandQuery(BrandId.From(999));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
