using Superpower;
using P = Superpower.Parsers;
using Superpower.Model;

namespace FlowSharpHtml
{
    public static class Parser
    {
        public static TokenListParser<Token, object> EncodedContent
            => P.Token.EqualTo(Token.Encoded)
            .Apply(TextParser.EncodedContent)
            .Select(x => (object)Type.Of(Token.Encoded, x));

        public static TokenListParser<Token, object> RawContent
            => P.Token.EqualTo(Token.Raw)
            .Apply(TextParser.RawContent)
            .Select(x => (object)Type.Of(Token.Raw, x));

        public static TokenListParser<Token, object> HtmlContent
            => P.Token.EqualTo(Token.HTML)
            .Apply(TextParser.HtmlContent)
            .Select(x => (object)Type.Of(Token.HTML, x));

        public static TokenListParser<Token, object[]> HtmlValue
            => RawContent
            .Or(EncodedContent)
            .Or(HtmlContent)
            .Many()
            .Named("HTML value");

        public static TokenListParser<Token, object[]> HtmlDocument
            => HtmlValue.AtEnd();

        public static bool TryParse(
            string html,
            out object[] value,
            out string error,
            out Position errorPosition)
        {
            var tokens = Tokenizer.Instance.TryTokenize(html);
            if (!tokens.HasValue)
            {
                value = null;
                error = tokens.ToString();
                errorPosition = tokens.ErrorPosition;
                return false;
            }

            var parsed = HtmlDocument.TryParse(tokens.Value);
            if (!parsed.HasValue)
            {
                value = null;
                error = parsed.ToString();
                errorPosition = parsed.ErrorPosition;
                return false;
            }

            value = parsed.Value;
            error = null;
            errorPosition = Position.Empty;
            return true;
        }
    }
}
