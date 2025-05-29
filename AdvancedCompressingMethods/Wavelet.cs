using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedCompressingMethods
{
    internal class Wavelet
    {
        public double[] analysisLow;
        public double[] analysisHigh;
        public double[] synthesisLow;
        public double[] synthesisHigh;

        public Wavelet()
        {
            this.analysisLow = [
                0.026748757411,
                -0.016864118443,
                -0.078223266529,
                0.266864118443,
                0.602949018236,
                0.266864118443,
                -0.078223266529,
                -0.016864118443,
                0.026748757411
                ];
            this.analysisHigh = [
                0.0,
                0.091271763114,
                -0.057543526229,
                -0.591271763114,
                1.115087052457,
                -0.591271763114,
                -0.057543526229,
                0.091271763114,
                0.0
                ];
            this.synthesisLow = [
                0.0,
                -0.091271763114,
                -0.057543526229,
                0.591271763114,
                1.115087052457,
                0.591271763114,
                -0.057543526229,
                -0.091271763114,
                0.0
                ];
            this.synthesisHigh = [
                0.026748757411,
                0.016864118443,
                -0.078223266529,
                -0.266864118443,
                0.602949018236,
                -0.266864118443,
                -0.078223266529,
                0.016864118443,
                0.026748757411
                ];
        }
    }
}
