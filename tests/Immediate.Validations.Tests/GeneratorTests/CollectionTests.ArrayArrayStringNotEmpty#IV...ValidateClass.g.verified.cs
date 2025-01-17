﻿//HintName: IV...ValidateClass.g.cs
using System.Collections.Generic;
using Immediate.Validations.Shared;

#nullable enable
#pragma warning disable CS1591


partial class ValidateClass
{
	static List<ValidationError> IValidationTarget<ValidateClass>.Validate(ValidateClass? target) =>
		Validate(target);

	public static  List<ValidationError> Validate(ValidateClass? target)
	{
		if (target is not { } t)
		{
			return 
			[
				new()
				{
					PropertyName = ".self",
					ErrorMessage = "`target` must not be `null`.",
				},
			];
		}
		
		var errors = new List<ValidationError>();


		__ValidateStringProperty(errors, t, t.StringProperty);


		return errors;
	}



	private static void __ValidateStringProperty00(
		List<ValidationError> errors, ValidateClass instance, string target, int counter0, int counter1
	)
	{

		if (target is not { } t)
		{
			errors.Add(new()
			{
				PropertyName = $"StringProperty[{counter0}][{counter1}]",
				ErrorMessage = "Property must not be `null`.",
			});

			return;
		}



		errors.Add(
			global::Immediate.Validations.Shared.NotEmptyAttribute.ValidateProperty(
				t
			),
			$"StringProperty[{counter0}][{counter1}]",
			null
		);
	}

	private static void __ValidateStringProperty0(
		List<ValidationError> errors, ValidateClass instance, string[] target, int counter0
	)
	{

		if (target is not { } t)
		{
			errors.Add(new()
			{
				PropertyName = $"StringProperty[{counter0}]",
				ErrorMessage = "Property must not be `null`.",
			});

			return;
		}


		var counter1 = 0;
		foreach (var item1 in t)
		{
			__ValidateStringProperty00(
				errors, instance, item1, counter0, counter1
			);
			counter1++;
		}

		errors.Add(
			global::Immediate.Validations.Shared.NotEmptyAttribute.ValidateProperty(
				t
			),
			$"StringProperty[{counter0}]",
			null
		);
	}

	private static void __ValidateStringProperty(
		List<ValidationError> errors, ValidateClass instance, string[][] target
	)
	{

		if (target is not { } t)
		{
			errors.Add(new()
			{
				PropertyName = $"StringProperty",
				ErrorMessage = "Property must not be `null`.",
			});

			return;
		}


		var counter0 = 0;
		foreach (var item0 in t)
		{
			__ValidateStringProperty0(
				errors, instance, item0, counter0
			);
			counter0++;
		}

		errors.Add(
			global::Immediate.Validations.Shared.NotEmptyAttribute.ValidateProperty(
				t
			),
			$"StringProperty",
			null
		);
	}

}

