using System;
using System.Collections.Generic;
using System.Reflection;
using NSubstitute;
using NUnit.Framework;
using ServiceStack.WebHost.Endpoints;
using Testing.Commons.Service_Stack.v3;
using Testing.Commons.Service_Stack.Tests.Example.Infrastructure.Shared;

namespace Testing.Commons.Service_Stack.Tests.Example.Tests
{
	[TestFixture]
	public class SingleHostPerFixtureTester : SingleHostPerFixture, IDisposable
	{
		protected override string ServiceName { get { return HostInfo.ServiceName; } }

		protected override IEnumerable<Assembly> AssembliesWithServices { get { return HostInfo.AssembliesWithServices; } }

		private readonly IAppHostBootstrapper _bootstrapper = Substitute.For<IAppHostBootstrapper>();

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
			// bootstapping and disposing happens exactly once
			_bootstrapper.Received(1).Bootstrap(Arg.Any<IAppHost>());
			_bootstrapper.Received(1).Dispose();
		}
	}
}