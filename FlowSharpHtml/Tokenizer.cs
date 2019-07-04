using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using Superpower.Tokenizers;

namespace FlowSharpHtml
{
    public static class Tokenizer
    {
        public static TextParser<Unit> StringArg =>
            from open in Character.EqualTo('"')
            from content in Span.EqualTo(@"\\""").Value(Unit.Value).Try()
                .Or(Character.Except('"').Value(Unit.Value))
                .IgnoreMany()
            from close in Character.EqualTo('"')
            select Unit.Value;

        public static TextParser<Unit> EncodedContent =>
            from open in Character.EqualTo('{')
            from content in Character.Except('}').Value(Unit.Value).IgnoreMany()
            from close in Character.EqualTo('}')
            select Unit.Value;

        public static TextParser<Unit> RawContent =>
            from open in Span.EqualTo("{!")
            from content in Span.EqualTo("\\!").Value(Unit.Value).Try()
                .Or(Character.Except('!').Value(Unit.Value))
                .IgnoreMany()
            from close in Span.EqualTo("!}").IgnoreMany()
            select Unit.Value;

        //public static TextParser<Unit> HtmlContent =>
        //    from content in Superpower.Parsers.Span.("");

        public static Tokenizer<Token> Instance =>
            new TokenizerBuilder<Token>()
            .Ignore(Span.WhiteSpace)
            .Match(RawContent, Token.Raw)
            .Match(EncodedContent, Token.Encoded)
            .Match(Span.NonWhiteSpace, Token.HTML)
            .Build();
    }
}
