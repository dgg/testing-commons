using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.ServiceStack.v3;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;

namespace Testing.Commons.Service_Stack.Tests.Example.Tests
{
	[TestFixture]
	public class SingleHostPerTestTester : SingleHostPerTest, IDisposable
	{
		protected override string ServiceName { get { return HostInfo.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return HostInfo.AssembliesWithServices; } }

		private readonly IAppBootstrapper _bootstrapper = Substitute.For<IAppBootstrapper>();

		protected override void Boootstrap(IAppHost arg)
		{
			_bootstrapper.Bootstrap(arg);
		}

		protected override void OnHostDispose(bool disposing)
		{
			_bootstrapper.Dispose();
		}

		[Test]
		public void Test_One() { }

		[Test]
		public void Test_Two() { }

		// called after all teardown is done, but exception makes no test fail
		public void Dispose()
		{
			// bootstapping and disposing happens once per test: two test => twice
			_bootstrapper.Received(2).Bootstrap(Arg.Any<IAppHost>());
			_bootstrapper.Received(2).Dispose();
		}
	}
}