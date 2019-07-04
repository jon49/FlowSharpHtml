using Superpower;
using Superpower.Parsers;

namespace FlowSharpHtml
{
    public static class TextParser
    {
        public static TextParser<string> EncodedContent =>
            from open in Character.EqualTo('{')
            from chars in Character.Except('}').Many()
            from close in Character.EqualTo('}')
            select new string(chars);

        public static TextParser<string> RawContent =>
            from open in Span.EqualTo("{!")
            from chars in Character.ExceptIn('!', '\\')
                .Or(Character.EqualTo('\\')
                    .IgnoreThen(Character.EqualTo('!'))
                    .Named("escape sequence")
                ).Many()
            from close in Span.EqualTo("!}")
            select new string(chars);

        public static TextParser<string> HtmlContent =>
            from content in Superpower.Parsers.Token. Span.NonWhiteSpace
            select content.ToString();
    }
}
