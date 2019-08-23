﻿using System.Collections.Generic;

namespace Monocle
{
    public static class Monocle
    {
        public static void Run(ref Scan[] Ms1ScansCentroids, Scan ParentScan, ref Scan DependentScan)
        {
            int numIsotopes = 0;
            int monoisotopicIndex = 0;
            int numTheo = 4;
            int left = -7;

            double mass = DependentScan.PrecursorMz * DependentScan.PrecursorCharge;

            // Restrict number of isotopes to consider based on precursor mass.
            if (mass > 2900)
            {
                numIsotopes = 14;
                left = -7;
                numTheo = 7;
            }
            else if (mass > 1200)
            {
                numIsotopes = 10;
                left = -5;
                numTheo = 5;
            }
            else
            {
                numIsotopes = 7;
                left = -3;
                numTheo = 4;
            }

            monoisotopicIndex = -1 * left;

            double precursorMz = DependentScan.PrecursorMz; // This shold be precursoMz?
            int precursorCharge = DependentScan.PrecursorCharge;

            // Search for ion in parent scan, use parent ion mz for future peaks
            int index = PeakMatcher.Match(ParentScan, precursorMz, 50, PeakMatcher.PPM);
            if (index >= 0)
            {
                precursorMz = ParentScan.Centroids[index].Mz;
            }

            // Generate expected relative intensities.
            List<double> expected = PeptideEnvelopeCalculator.GetTheoreticalEnvelope(precursorMz, precursorCharge, numTheo);
            PeptideEnvelopeCalculator.Scale(expected);

            PeptideEnvelope envelope = PeptideEnvelopeExtractor.Extract(Ms1ScansCentroids, precursorMz, precursorCharge, left, numIsotopes);

            // Get best match using pearson correlation.
            double best_score = -1;
            int bestIndex = monoisotopicIndex;
            for (int i = 0; i < (numIsotopes - (expected.Count - 1)); ++i)
            {
                List<double> observed = envelope.averageIntensity.GetRange(i, expected.Count);
                PeptideEnvelopeCalculator.Scale(observed);
                double p = Pearson.P(observed, expected);

                if (p > best_score && p > 0.1)
                {
                    best_score = p;
                    bestIndex = i + 1;
                }
            }

            double newMonoisotopicMz = CalculateMz(envelope.mzs[bestIndex], envelope.intensities[bestIndex]);

            newMonoisotopicMz = (newMonoisotopicMz == 0) ? precursorMz : newMonoisotopicMz;

            DependentScan.MonoisotopicMz = newMonoisotopicMz;
        }

        public static double CalculateMz(List<double> mzs, List<double> intensities)
        {
            if (mzs.Count == 0)
            {
                return 0;
            }

            double totalWeightedMz = 0;
            double totalMz = 0;
            double totalIntensity = 0;
            for (int i = 0; i < mzs.Count; ++i)
            {
                totalWeightedMz += mzs[i] * intensities[i];
                totalMz += mzs[i];
                totalIntensity += intensities[i];
            }
            if (totalIntensity > 0)
            {
                return totalWeightedMz / totalIntensity;
            }
            return totalMz / mzs.Count;
        }
    }
}