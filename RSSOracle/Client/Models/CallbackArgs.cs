namespace Sample.Shared.Service.Models
{
	public class CallbackArgs
	{
		public string channelId { get; set; }
		
		public messageInfo message { get; set; }
		
		public Id subscriptionId { get; set; }
		
		public EdjCase.ICP.Candid.Models.Principal userId { get; set; }
		
		public enum messageInfoType
		{
			changeOwner,
			newContent,
		}
		public class messageInfo : EdjCase.ICP.Candid.CandidVariantValueBase<messageInfoType>
		{
			public messageInfo(messageInfoType type, object? value)  : base(type, value)
			{
			}
			
			protected messageInfo()
			{
			}
			
			public static messageInfo changeOwner(EdjCase.ICP.Candid.Models.Principal info)
			{
				return new messageInfo(messageInfoType.changeOwner, info);
			}
			
			public EdjCase.ICP.Candid.Models.Principal AschangeOwner()
			{
				this.ValidateType(messageInfoType.changeOwner);
				return (EdjCase.ICP.Candid.Models.Principal)this.value!;
			}
			
			public static messageInfo newContent(Content info)
			{
				return new messageInfo(messageInfoType.newContent, info);
			}
			
			public Content AsnewContent()
			{
				this.ValidateType(messageInfoType.newContent);
				return (Content)this.value!;
			}
			
		}
	}
}
