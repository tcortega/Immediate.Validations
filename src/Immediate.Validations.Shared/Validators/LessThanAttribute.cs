namespace Immediate.Validations.Shared;

/// <summary>
///	    Applied to a property to indicate that the value should be less than <paramref name="operand"/>.
/// </summary>
/// <param name="operand">
///		The value that the applied property should be less than.
/// </param>
public sealed class LessThanAttribute(
	[TargetType]
	object operand
) : ValidatorAttribute
{
	/// <summary>
	///	    Validates that the value of the applied property is less than <paramref name="operand"/>.
	/// </summary>
	/// <param name="target">
	///	    The value to validate.
	/// </param>
	/// <param name="operand">
	///		The value that the applied property should be less than.
	/// </param>
	/// <returns>
	///	    A <see cref="ValueTuple{T1, T2}"/> indicating whether the property is valid or not, along with an error
	///     message if the property is not valid.
	/// </returns>
	public static (bool Invalid, string? Message) ValidateProperty<T>(T target, T operand) =>
		Comparer<T>.Default.Compare(target, operand) < 0
			? default
			: (true, $"Value '{target}' is not less than '{operand}'");
}
