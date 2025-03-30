namespace CalculateTaxes.Domain.Dtos.FeatureFlag
{
    public record FeatureFlagResponse(int Id, string Name, bool Active, DateTime CreatedAt, DateTime? UpdatedAt);
}