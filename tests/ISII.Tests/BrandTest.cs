
using ISII.Web.Domain.BrandAggregate;

namespace ISII.Tests;

public class BrandTest
{
    [Fact]
    public void Constructor_ShouldAssignProvidedValues()
    {
        var id = BrandId.New;
        const string name = "Acme";
        const string country = "Argentina";
        var foundedYear = 1990;

        var brand = new Brand(id, name, country, foundedYear);

        Assert.Equal(id, brand.Id);
        Assert.Equal(name, brand.Name);
        Assert.Equal(country, brand.Country);
        Assert.Equal(foundedYear, brand.FoundedYear);
    }

    [Fact]
    public void Create_ShouldReturnBrandWithProvidedValues()
    {
        var id = BrandId.New;
        const string name = "Globex";
        const string country = "Brasil";
        var foundedYear = 2005;

        var brand = Brand.Create(id, name, country, foundedYear);

        Assert.NotNull(brand);
        Assert.Equal(id, brand.Id);
        Assert.Equal(name, brand.Name);
        Assert.Equal(country, brand.Country);
        Assert.Equal(foundedYear, brand.FoundedYear);
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyName()
    {
        Assert.ThrowsAny<ArgumentException>(() =>
            new Brand(BrandId.New, "", "Argentina", 1990));
    }

    [Fact]
    public void Constructor_ShouldRejectEmptyCountry()
    {
        Assert.ThrowsAny<ArgumentException>(() =>
            new Brand(BrandId.New, "Acme", "", 1990));
    }

    [Fact]
    public void Constructor_ShouldRejectNameLongerThan100Characters()
    {
        var name = new string('A', Brand.NameMaxLength + 1);

        Assert.Throws<ArgumentException>(() =>
            new Brand(BrandId.New, name, "Argentina", 1990));
    }

    [Fact]
    public void Constructor_ShouldRejectFoundedYearBefore1800()
    {
        Assert.ThrowsAny<ArgumentException>(() =>
            new Brand(BrandId.New, "Acme", "Argentina", 1799));
    }

    [Fact]
    public void Constructor_ShouldRejectFoundedYearInTheFuture()
    {
        var futureYear = DateTime.UtcNow.Year + 1;

        Assert.ThrowsAny<ArgumentException>(() =>
            new Brand(BrandId.New, "Acme", "Argentina", futureYear));
    }
}
