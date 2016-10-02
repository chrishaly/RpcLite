﻿namespace RpcLite.Client
{
	/// <summary>
	/// 
	/// </summary>
	public class DefaultClientChannel : IClientChannelFactory
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public IClientChannel GetClientChannel(string address)
		{
			return new HttpClientChannel(address);
		}
	}
}