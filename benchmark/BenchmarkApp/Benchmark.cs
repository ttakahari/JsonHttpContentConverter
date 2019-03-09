using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using JsonHttpContentConverter.Jil;
using JsonHttpContentConverter.JsonNet;
using JsonHttpContentConverter.Utf8Json;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BenchmarkApp
{
    [Config(typeof(BenchmarkConfig))]
    public class Benchmark
    {
        private readonly JsonNetHttpContentConverter _jsonNet;
        private readonly JilHttpContentConverter _jil;
        private readonly Utf8JsonHttpContentConverter _utf8Json;

        private readonly Payload _payload;
        private readonly HttpContent _content;

        public Benchmark()
        {
            _jsonNet = new JsonNetHttpContentConverter();
            _jil = new JilHttpContentConverter();
            _utf8Json = new Utf8JsonHttpContentConverter();
            // Slack Incoming WebHook Json
            _payload = new Payload
            {
                Text = "Robert DeSoto added a new task",
                Attachments = new[]
                {
                    new Attachment
                    {
                        Fallback = "Plan a vacation",
                        AuthorName = "Owner: rdesoto",
                        Title = "Plan a vacation",
                        Text = "I've been working too hard, it's time for a break.",
                        Actions = new[]
                        {
                            new Action
                            {
                                Name = "action",
                                Type = "button",
                                Text = "Complete this task",
                                Style = "",
                                Value = "complete"
                            },
                            new Action
                            {
                                Name = "tag_list",
                                Type = "select",
                                Text = "Add a tag...",
                                DataSource = "static",
                                Options = new[]
                                {
                                    new Option
                                    {
                                        Text = "Launch Blocking",
                                        Value = "launch-blocking"
                                    },
                                    new Option
                                    {
                                        Text = "Enhancement",
                                        Value = "enhancement"
                                    },
                                    new Option
                                    {
                                        Text = "Bug",
                                        Value = "bug"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            _content = new StringContent(@"{
    ""text"": ""Robert DeSoto added a new task"",
    ""attachments"": [
        {
            ""fallback"": ""Plan a vacation"",
            ""author_name"": ""Owner: rdesoto"",
            ""title"": ""Plan a vacation"",
            ""text"": ""I've been working too hard, it's time for a break."",
            ""actions"": [
                {
                    ""name"": ""action"",
                    ""type"": ""button"",
                    ""text"": ""Complete this task"",
                    ""style"": """",
                    ""value"": ""complete""
                },
                {
                    ""name"": ""tags_list"",
                    ""type"": ""select"",
                    ""text"": ""Add a tag..."",
                    ""data_source"": ""static"",
                    ""options"": [
                        {
                            ""text"": ""Launch Blocking"",
                            ""value"": ""launch-blocking""
                        },
                        {
                            ""text"": ""Enhancement"",
                            ""value"": ""enhancement""
                        },
                        {
                            ""text"": ""Bug"",
                            ""value"": ""bug""
                        }
                    ]
                }
            ]
        }
    ]
}");
        }

        [Benchmark]
        public void ToJsonHttpContent_JsonNet()
        {
            var _ = _jsonNet.ToJsonHttpContent(_payload);
        }

        [Benchmark]
        public void ToJsonHttpContent_Jil()
        {
            var _ = _jil.ToJsonHttpContent(_payload);
        }

        [Benchmark]
        public void ToJsonHttpContent_Utf8Json()
        {
            var _ = _utf8Json.ToJsonHttpContent(_payload);
        }

        [Benchmark]
        public async Task FromJsonHttpContent_JsonNet()
        {
            var _ = await _jsonNet.FromJsonHttpContent<Payload>(_content);
        }

        [Benchmark]
        public async Task FromJsonHttpContent_Jil()
        {
            var _ = await _jil.FromJsonHttpContent<Payload>(_content);
        }

        [Benchmark]
        public async Task FromJsonHttpContent_Utf8Json()
        {
            var _ = await _utf8Json.FromJsonHttpContent<Payload>(_content);
        }
    }

    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            Add(MarkdownExporter.GitHub); // ベンチマーク結果を書く時に出力させとくとベンリ
            Add(MemoryDiagnoser.Default);

            // ShortRunを使うとサクッと終わらせられる、デフォルトだと本気で長いので短めにしとく。
            // ShortRunは LaunchCount=1  TargetCount=3 WarmupCount = 3 のショートカット
            Add(Job.ShortRun);
        }
    }

    [DataContract]
    public class Payload
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "attachments")]
        public Attachment[] Attachments { get; set; }
    }

    [DataContract]
    public class Attachment
    {
        [DataMember(Name = "fallback")]
        public string Fallback { get; set; }

        [DataMember(Name = "author_name")]
        public string AuthorName { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "actions")]
        public Action[] Actions { get; set; }
    }

    [DataContract]
    public class Action
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "style")]
        public string Style { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "data_source")]
        public string DataSource { get; set; }

        [DataMember(Name = "options")]
        public Option[] Options { get; set; }
    }

    [DataContract]
    public class Option
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
