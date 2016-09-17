﻿using System;
using RpcLite;
using RpcLite.AspNetCore;
using RpcLite.Client;
using ServiceRegistry.Contract;

namespace ServiceTest.ClientTest
{
	public class RegistryTest
	{
		public static void Test()
		{
			try
			{
				RpcInitializer.Initialize();

				var client = ClientFactory.GetInstance<IRegistryService>();

				var resp = client.GetServiceAddressAsync(new GetServiceAddressRequest
				{
					ServiceName = "ProductService",
					Namespace = "v1",
					Environment = "IT",
				});

				Console.WriteLine(resp.Result.Address.ToString());
			}
			catch (ConnectionException ex)
			{
				Console.WriteLine(ex);
				//throw;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				//throw;
			}

		}
	}
}
