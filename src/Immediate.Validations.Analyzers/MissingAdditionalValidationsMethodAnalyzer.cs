using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Immediate.Validations.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class MissingAdditionalValidationsMethodAnalyzer : DiagnosticAnalyzer
{
	public static readonly DiagnosticDescriptor AdditionalValidationsMissing =
		new(
			id: DiagnosticIds.IV0017AdditionalValidationsMissing,
			title: "Validation target is missing an `AdditionalValidations` method",
			messageFormat: "Validation target `{0}` does not have an `AdditionalValidations` method",
			category: "ImmediateValidations",
			defaultSeverity: DiagnosticSeverity.Hidden,
			isEnabledByDefault: true,
			description: "An `AdditionalValidations` allows the developer to add validations not capable via attributes."
		);

	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
		ImmutableArray.Create(
		[
			AdditionalValidationsMissing,
		]);

	public override void Initialize(AnalysisContext context)
	{
		if (context == null)
			throw new ArgumentNullException(nameof(context));

		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.EnableConcurrentExecution();

		context.RegisterSymbolAction(AnalyzeSymbol, SymbolKind.NamedType);
	}

	private void AnalyzeSymbol(SymbolAnalysisContext context)
	{
		var token = context.CancellationToken;
		token.ThrowIfCancellationRequested();

		var symbol = (INamedTypeSymbol)context.Symbol;

		var hasValidateAttribute = symbol
			.GetAttributes()
			.Any(a => a.AttributeClass.IsValidateAttribute());

		var isIValidationTarget = symbol
			.Interfaces
			.Any(i => i.IsIValidationTarget());

		if (!hasValidateAttribute && !isIValidationTarget)
			return;

		if (!symbol.HasAdditionalValidationsMethod())
		{
			context.ReportDiagnostic(
				Diagnostic.Create(
					AdditionalValidationsMissing,
					symbol.Locations[0],
					symbol.Name
				)
			);
		}
	}
}

file static class Extensions
{
	public static bool HasAdditionalValidationsMethod(this INamedTypeSymbol typeSymbol) =>
		typeSymbol.GetMembers()
			.OfType<IMethodSymbol>()
			.Any(m =>
				m is
				{
					Name: "AdditionalValidations",
					IsStatic: true,
					ReturnsVoid: true,
					Parameters:
					[
					{
						Type: INamedTypeSymbol
						{
							ConstructedFrom: INamedTypeSymbol
							{
								MetadataName: "List`1",
								ContainingNamespace:
								{
									Name: "Generic",
									ContainingNamespace:
									{
										Name: "Collections",
										ContainingNamespace:
										{
											Name: "System",
											ContainingNamespace.IsGlobalNamespace: true,
										},
									},
								},
							},
							TypeArguments:
							[
								INamedTypeSymbol
							{
								Name: "ValidationError",
								ContainingNamespace:
								{
									Name: "Shared",
									ContainingNamespace:
									{
										Name: "Validations",
										ContainingNamespace:
										{
											Name: "Immediate",
											ContainingNamespace.IsGlobalNamespace: true,
										},
									},
								},
							},
							],
						},
					},
					{ Type: INamedTypeSymbol parameterType },
					],
				}
				&& SymbolEqualityComparer.Default.Equals(parameterType, typeSymbol)
			);
}
