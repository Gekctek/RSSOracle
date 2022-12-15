namespace Sample.Shared.Service.Models
{
	public enum AddResultType
	{
		alreadyExists,
		notAuthenticated,
		ok,
	}
	public class AddResult : EdjCase.ICP.Candid.CandidVariantValueBase<AddResultType>
	{
		public AddResult(AddResultType type, object? value)  : base(type, value)
		{
		}
		
		protected AddResult()
		{
		}
		
		public static AddResult alreadyExists()
		{
			return new AddResult(AddResultType.alreadyExists, null);
		}
		
		public static AddResult notAuthenticated()
		{
			return new AddResult(AddResultType.notAuthenticated, null);
		}
		
		public static AddResult ok()
		{
			return new AddResult(AddResultType.ok, null);
		}
		
	}
}
