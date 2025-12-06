using Aidn.NewsScore.Api.Validation;
using FastEndpoints;
using FluentValidation;

namespace Aidn.NewsScore.Api.Endpoints.NewsScores.CreateNewsScore;

public sealed class CreateNewsScoreRequestValidator : Validator<CreateNewsScoreRequest>
{
    private static readonly HashSet<string> _requiredMeasurementTypes = NewsScoresConstants.AllMeasurementTypes;

    private static readonly string _measurementTypesListMessage =
        $"must contain exactly one of each of the following: {_requiredMeasurementTypes.ToValidationMessageFormat()}";

    public CreateNewsScoreRequestValidator()
    {
        RuleFor(x => x.Measurements)
            .Must(HaveNoMissingTypes)
            .WithMessage(request =>
            {
                var providedTypes = request.Measurements.Select(m => m.Type).Distinct().ToHashSet();
                var missingTypes = _requiredMeasurementTypes.Where(required => !providedTypes.Contains(required));
                return $"'Measurements' {_measurementTypesListMessage}. Missing type(s): {missingTypes.ToValidationMessageFormat()}";
            });

        RuleFor(x => x.Measurements)
            .Must(HaveNoDuplicateTypes)
            .WithMessage(request =>
            {
                var duplicateTypes = request.Measurements.GroupBy(m => m.Type).Where(g => g.Count() > 1).Select(g => g.Key);
                return $"'Measurements' {_measurementTypesListMessage}. Duplicate type(s): {duplicateTypes.ToValidationMessageFormat()}";
            });

        RuleFor(x => x.Measurements)
            .Must(HaveNoUnexpectedTypes)
            .WithMessage(request =>
            {
                var providedTypes = request.Measurements.Select(m => m.Type).Distinct();
                var unexpectedTypes = providedTypes.Except(_requiredMeasurementTypes);
                return $"'Measurements' {_measurementTypesListMessage}. Unknown type(s): {unexpectedTypes.ToValidationMessageFormat()}";
            });

        RuleForEach(x => x.Measurements)
            .ChildRules(measurement =>
            {
                measurement
                    .RuleFor(m => m.Type)
                    .Must(BeValidMeasurementType)
                    .WithMessage(m =>
                        $"Measurement type must be one of: {_requiredMeasurementTypes.ToValidationMessageFormat()}. You input '{m.Type}'"
                    );
            });
    }

    private static bool HaveNoMissingTypes(IEnumerable<Measurement> measurements)
    {
        var providedTypes = measurements.Select(m => m.Type).Distinct().ToHashSet();
        return _requiredMeasurementTypes.All(required => providedTypes.Contains(required));
    }

    private static bool HaveNoDuplicateTypes(IEnumerable<Measurement> measurements)
    {
        var types = measurements.Select(m => m.Type).ToList();
        return types.Count == types.Distinct().Count();
    }

    private static bool HaveNoUnexpectedTypes(IEnumerable<Measurement> measurements)
    {
        var providedTypes = measurements.Select(m => m.Type).Distinct();
        return !providedTypes.Except(_requiredMeasurementTypes).Any();
    }

    private static bool BeValidMeasurementType(string type)
    {
        return _requiredMeasurementTypes.Contains(type);
    }
}
