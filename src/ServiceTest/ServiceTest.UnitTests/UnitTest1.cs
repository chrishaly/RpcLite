using System;
using RpcLite;
using RpcLite.Client;
using RpcLite.Config;
using RpcLite.Server.Kestrel;
using ServiceTest.UnitTests.Basics;
using Xunit;
using IProductService = ServiceTest.UnitTests.Basics.IProductService;

namespace ServiceTest.UnitTests
{
	public class UnitTest1 : IDisposable
	{
		public UnitTest1()
		{
		}

		[Fact]
		public void Test1()
		{
			var serverAddress = "http://localhost:" + (40000 + new Random().Next(10000)).ToString();
			var serviceAddress = serverAddress + "/api/service/";

			var server = new ServerBuilder()
				.UseConfig(config => config.AddService<ProductService>("api/service/"))
				.UseUrls(serverAddress)
				.Build();
			server.Start();

			RpcManager.Initialize(new RpcConfig());
			var client = ClientFactory.GetInstance<IProductService>(serviceAddress);

			var product1 = client.GetById(9);
			Assert.NotNull(product1);
			Assert.Equal(9, product1.Id);

			var prodcut2 = client.GetByIdAsync(9).Result;
			Assert.Equal(9, prodcut2.Id);

			var page1 = client.GetPage(1, 3);
			Assert.Equal(3, page1.Length);

			var page2 = client.GetPageAsync(1, 3).Result;
			Assert.Equal(3, page2.Length);
		}

		public void Dispose()
		{
		}
	}
}
