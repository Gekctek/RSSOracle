namespace Sample.Shared.Service.Models
{
	public class Content
	{
		public List<Author> authors { get; set; }
		
		public bodyInfo? body { get; set; }
		
		public Time date { get; set; }
		
		public string? imageLink { get; set; }
		
		public string? language { get; set; }
		
		public string link { get; set; }
		
		public string title { get; set; }
		
		public class bodyInfo
		{
			public string? format { get; set; }
			
			public string value { get; set; }
			
		}
	}
}
