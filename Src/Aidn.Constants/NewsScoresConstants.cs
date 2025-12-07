namespace Aidn.Constants;

public static class NewsScoresConstants
{
    public static class Types
    {
        public const string HeartRate = "HR";
        public const string RespiratoryRate = "RR";
        public const string BodyTemperature = "TEMP";

        public static readonly HashSet<string> AllMeasurementTypes = [HeartRate, RespiratoryRate, BodyTemperature];
    }

    public static class ErrorCodes
    {
        public const string DuplicatesMeasurementTypes = "DUPLICATE_MEASUREMENT_TYPES";
        public const string MissingMeasurementTypes = "MISSING_MEASUREMENTS_TYPES";
        public const string InvalidMeasurementTypes = "INVALID_MEASUREMENT_TYPES";
        public const string InvalidMeasurementType = "INVALID_MEASUREMENT_TYPE";
        public const string ValueOutOfRange = "VALUE_OUT_OF_RANGE";
    }
}
