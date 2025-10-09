using UtmBuilder.Core.ValueObjects;
using UtmBuilder.Core.ValueObjects.Exceptions;

namespace UtmBuilder.Core.Tests.ValueObjects;

[TestClass]
public class UrlTests
{
    private const string InvalidUrl = "juice fruit";
    private const string validUrl = "https://balta.io";

    [TestMethod]
    [ExpectedException(typeof(InvalidUrlException))]
    public void SouldBeAbeReturnExceptionWhenUrlIsInvalid()
    {
        new Url(InvalidUrl);
    }

    [TestMethod]
    public void SouldBeAbeNotReturnExceptionWhenUrlIsInvalid()
    {
        new Url(validUrl);
        Assert.IsTrue(true);
    }
}