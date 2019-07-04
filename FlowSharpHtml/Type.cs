namespace FlowSharpHtml
{
    public struct Type
    {
        public readonly string Value;
        public readonly Token HTMLType;

        private Type( Token htmlType, string value)
        {
            Value = value;
            HTMLType = htmlType;
        }

        public static Type Of(Token htmlType, string value)
            => new Type(htmlType, value);

        public override bool Equals(object obj) 
        {
            if (!(obj is Type o))
                return false;

            return Equals(o);
        }

        public bool Equals(Type obj)
            => HTMLType == obj.HTMLType && Value == obj.Value;

        public override string ToString()
            => $"{HTMLType}: '{Value}'";
    }
}
