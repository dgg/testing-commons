using System.Collections.Specialized;
using NUnit.Framework;
using Testing.Commons.Web;

namespace Testing.Commons.Tests.Web
{
	[TestFixture]
	public class QueryBuilderTester
	{
		[Test]
		public void Ctor_NullCollection_EmptyQuery()
		{
			var subject = new QueryBuilder((NameValueCollection)null);
			Assert.That(subject.Query, Is.Empty);
		}

		[Test]
		public void Ctor_EmptyCollection_EmptyQuery()
		{
			var subject = new QueryBuilder(new NameValueCollection());
			Assert.That(subject.Query, Is.Empty);
		}

		[Test]
		public void ctor_OneElement_EqualSeparatedKeyValue()
		{
			var subject = new QueryBuilder(new NameValueCollection { { "a", "b" } });
			Assert.That(subject.Query, Is.EqualTo("a=b"));
		}

		[Test]
		public void ctor_TwoElement_EqualSeparatedKeyValuesTokenizedByAmpersand()
		{
			var subject = new QueryBuilder(new NameValueCollection { { "a", "b" }, {"c", "d"} });
			Assert.That(subject.Query, Is.EqualTo("a=b&c=d"));
		}

		[Test]
		public void Ctor_EmmptyKeys_AreNotAdded()
		{
			var subject = new QueryBuilder(new NameValueCollection { { "a", "b" }, { null, "d" } });
			Assert.That(subject.Query, Is.EqualTo("a=b"));
		}

		[Test]
		public void Ctor_EncodableKeys_GetEncoded()
		{
			var subject = new QueryBuilder(new NameValueCollection { { "a a", "b" } });
			Assert.That(subject.Query, Is.EqualTo("a+a=b"));
		}

		[Test]
		public void Ctor_EncodableValues_GetEncoded()
		{
			var subject = new QueryBuilder(new NameValueCollection { { "a", "b&b" } });

			Assert.That(subject.Query, Is.EqualTo("a=b%26b"));
		}
	}
}
