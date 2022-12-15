namespace Sample.Shared.Service.Models
{
	public class UpdateRequest
	{
		public Callback? callback { get; set; }
		
		public channelsInfo? channels { get; set; }
		
		public Id id { get; set; }
		
		public enum channelsInfoType
		{
			add,
			remove,
			set,
		}
		public class channelsInfo : EdjCase.ICP.Candid.CandidVariantValueBase<channelsInfoType>
		{
			public channelsInfo(channelsInfoType type, object? value)  : base(type, value)
			{
			}
			
			protected channelsInfo()
			{
			}
			
			public static channelsInfo add(List<ChannelId> info)
			{
				return new channelsInfo(channelsInfoType.add, info);
			}
			
			public List<ChannelId> Asadd()
			{
				this.ValidateType(channelsInfoType.add);
				return (List<ChannelId>)this.value!;
			}
			
			public static channelsInfo remove(List<ChannelId> info)
			{
				return new channelsInfo(channelsInfoType.remove, info);
			}
			
			public List<ChannelId> Asremove()
			{
				this.ValidateType(channelsInfoType.remove);
				return (List<ChannelId>)this.value!;
			}
			
			public static channelsInfo set(List<ChannelId> info)
			{
				return new channelsInfo(channelsInfoType.set, info);
			}
			
			public List<ChannelId> Asset()
			{
				this.ValidateType(channelsInfoType.set);
				return (List<ChannelId>)this.value!;
			}
			
		}
	}
}
