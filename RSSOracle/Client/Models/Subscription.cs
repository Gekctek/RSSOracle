namespace Sample.Shared.Service.Models
{
	public class Subscription
	{
		public Callback callback { get; set; }
		
		public List<string> channels { get; set; }
		
		public Id id { get; set; }
		
	}
}
