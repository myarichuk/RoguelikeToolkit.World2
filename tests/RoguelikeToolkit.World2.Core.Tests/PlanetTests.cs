using Xunit;

namespace RoguelikeToolkit.World2.Core.Tests;

public class PlanetTests
{
    [Fact]
    public void SameSeed_ProducesSameElevation()
    {
        var a = new Planet(42).SampleElevation(12.5, -77.1);
        var b = new Planet(42).SampleElevation(12.5, -77.1);

        Assert.Equal(a, b);
    }

    [Fact]
    public void DifferentSeeds_ProduceDifferentElevation()
    {
        Assert.NotEqual(new Planet(1).SampleElevation(10, 20), new Planet(2).SampleElevation(10, 20));
    }

    [Theory]
    [InlineData(-90, 0)]
    [InlineData(0, 180)]
    [InlineData(45, -45)]
    [InlineData(90, 123)]
    public void Elevation_IsNormalized(double lat, double lon)
    {
        var e = new Planet(7).SampleElevation(lat, lon);

        Assert.InRange(e, 0.0, 1.0);
    }

    [Fact]
    public void LongitudeSeam_IsContinuous()
    {
        var planet = new Planet(7);

        Assert.Equal(planet.SampleElevation(10, 180), planet.SampleElevation(10, -180), 9);
    }
}
