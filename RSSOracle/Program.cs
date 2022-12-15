using EdjCase.ICP.Agent.Auth;
using EdjCase.ICP.Candid.Models.Types;
using EdjCase.ICP.Candid.Models.Values;
using EdjCase.ICP.Candid.Models;
using System.Xml.Serialization;
using EdjCase.ICP.Agent.Requests;
using EdjCase.ICP.Candid;
using System.Net.Http.Headers;
using Path = EdjCase.ICP.Candid.Models.Path;
using EdjCase.ICP.Agent.Agents;
using Microsoft.Extensions.Configuration;
using System;
using Sample.Shared.Service;
using EdjCase.ICP.Agent.Responses;
using System.Threading.Channels;
using System.Xml;
using System.IO;
using CodeHollow.FeedReader;
using System.Runtime.CompilerServices;
using CodeHollow.FeedReader.Feeds;
using Sample.Shared.Service.Models;

var configBuilder = new ConfigurationBuilder()
    .AddEnvironmentVariables()
    .AddJsonFile("appsettings.json")
#if DEBUG
    .AddUserSecrets<Program>();
#endif

IConfiguration config = configBuilder.Build();

string rssCanisterId = config["RSSCanisterId"] ?? throw new InvalidOperationException("'RSSCanisterId' must be set in ENV");
string? baseUrl = config["BaseUrl"];

var identity = new AnonymousIdentity();
HttpAgent httpAgent = new(identity, baseUrl == null ? null : new Uri(baseUrl));
var canisterId = Principal.FromText(rssCanisterId);

var client = new ServiceApiClient(httpAgent, canisterId);

Console.WriteLine("Starting up...");
while (true)
{
    DateTime nextCheck = DateTime.UtcNow.AddMinutes(5);
    //Console.WriteLine("Getting channels...");
    List<ChannelInfo> channels = await client.getChannels();


    using (var httpClient = new HttpClient())
    {
        foreach (ChannelInfo channel in channels)
        {
            Feed feed = await FeedReader.ReadAsync(channel.id);

            var channelId = CandidValueWithType.FromValueAndType(
                CandidPrimitive.Text(channel.id),
                new CandidPrimitiveType(PrimitiveType.Text)
            );
            List<Content> content = ParseContent(feed, channel.lastUpdated).ToList();

            if(content.Any())
            {
                Console.WriteLine($"Channel: {channel.id}");
                Console.WriteLine($"New Content Count: {content.Count}");
                await Task.WhenAll(content.Select(async c =>
                {
                    await client.push(channel.id, c);
                }));
            }
        }
    }
    TimeSpan delay;
    try
    {
        delay = nextCheck - DateTime.UtcNow;
    }
    catch
    {
        delay = TimeSpan.Zero;
    }
    //Console.WriteLine("Done. Next check in " + delay);
    await Task.Delay(delay);
}

IEnumerable<Content> ParseContent(Feed feed, UnboundedInt? lastUpdated)
{
    DateTimeOffset? lastUpdatedDate = lastUpdated == null ? null : DateTimeOffset.FromUnixTimeMilliseconds((long)lastUpdated.ToBigInteger() / 1000000);
    foreach(FeedItem item in feed.Items)
    {
        if (lastUpdatedDate != null && item.PublishingDate!.Value <= lastUpdatedDate.Value)
        {
            continue;
        }
        var authors = new List<Author>();
        if (!string.IsNullOrWhiteSpace(item.Author))
        {
            authors.Add(Author.name(item.Author));
        }
        (Content.bodyInfo? body, string? imageLink) = item.SpecificItem switch
        {
            AtomFeedItem a => (new Content.bodyInfo { value = item.Content, format = "html" }, (string?)null),
            MediaRssFeedItem m => (null, m.Media.FirstOrDefault()?.Url),
            _ => throw new NotImplementedException(),
        };
        yield return new Content
        {
            authors = authors,
            body = body,
            date = DateToNano(item.PublishingDate),
            imageLink = imageLink,
            language = "en-us",
            link = item.Link,
            title = item.Title
        };
    }
}

