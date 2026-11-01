using System;

namespace RoguelikeToolkit.World2
{
    /// <summary>
    /// A deterministic, procedurally generated planet. Data is computed on demand from the seed,
    /// so planet-sized worlds need no up-front storage.
    /// </summary>
    public sealed class Planet
    {
        /// <summary>Creates a planet from a seed and radius (km).</summary>
        public Planet(int seed, double radiusKm = 6371.0)
        {
            if (radiusKm <= 0) throw new ArgumentOutOfRangeException(nameof(radiusKm));
            Seed = seed;
            RadiusKm = radiusKm;
        }

        /// <summary>Seed that fully determines the planet.</summary>
        public int Seed { get; }

        /// <summary>Planet radius in kilometres.</summary>
        public double RadiusKm { get; }

        /// <summary>
        /// Samples normalized elevation in [0, 1] at the given latitude/longitude (degrees).
        /// </summary>
        public double SampleElevation(double latitudeDeg, double longitudeDeg)
        {
            var lat = latitudeDeg * Math.PI / 180.0;
            var lon = longitudeDeg * Math.PI / 180.0;

            // Sample 3D noise on the unit sphere so there is no seam or polar distortion.
            var x = Math.Cos(lat) * Math.Cos(lon);
            var y = Math.Cos(lat) * Math.Sin(lon);
            var z = Math.Sin(lat);

            double sum = 0, amplitude = 1, frequency = 2, norm = 0;
            for (var octave = 0; octave < 5; octave++)
            {
                sum += amplitude * ValueNoise(x * frequency, y * frequency, z * frequency);
                norm += amplitude;
                amplitude *= 0.5;
                frequency *= 2;
            }

            return sum / norm;
        }

        private double ValueNoise(double x, double y, double z)
        {
            var x0 = (int)Math.Floor(x);
            var y0 = (int)Math.Floor(y);
            var z0 = (int)Math.Floor(z);
            var fx = Smooth(x - x0);
            var fy = Smooth(y - y0);
            var fz = Smooth(z - z0);

            double Lerp(double a, double b, double t) => a + (b - a) * t;
            double C(int dx, int dy, int dz) => Hash(x0 + dx, y0 + dy, z0 + dz);

            return Lerp(
                Lerp(Lerp(C(0, 0, 0), C(1, 0, 0), fx), Lerp(C(0, 1, 0), C(1, 1, 0), fx), fy),
                Lerp(Lerp(C(0, 0, 1), C(1, 0, 1), fx), Lerp(C(0, 1, 1), C(1, 1, 1), fx), fy),
                fz);
        }

        private static double Smooth(double t) => t * t * (3 - 2 * t);

        private double Hash(int x, int y, int z)
        {
            unchecked
            {
                var h = (uint)Seed;
                h = (h ^ (uint)x) * 0x85EBCA6Bu;
                h = (h ^ (uint)y) * 0xC2B2AE35u;
                h = (h ^ (uint)z) * 0x27D4EB2Fu;
                h ^= h >> 15;
                h *= 0x2C1B3C6Du;
                h ^= h >> 13;
                return h / (double)uint.MaxValue;
            }
        }
    }
}
