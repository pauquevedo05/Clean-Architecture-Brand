using FluentValidation.TestHelper;
using ISII.Web.BrandFeatures.Create;

namespace ISII.Tests;

public class CreateBrandValidatorTest
{
    private readonly CreateBrandValidator _validator = new();

    [Fact]
    public async Task ShouldAcceptValidBrand()
    {
        var request = new CreateBrandRequest
        {
            Name = "Acme",
            Country = "Argentina",
            FoundedYear = 1990
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public async Task ShouldRejectEmptyName()
    {
        var request = new CreateBrandRequest
        {
            Name = "",
            Country = "Argentina",
            FoundedYear = 1990
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task ShouldRejectEmptyCountry()
    {
        var request = new CreateBrandRequest
        {
            Name = "Acme",
            Country = "",
            FoundedYear = 1990
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.Country);
    }

    [Fact]
    public async Task ShouldRejectNameLongerThan100Characters()
    {
        var request = new CreateBrandRequest
        {
            Name = new string('A', 101),
            Country = "Argentina",
            FoundedYear = 1990
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public async Task ShouldRejectFoundedYearBefore1800()
    {
        var request = new CreateBrandRequest
        {
            Name = "Acme",
            Country = "Argentina",
            FoundedYear = 1799
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.FoundedYear);
    }

    [Fact]
    public async Task ShouldRejectFoundedYearInFuture()
    {
        var request = new CreateBrandRequest
        {
            Name = "Acme",
            Country = "Argentina",
            FoundedYear = DateTime.UtcNow.Year + 1
        };

        var result = await _validator.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(x => x.FoundedYear);
    }
}
