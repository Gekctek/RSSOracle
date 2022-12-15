namespace Sample.Shared.Service.Models
{
	public enum DeleteResultType
	{
		notAuthenticated,
		notFound,
		ok,
	}
	public class DeleteResult : EdjCase.ICP.Candid.CandidVariantValueBase<DeleteResultType>
	{
		public DeleteResult(DeleteResultType type, object? value)  : base(type, value)
		{
		}
		
		protected DeleteResult()
		{
		}
		
		public static DeleteResult notAuthenticated()
		{
			return new DeleteResult(DeleteResultType.notAuthenticated, null);
		}
		
		public static DeleteResult notFound()
		{
			return new DeleteResult(DeleteResultType.notFound, null);
		}
		
		public static DeleteResult ok()
		{
			return new DeleteResult(DeleteResultType.ok, null);
		}
		
	}
}
