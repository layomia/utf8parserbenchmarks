using System;
using System.Text;
using System.Buffers.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace MyBenchMark
{
	[ClrJob(baseline: true), CoreJob]//, CoreRtJob]
	[RPlotExporter, RankColumn]
	public class Utf8ParserBenchmark
	{
		[Params("1997-07-16", "1997-07-16T19:20", "1997-07-16T19:20:30", "1997-07-16T19:20:30.45", "1997-07-16T19:20+01:00", "1997-07-16T19:20:30+01:00")]
		public string TestString;
		byte[] Utf8ByteArray;

		[GlobalSetup]
		public void Setup()
		{
			Utf8ByteArray = Encoding.UTF8.GetBytes(TestString);
		}

		[Benchmark]
		public DateTime ByteSpanToDateTimeJ()
		{
			ReadOnlySpan<byte> utf8ByteSpan = Utf8ByteArray;
			Utf8Parser.TryParse(utf8ByteSpan, out DateTime value, out int bytesConsumed, 'J');
			return value;
		}

		[Benchmark]
		public DateTime ByteSpanToDateTimeO()
		{
			ReadOnlySpan<byte> utf8ByteSpan = Utf8ByteArray;
			Utf8Parser.TryParse(utf8ByteSpan, out DateTime value, out int bytesConsumed, 'O');
			return value;
		}

		[Benchmark]
		public DateTimeOffset ByteSpanToDateTimeOffsetJ()
		{
			ReadOnlySpan<byte> utf8ByteSpan = Utf8ByteArray;
			Utf8Parser.TryParse(utf8ByteSpan, out DateTimeOffset value, out int bytesConsumed, 'J');
			return value;
		}

		[Benchmark]
		public DateTimeOffset ByteSpanToDateTimeOffsetO()
		{
			ReadOnlySpan<byte> utf8ByteSpan = Utf8ByteArray;
			Utf8Parser.TryParse(utf8ByteSpan, out DateTimeOffset value, out int bytesConsumed, 'O');
			return value;
		}
	}

    public class Program
    {
		static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
	}
}
