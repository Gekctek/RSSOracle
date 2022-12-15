namespace Sample.Shared.Service.Models
{
	public enum GetResultType
	{
		notAuthenticated,
		notFound,
		ok,
	}
	public class GetResult : EdjCase.ICP.Candid.CandidVariantValueBase<GetResultType>
	{
		public GetResult(GetResultType type, object? value)  : base(type, value)
		{
		}
		
		protected GetResult()
		{
		}
		
		public static GetResult notAuthenticated()
		{
			return new GetResult(GetResultType.notAuthenticated, null);
		}
		
		public static GetResult notFound()
		{
			return new GetResult(GetResultType.notFound, null);
		}
		
		public static GetResult ok(Subscription info)
		{
			return new GetResult(GetResultType.ok, info);
		}
		
		public Subscription Asok()
		{
			this.ValidateType(GetResultType.ok);
			return (Subscription)this.value!;
		}
		
	}
}
