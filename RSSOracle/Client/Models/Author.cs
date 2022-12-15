using EdjCase.ICP.Candid;
using EdjCase.ICP.Candid.Models;

namespace Sample.Shared.Service.Models
{
	public enum AuthorType
	{
		[VariantOptionType(typeof(string))]
		handle,
        [VariantOptionType(typeof(Principal))]
        identity,
        [VariantOptionType(typeof(string))]
        name,
	}
	public class Author : EdjCase.ICP.Candid.CandidVariantValueBase<AuthorType>
	{
		public Author(AuthorType type, object? value)  : base(type, value)
		{
		}
		
		protected Author()
		{
		}
		
		public static Author handle(string info)
		{
			return new Author(AuthorType.handle, info);
		}
		
		public string Ashandle()
		{
			this.ValidateType(AuthorType.handle);
			return (string)this.value!;
		}
		
		public static Author identity(EdjCase.ICP.Candid.Models.Principal info)
		{
			return new Author(AuthorType.identity, info);
		}
		
		public EdjCase.ICP.Candid.Models.Principal Asidentity()
		{
			this.ValidateType(AuthorType.identity);
			return (EdjCase.ICP.Candid.Models.Principal)this.value!;
		}
		
		public static Author name(string info)
		{
			return new Author(AuthorType.name, info);
		}
		
		public string Asname()
		{
			this.ValidateType(AuthorType.name);
			return (string)this.value!;
		}
		
	}
}
