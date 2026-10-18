
using ISII.Web.BrandFeatures.GetById;
using FluentValidation.TestHelper;
using Xunit;

namespace ISII.Tests.BrandFeatures;

public class GetBrandByIdValidatorTest
{
    private readonly GetBrandByIdValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_BrandId_Is_Zero()
    {
        var request = new GetBrandByIdRequest { BrandId = 0 };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.BrandId);
    }

    [Fact]
    public void Should_Have_Error_When_BrandId_Is_Negative()
    {
        var request = new GetBrandByIdRequest { BrandId = -1 };

        var result = _validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.BrandId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_BrandId_Is_Positive()
    {
        var request = new GetBrandByIdRequest { BrandId = 1 };

        var result = _validator.TestValidate(request);

        result.ShouldNotHaveValidationErrorFor(x => x.BrandId);
    }
}
