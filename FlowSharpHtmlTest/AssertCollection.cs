using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlowSharpHtmlTest
{
    public static class AssertCollection
    {
        public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual)
        {
            if (expected is null)
                throw new ArgumentNullException(nameof(expected));
            if (actual is null)
                throw new ArgumentNullException(nameof(actual));

            var errors = new List<string>();
            var e = expected.ToArray();
            var a = actual.ToArray();

            if (e.Length < a.Length)
            {
                errors.Add("The expected value is shorter than the actual value.");
            }
            else if (e.Length > a.Length)
            {
                errors.Add("The expected value is longer than the actual value.");
            }

            var length = Math.Min(e.Length, a.Length);
            var withMessage = errors.Any() ? string.Join('\n', errors) : null;
            for (int i = 0; i < length; i++)
            {
                Assert.AreEqual(e[i], a[i], $@"Expected value ""{e[i]}"" is not equal to the actual value ""{a[i]}"".\n{withMessage}");
            }

            if (withMessage.Length > 0)
            {
                throw new ArgumentOutOfRangeException(withMessage);
            }
        }
    }
}
