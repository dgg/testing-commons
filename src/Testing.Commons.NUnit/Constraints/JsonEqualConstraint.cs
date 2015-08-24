using NUnit.Framework.Constraints;
using Testing.Commons.Serialization;

namespace Testing.Commons.NUnit.Constraints
{
    public class JsonEqualConstraint : EqualConstraint
    {
        public JsonEqualConstraint(string expected) : base(expected.Jsonify()) { }
    }
}