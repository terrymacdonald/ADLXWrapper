using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Managed equivalent of ADLX_IntRange. Safe for JSON serialisation; contains no native pointers.
    /// </summary>
    public readonly struct IntRangeDto
    {
        public int MinValue { get; init; }
        public int MaxValue { get; init; }
        public int Step { get; init; }

        [JsonConstructor]
        public IntRangeDto(int minValue, int maxValue, int step)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Step = step;
        }

        internal static IntRangeDto FromNative(ADLX_IntRange r) =>
            new IntRangeDto(r.minValue, r.maxValue, r.step);
    }

    /// <summary>
    /// Managed equivalent of ADLX_LUID. Safe for JSON serialisation; contains no native pointers.
    /// </summary>
    public readonly struct LuidDto
    {
        public uint LowPart { get; init; }
        public int HighPart { get; init; }

        [JsonConstructor]
        public LuidDto(uint lowPart, int highPart)
        {
            LowPart = lowPart;
            HighPart = highPart;
        }

        internal static LuidDto FromNative(ADLX_LUID l) =>
            new LuidDto(l.lowPart, l.highPart);
    }

    /// <summary>
    /// Managed equivalent of ADLX_RegammaCoeff. Safe for JSON serialisation; contains no native pointers.
    /// </summary>
    public readonly struct RegammaCoeffDto
    {
        public int CoefficientA0 { get; init; }
        public int CoefficientA1 { get; init; }
        public int CoefficientA2 { get; init; }
        public int CoefficientA3 { get; init; }
        public int Gamma { get; init; }

        [JsonConstructor]
        public RegammaCoeffDto(int coefficientA0, int coefficientA1, int coefficientA2, int coefficientA3, int gamma)
        {
            CoefficientA0 = coefficientA0;
            CoefficientA1 = coefficientA1;
            CoefficientA2 = coefficientA2;
            CoefficientA3 = coefficientA3;
            Gamma = gamma;
        }

        internal static RegammaCoeffDto FromNative(ADLX_RegammaCoeff c) =>
            new RegammaCoeffDto(c.coefficientA0, c.coefficientA1, c.coefficientA2, c.coefficientA3, c.gamma);

        internal ADLX_RegammaCoeff ToNative() => new ADLX_RegammaCoeff
        {
            coefficientA0 = CoefficientA0,
            coefficientA1 = CoefficientA1,
            coefficientA2 = CoefficientA2,
            coefficientA3 = CoefficientA3,
            gamma = Gamma
        };
    }

    /// <summary>
    /// Managed equivalent of ADLX_GammaRamp (768-entry ushort LUT). Safe for JSON serialisation; contains no native pointers.
    /// </summary>
    public readonly struct GammaRampDto
    {
        private readonly IReadOnlyList<ushort> _values;

        /// <summary>
        /// The 768 gamma ramp entries. Returns an empty list when the ramp has not been populated.
        /// </summary>
        public IReadOnlyList<ushort> Values => _values ?? System.Array.Empty<ushort>();

        [JsonConstructor]
        public GammaRampDto(IReadOnlyList<ushort> values)
        {
            _values = values;
        }

        internal static GammaRampDto FromNative(ADLX_GammaRamp ramp)
        {
            var values = new List<ushort>(768);
            for (int i = 0; i < 768; i++)
                values.Add(ramp.gamma[i]);
            return new GammaRampDto(values);
        }

        internal ADLX_GammaRamp ToNative()
        {
            var ramp = default(ADLX_GammaRamp);
            var vals = Values;
            int count = System.Math.Min(vals.Count, 768);
            for (int i = 0; i < count; i++)
                ramp.gamma[i] = vals[i];
            return ramp;
        }
    }

    /// <summary>
    /// Managed equivalent of ADLX_Point. Safe for JSON serialisation; contains no native pointers.
    /// </summary>
    public readonly struct PointDto
    {
        public int X { get; init; }
        public int Y { get; init; }

        [JsonConstructor]
        public PointDto(int x, int y)
        {
            X = x;
            Y = y;
        }

        internal static PointDto FromNative(ADLX_Point p) =>
            new PointDto(p.x, p.y);

        internal ADLX_Point ToNative() => new ADLX_Point { x = X, y = Y };
    }

    /// <summary>
    /// Managed equivalent of ADLX_GamutColorSpace. Safe for JSON serialisation; contains no native pointers.
    /// </summary>
    public readonly struct GamutColorSpaceDto
    {
        public PointDto Red { get; init; }
        public PointDto Green { get; init; }
        public PointDto Blue { get; init; }

        [JsonConstructor]
        public GamutColorSpaceDto(PointDto red, PointDto green, PointDto blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        internal static GamutColorSpaceDto FromNative(ADLX_GamutColorSpace cs) =>
            new GamutColorSpaceDto(
                PointDto.FromNative(cs.red),
                PointDto.FromNative(cs.green),
                PointDto.FromNative(cs.blue));

        internal ADLX_GamutColorSpace ToNative() => new ADLX_GamutColorSpace
        {
            red = Red.ToNative(),
            green = Green.ToNative(),
            blue = Blue.ToNative()
        };
    }
}
