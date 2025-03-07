// SPDX-FileCopyrightText: © SimPE contributors
// SPDX-License-Identifier: GPL-2.0-or-later

using System;
using System.CodeDom.Compiler;
using System.Reflection;

using Microsoft.CSharp;

namespace SimPe
{
	/// <summary>
	/// Class that offers method allowing you to simply compile c# sourcecode at runtime
	/// </summary>
	public static class RuntimeCompiler
	{
		/// <summary>
		/// Compiles C# Code at runtime
		/// </summary>
		/// <param name="s">Your Code</param>
		/// <returns></returns>
		/// <remarks>based on http://www.csharpfriends.com/Articles/getArticle.aspx?articleID=118</remarks>
		public static Assembly Compile(string s)
		{
			return Compile(s, new string[0]);
		}

		/// <summary>
		/// Compiles C# Code at runtime
		/// </summary>
		/// <param name="s">Your Code</param>
		/// <param name="referenced">List of references Assemblies</param>
		/// <returns></returns>
		/// <remarks>based on http://www.csharpfriends.com/Articles/getArticle.aspx?articleID=118</remarks>
		public static Assembly Compile(string s, string[] referenced)
		{
			string flname = System.IO.Path.GetTempFileName();

			// Create the C# compiler
			CodeDomProvider iCodeCompiler = new CSharpCodeProvider();

			// input params for the compiler
			CompilerParameters compilerParams = new CompilerParameters
			{
				GenerateInMemory = true,
				GenerateExecutable = false,
				IncludeDebugInformation = true
			};

			compilerParams.ReferencedAssemblies.Add("system.dll");
			compilerParams.ReferencedAssemblies.Add("system.data.dll");
			compilerParams.ReferencedAssemblies.Add("system.xml.dll");
			foreach (string rs in referenced)
			{
				compilerParams.ReferencedAssemblies.Add(rs);
			}

			// Run the compiler and build the assembly
			CompilerResults res = iCodeCompiler.CompileAssemblyFromSource(
				compilerParams,
				s
			);

			if (res.Errors.Count > 0)
			{
				string errs = "";
				foreach (object o in res.Errors)
				{
					if (o != null)
					{
						errs += o.ToString() + Helper.lbr;
					}
				}

				throw new Exception(
					"Failed to compile RuntimePathSettings",
					new Exception(errs)
				);
			}

			return res.CompiledAssembly;
		}

		/// <summary>
		/// Instanciate a class in an assembly
		/// </summary>
		/// <param name="asm">The assembly that contains the class</param>
		/// <param name="name">The name of the class</param>
		/// <param name="args">The arguments passed to the constructor of the class</param>
		/// <returns>null on error or the instance of the passed class</returns>
		public static object CreateInstance(Assembly asm, string name, object[] args)
		{
			Type t = asm.GetType(name, false);
			return t == null ? null : Activator.CreateInstance(t, args);
		}
	}
}
