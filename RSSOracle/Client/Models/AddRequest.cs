namespace Sample.Shared.Service.Models
{
	public class AddRequest
	{
		public Callback callback { get; set; }
		
		public List<ChannelId> channels { get; set; }
		
		public Id id { get; set; }
		
	}
}