Time DateToNano(DateTime? date)
{
    if(date == null)
    {
        throw new InvalidDataException("Missing date");
    }
    return (ulong)(date.Value - DateTime.UnixEpoch).TotalMicroseconds * 1000;
}

//async Task PushAsync(FeedContent content, CandidValueWithType channelId)
//{
//    //var encodedArgument = CandidArg.FromCandid(
//    //        channelId,
//    //        CandidValueWithType.FromValueAndType(
//    //            new CandidRecord(new Dictionary<CandidTag, CandidValue>
//    //            {
//    //            {
//    //                CandidTag.FromName("title"),
//    //                CandidPrimitive.Text(content.Title)
//    //            },
//    //            {
//    //                CandidTag.FromName("body"),
//    //                new CandidRecord(new Dictionary<CandidTag, CandidValue>
//    //                {
//    //                    {
//    //                        CandidTag.FromName("format"),
//    //                        new CandidOptional(CandidPrimitive.Text("html"))
//    //                    },
//    //                    {
//    //                        CandidTag.FromName("value"),
//    //                        CandidPrimitive.Text(content.Content)
//    //                    }
//    //                })
//    //            },
//    //            {
//    //                CandidTag.FromName("link"),
//    //                CandidPrimitive.Text(content.Link)
//    //            },
//    //            {
//    //                CandidTag.FromName("authors"),
//    //                new CandidVector(new [] {new CandidVariant("name", CandidPrimitive.Text(content.Author))})
//    //            },
//    //            {
//    //                CandidTag.FromName("imageLink"),
//    //                new CandidOptional(null)
//    //            },
//    //            {
//    //                CandidTag.FromName("language"),
//    //                new CandidOptional(CandidPrimitive.Text("en-us")) // TODO
//    //            },
//    //            {
//    //                CandidTag.FromName("date"),
//    //                CandidPrimitive.Int(0)
//    //            }
//    //                    }),
//    //                    new CandidRecordType(new Dictionary<CandidTag, CandidType>
//    //                    {
//    //            {
//    //                CandidTag.FromName("title"),
//    //                new CandidPrimitiveType(PrimitiveType.Text)
//    //            },
//    //            {
//    //                CandidTag.FromName("body"),
//    //                new CandidRecordType(new Dictionary<CandidTag, CandidType>
//    //                {
//    //                    {
//    //                        CandidTag.FromName("format"),
//    //                        new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
//    //                    },
//    //                    {
//    //                        CandidTag.FromName("value"),
//    //                        new CandidPrimitiveType(PrimitiveType.Text)
//    //                    }
//    //                })
//    //            },
//    //            {
//    //                CandidTag.FromName("link"),
//    //                new CandidPrimitiveType(PrimitiveType.Text)
//    //            },
//    //            {
//    //                CandidTag.FromName("authors"),
//    //                new CandidVectorType(new CandidVariantType(new Dictionary<CandidTag, CandidType>
//    //                {
//    //                    {
//    //                        CandidTag.FromName("name"),
//    //                        new CandidPrimitiveType(PrimitiveType.Text)
//    //                    },
//    //                    {
//    //                        CandidTag.FromName("identity"),
//    //                        new CandidPrimitiveType(PrimitiveType.Principal)
//    //                    },
//    //                    {
//    //                        CandidTag.FromName("handle"),
//    //                        new CandidPrimitiveType(PrimitiveType.Text)
//    //                    }
//    //                }))
//    //            },
//    //            {
//    //                CandidTag.FromName("imageLink"),
//    //                new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
//    //            },
//    //            {
//    //                CandidTag.FromName("language"),
//    //                new CandidOptionalType(new CandidPrimitiveType(PrimitiveType.Text))
//    //            },
//    //            {
//    //                CandidTag.FromName("date"),
//    //                new CandidPrimitiveType(PrimitiveType.Int)
//    //            }
//    //            })
//    //        )
//    //    );
//    CandidArg arg = await httpAgent.CallAndWaitAsync(canisterId, "push", encodedArgument);
//}
