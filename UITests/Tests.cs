﻿using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Poof.UITests
{
	[TestFixture(Platform.Android)]
	[TestFixture(Platform.iOS)]
	public class Tests
	{
		IApp app;
	    readonly Platform platform;

		public Tests(Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest()
		{
			app = AppInitializer.StartApp(platform);
		}

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

		[Test]
        public void AppLaunches2()
        {
            app.Screenshot("Second screen.");
        }
    }
}
