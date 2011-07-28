#region license

// Copyright 2004-2011 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//      http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

namespace Castle.Facilities.Transactions.Tests
{
	using MicroKernel.Facilities;
	using MicroKernel.Registration;
	using NUnit.Framework;
	using Windsor;

	public class InitTests
	{
		[Test]
		public void Cannot_Register_Class_Without_Virtual_Method()
		{
			var c = new WindsorContainer();
			c.AddFacility<AutoTxFacility>();

			try
			{
				c.Register(Component.For<FaultyComponent>());
				Assert.Fail("invalid component registration should be noted.");
			}
			catch (FacilityException ex)
			{
				Assert.That(ex.Message.Contains("FaultyMethod"));
				Assert.That(ex.Message.Contains("virtual"));
			}
		}

		internal class FaultyComponent
		{
			[Transaction]
			public void FaultyMethod()
			{
			}
		}
	}
}