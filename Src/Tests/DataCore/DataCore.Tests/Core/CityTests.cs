using DataCoreModule.Core.Models.Entities;

namespace DataCore.Tests.Core;

public class CityTests
{
    [Fact]
    public void Prop_Should_ThrowException_When_NameIsNullOrEmpty()
    {
        // Arrange
        var city = new City("Name");

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            city.Name = null!;
        });

        Assert.Throws<ArgumentException>(() =>
        {
            city.Name = "";
        });
    }

    [Fact]
    public void Ctor_Should_ThrowException_When_NameIsNullOrEmpty()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            var city = new City(null!);
        });

        Assert.Throws<ArgumentException>(() =>
        {
            var city = new City("");
        });
    }

    [Fact]
    public void IsMonitored_Should_Be_False_WhenCreating()
    {
        // Arrange
        var city = new City("Name");

        // Assert
        Assert.False(city.IsMonitored);
    }

    [Fact]
    public void Prop_Should_Make_FirstNameLetter_UpperCase()
    {
        // Arrange
        var city = new City(":D:D:");

        // Act
        city.Name = "name";

        // Assert
        Assert.Equal("Name", city.Name);
    }

    [Fact]
    public void Ctor_Should_Make_FirstNameLetter_UpperCase()
    {
        // Act
        var city = new City("name");

        // Assert
        Assert.Equal("Name", city.Name);
    }

    [Fact]
    public void Ctor_Should_SetName_When_NameIsValid()
    {
        // Arrange & Act
        var city = new City("ValidName");

        // Assert
        Assert.Equal("ValidName", city.Name);
    }

    [Fact]
    public void Prop_Should_SetName_When_NameIsValid()
    {
        // Arrange
        var city = new City("InitialName");

        // Act
        city.Name = "ValidName";

        // Assert
        Assert.Equal("ValidName", city.Name);
    }

    [Fact]
    public void Prop_Should_SetIsMonitored_When_ValueIsValid()
    {
        // Arrange
        var city = new City("Name");

        // Act
        city.IsMonitored = true;

        // Assert
        Assert.True(city.IsMonitored);
    }
}
