namespace Sample.Shared.Service.Models
{
	public enum UpdateResultType
	{
		notAuthenticated,
		notFound,
		ok,
	}
	public class UpdateResult : EdjCase.ICP.Candid.CandidVariantValueBase<UpdateResultType>
	{
		public UpdateResult(UpdateResultType type, object? value)  : base(type, value)
		{
		}
		
		protected UpdateResult()
		{
		}
		
		public static UpdateResult notAuthenticated()
		{
			return new UpdateResult(UpdateResultType.notAuthenticated, null);
		}
		
		public static UpdateResult notFound()
		{
			return new UpdateResult(UpdateResultType.notFound, null);
		}
		
		public static UpdateResult ok()
		{
			return new UpdateResult(UpdateResultType.ok, null);
		}
		
	}
}
