using Immediate.Validations.Shared;
using Xunit;

namespace Immediate.Validations.FunctionalTests.Validators;

public sealed partial class GreaterThanOrEqualTests
{
	[Validate]
	public partial record IntGreaterThanRecord : IValidationTarget<IntGreaterThanRecord>
	{
		[GreaterThanOrEqual(0)]
		public required int IntValue { get; init; }
	}

	[Validate]
	public partial record StringGreaterThanRecord : IValidationTarget<StringGreaterThanRecord>
	{
		[GreaterThanOrEqual(nameof(KeyValue))]
		public required string StringValue { get; init; }
		public required string KeyValue { get; init; }
	}

	[Fact]
	public void IntGreaterThanRecordWhenZero()
	{
		var instance = new IntGreaterThanRecord { IntValue = 0 };

		var errors = IntGreaterThanRecord.Validate(instance);

		Assert.Empty(errors);
	}

	[Fact]
	public void IntGreaterThanRecordWhenNotZero()
	{
		var instance = new IntGreaterThanRecord { IntValue = -1 };

		var errors = IntGreaterThanRecord.Validate(instance);

		Assert.Equal(
			[
				new()
				{
					PropertyName = nameof(IntGreaterThanRecord.IntValue),
					ErrorMessage = "Value '-1' is not greater than or equal to '0'",
				}
			],
			errors
		);
	}

	[Fact]
	public void StringGreaterThanRecordWhenGreaterThan()
	{
		var instance = new StringGreaterThanRecord { StringValue = "Foo", KeyValue = "Foo" };

		var errors = StringGreaterThanRecord.Validate(instance);

		Assert.Empty(errors);
	}

	[Fact]
	public void StringGreaterThanRecordWhenNotGreaterThan()
	{
		var instance = new StringGreaterThanRecord { StringValue = "Bar", KeyValue = "Foo" };

		var errors = StringGreaterThanRecord.Validate(instance);

		Assert.Equal(
			[
				new()
				{
					PropertyName = nameof(StringGreaterThanRecord.StringValue),
					ErrorMessage = "Value 'Bar' is not greater than or equal to 'Foo'",
				}
			],
			errors
		);
	}
}
