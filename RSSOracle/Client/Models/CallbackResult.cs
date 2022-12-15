namespace Sample.Shared.Service.Models
{
	public enum CallbackResultType
	{
		accepted,
		notAuthorized,
	}
	public class CallbackResult : EdjCase.ICP.Candid.CandidVariantValueBase<CallbackResultType>
	{
		public CallbackResult(CallbackResultType type, object? value)  : base(type, value)
		{
		}
		
		protected CallbackResult()
		{
		}
		
		public static CallbackResult accepted()
		{
			return new CallbackResult(CallbackResultType.accepted, null);
		}
		
		public static CallbackResult notAuthorized()
		{
			return new CallbackResult(CallbackResultType.notAuthorized, null);
		}
		
	}
}
