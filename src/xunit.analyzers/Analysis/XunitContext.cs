﻿using System;
using Microsoft.CodeAnalysis;

namespace Xunit.Analyzers
{
	/// <summary>
	/// Class which provides information about references to xUnit.net assemblies.
	/// </summary>
	public class XunitContext
	{
		static readonly Version v2AbstractionsVersion = new(2, 0, 3);

		XunitContext()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="XunitContext"/> class.
		/// </summary>
		/// <param name="compilation">The Roslyn compilation object used to look up types and
		/// inspect references</param>
		public XunitContext(Compilation compilation)
		{
			V2Abstractions = V2AbstractionsContext.Get(compilation);
			V2Core = V2CoreContext.Get(compilation);
			V2Execution = V2ExecutionContext.Get(compilation);
			V3Core = V3CoreContext.Get(compilation);
		}

		/// <summary>
		/// Gets a flag which indicates whether there are any xUnit.net v2 references in the project
		/// (including abstractions, core, and execution references).
		/// </summary>
		public bool HasV2References =>
			V2Abstractions is not null || V2Core is not null || V2Execution is not null;

		/// <summary>
		/// Gets a flag which indicates whether there are any xUnit.net v3 references in the project.
		/// </summary>
		public bool HasV3References =>
			V3Core is not null;

		/// <summary>
		/// Gets information about the reference to xunit.abstractions (v2). If the project does
		/// not reference v2 Abstractions, then returns <c>null</c>.
		/// </summary>
		public V2AbstractionsContext? V2Abstractions { get; set; }

		/// <summary>
		/// Gets information about the reference to xunit.core (v2). If the project does not
		/// reference v2 Core, then returns <c>null</c>.
		/// </summary>
		public V2CoreContext? V2Core { get; set; }

		/// <summary>
		/// Gets information about the reference to xunit.execution.* (v2). If the project does
		/// not reference v2 Execution, then returns <c>null</c>.
		/// </summary>
		public V2ExecutionContext? V2Execution { get; set; }

		/// <summary>
		/// Gets information about the reference to xunit.v3.core (v3). If the project does not
		/// reference v3 Core, then returns <c>null</c>.
		/// </summary>
		public V3CoreContext? V3Core { get; set; }

		/// <summary>
		/// Used to create a context object for testing purposes, which is stuck to a specific version
		/// of xunit.core and xunit.execution.* (abstractions is always set to 2.0.3, since it did
		/// not float versions).
		/// </summary>
		/// <param name="compilation">The Roslyn compilation object used to look up types</param>
		/// <param name="v2VersionOverride">The overridden version for xunit.core and xunit.execution.*</param>
		public static XunitContext ForV2(
			Compilation compilation,
			Version? v2VersionOverride = null) =>
				new()
				{
					V2Abstractions = V2AbstractionsContext.Get(compilation, v2AbstractionsVersion),
					V2Core = V2CoreContext.Get(compilation, v2VersionOverride),
					V2Execution = V2ExecutionContext.Get(compilation, v2VersionOverride)
				};

		/// <summary>
		/// Used to create a context object for testing purposes, which is stuck to a specific version
		/// of xunit.v3.core.
		/// </summary>
		/// <param name="compilation">The Roslyn compilation object used to look up types</param>
		/// <param name="v3VersionOverride">The overridden version for xunit.v3.core</param>
		public static XunitContext ForV3(
			Compilation compilation,
			Version? v3VersionOverride = null) =>
				new() { V3Core = V3CoreContext.Get(compilation, v3VersionOverride) };
	}
}
