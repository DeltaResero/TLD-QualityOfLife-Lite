namespace System.Runtime.CompilerServices
{
	// UnityEngine.CoreModule (Unity 6) exposes a public NullableAttribute with its constructors stripped by Il2CppInterop.
	// Declaring our own makes the compiler use this one instead.
	[AttributeUsage( AttributeTargets.All, AllowMultiple = false, Inherited = false )]
	internal sealed class NullableAttribute : Attribute
	{
		public readonly byte[] NullableFlags;

		public NullableAttribute( byte Flag )
		{
			NullableFlags = new[] { Flag };
		}

		public NullableAttribute( byte[] Flags )
		{
			NullableFlags = Flags;
		}
	}
}
