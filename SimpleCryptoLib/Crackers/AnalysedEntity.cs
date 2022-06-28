using System;

namespace SimpleCryptoLib.Crackers;

public abstract class AnalysedEntity
{
    private const int PercentageDelta = 100; // Makes reading the percentages easier.
        
    /// <summary>
    /// Number of times the entity appears within the source.
    /// </summary>
    public int OccurenceCount { get; set; }
        
    /// <summary>
    /// Percentage of the text that the character makes up.
    /// </summary>
    public decimal Frequency { get; set; }

    protected AnalysedEntity()
    {
        OccurenceCount = 0;
        Frequency = 0.0m;
    }

    /// <summary>
    /// Calculates the percentage of total occurance count, that a single analysed entity composes.
    /// </summary>
    /// <param name="totalOccuranceCount">Number of times entity appears within the context.</param>
    /// <returns>Frequency percentage</returns>
    /// <exception cref="ArgumentOutOfRangeException"><param name="totalOccuranceCount"></param> is below or
    /// equal to zero.</exception>
    public void CalculateFrequency(int totalOccuranceCount)
    {
        // Avoid division by zero exception
        if (totalOccuranceCount <= 0) { throw new ArgumentOutOfRangeException(nameof(totalOccuranceCount)); }

        Frequency = (Convert.ToDecimal(OccurenceCount) / Convert.ToDecimal(totalOccuranceCount))
                    * PercentageDelta;
    }
}