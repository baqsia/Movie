using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Movie.Api.Service;
using NUnit.Framework;

namespace Movie.Api.UnitTests;

public class GuidTransformServiceTests
{
    private readonly GuidTransformService _sut = new();

    [Test(Description = "ToUriString should return correct Base64 string")]
    public void ToUriString_ShouldReturnCorrectBase64String()
    {
        //Arrange
        var id = Guid.NewGuid();
        
        //Act
        var result = _sut.ToUriString(id);

        //Assert
        using (new AssertionScope())
        {
            result.Should().NotBeNull();
            result.Should().HaveLength(22);
        }
    }
    
    [Test(Description = "ToUriString should return correct Base64 string")]
    [TestCase("SRhqjbphG0+JllKSUPhEcA")]
    [TestCase("eJFYfV4rskOWLqp_RNAMew")]
    public void FromUriString_ShouldReturnCorrectBase64String(string id)
    { 
        //Act
        var result = _sut.FromUriString(id);
        
        //Assert
        using (new AssertionScope())
        {
            result.Should().NotBeEmpty();
        }
    }
}