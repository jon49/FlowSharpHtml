using FlowSharpHtml;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace FlowSharpHtmlTest
{
    [TestClass]
    public class HtmlParsingTests
    {
        [TestMethod]
        public void Should_be_able_to_parse_HTML()
        {
            // Arrange
            var test = "<h1>Something</h1>";

            // Act
            var result = Parser.TryParse(test, out var value, out var error, out var position);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(1, value.Length);
            Assert.IsInstanceOfType(value[0], typeof(Type));
            var v = (Type)value[0];
            Assert.AreEqual(Token.HTML, v.HTMLType);
            Assert.AreEqual(test, v.Value);
            Assert.IsNull(error);
            Assert.IsFalse(position.HasValue);
        }

        [TestMethod]
        public void Should_be_able_to_parse_HTML_with_odd_white_spacing()
        {
            // Arrange
            var test = @"<something>OK</something>
    <main>OK alrighty</main> Other stuff.

   ";

            // Act
            var result = Parser.TryParse(test, out var value, out var error, out var position);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(error);
            Assert.IsFalse(position.HasValue);
            Assert.AreEqual(5, value.Length);
            var htmlString = string.Join(' ', value.Select(x => ((Type)x).Value));
            Assert.AreEqual("<something>OK</something> <main>OK alrighty</main> Other stuff.", htmlString);
        }

        [TestMethod]
        public void Should_be_able_to_parse_html_without_parsing_encoded_variable()
        {
            // Arrange
            var test = "some random HTML text{variable} {other variable} <p>Other HTML text</p>";

            // Act
            var result = Parser.TryParse(test, out var value, out var error, out var position);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNull(error);
            Assert.IsFalse(position.HasValue);
            var v = value.Select(x => (Type)x);
            AssertCollection.AreEqual(new[]
            {
                Type.Of(Token.HTML, "some"),
                Type.Of(Token.HTML, "random"),
                Type.Of(Token.HTML, "HTML"),
                Type.Of(Token.HTML, "text"),
                Type.Of(Token.Encoded, "variable"),
                Type.Of(Token.Encoded, "other variable"),
                Type.Of(Token.HTML, "<p>Other"),
                Type.Of(Token.HTML, "HTML"),
                Type.Of(Token.HTML, "text</p>"),
            }, v);
        }
    }
}
