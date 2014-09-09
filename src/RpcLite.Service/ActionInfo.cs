﻿using System;
using System.Reflection;

namespace RpcLite
{
	public class ActionInfo
	{
		public string Name { get; set; }
		public MethodInfo MethodInfo { get; set; }
		public int ArgumentCount { get; set; }
		public Type ArgumentType { get; set; }
		public bool HasReturnValue { get; set; }
		public Func<object, object, object> Func { get; set; }
		public Action<object, object> Action { get; set; }
		public Func<object> ServiceCreator { get; set; }
	}
}