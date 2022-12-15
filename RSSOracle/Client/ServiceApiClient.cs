using EdjCase.ICP.Agent.Agents;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models;
using Sample.Shared.Service.Models;

namespace Sample.Shared.Service
{
	public class ServiceApiClient
	{
		public IAgent Agent { get; }
		public Principal CanisterId { get; }
		public ServiceApiClient(IAgent agent, Principal canisterId)
		{
			this.Agent = agent ?? throw new ArgumentNullException(nameof(agent));
			this.CanisterId = canisterId ?? throw new ArgumentNullException(nameof(canisterId));
		}
		public async Task<AddResult> addSubscription(AddRequest arg0, IIdentity? identityOverride = null)
		{
			string method = "addSubscription";
			CandidValueWithType p0 = CandidValueWithType.FromObject<AddRequest>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			AddResult r0 = responseArg.Values[0].ToObject<AddResult>();
			return (r0);
		}
		public async Task<DeleteResult> deleteSubscription(DeleteRequest arg0, IIdentity? identityOverride = null)
		{
			string method = "deleteSubscription";
			CandidValueWithType p0 = CandidValueWithType.FromObject<DeleteRequest>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			DeleteResult r0 = responseArg.Values[0].ToObject<DeleteResult>();
			return (r0);
		}
		public async Task<List<ChannelInfo>> getChannels(IIdentity? identityOverride = null)
		{
			string method = "getChannels";
			var candidArgs = new List<CandidValueWithType>
			{
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			List<ChannelInfo> r0 = responseArg.Values[0].ToObject<List<ChannelInfo>>();
			return (r0);
		}
		public async Task<GetResult> getSubscription(Id arg0, IIdentity? identityOverride = null)
		{
			string method = "getSubscription";
			CandidValueWithType p0 = CandidValueWithType.FromObject<Id>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			QueryResponse response = await this.Agent.QueryAsync(this.CanisterId, method, arg, identityOverride);
			QueryReply reply = response.ThrowOrGetReply();
			GetResult r0 = reply.Arg.Values[0].ToObject<GetResult>();
			return (r0);
		}
		public async Task push(string arg0, Content arg1, IIdentity? identityOverride = null)
		{
			string method = "push";
			CandidValueWithType p0 = CandidValueWithType.FromObject<string>(arg0, false);
			CandidValueWithType p1 = CandidValueWithType.FromObject<Content>(arg1, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
				p1,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
		}
		public async Task<UpdateResult> updateSubscription(UpdateRequest arg0, IIdentity? identityOverride = null)
		{
			string method = "updateSubscription";
			CandidValueWithType p0 = CandidValueWithType.FromObject<UpdateRequest>(arg0, false);
			var candidArgs = new List<CandidValueWithType>
			{
				p0,
			};
			CandidArg arg = CandidArg.FromCandid(candidArgs);
			CandidArg responseArg = await this.Agent.CallAndWaitAsync(this.CanisterId, method, arg, null, identityOverride);
			UpdateResult r0 = responseArg.Values[0].ToObject<UpdateResult>();
			return (r0);
		}
	}
}
