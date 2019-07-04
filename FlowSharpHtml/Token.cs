using Superpower.Display;

namespace FlowSharpHtml
{
    public enum Token
    {
        [Token(Example = "{")]
        LBrace,

        [Token(Example = "}")]
        RBrace,

        [Token(Example = "!")]
        Bang,

        [Token(Example = "<")]
        LT,

        [Token(Example = ">")]
        RT,

        [Token(Example = "/")]
        Slash,

        Identifier,

        String,

        HTML,

        Raw,
        Encoded,
    }
}
