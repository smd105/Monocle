﻿using CommandLine;
using Monocle;

namespace MakeMono
{
    /// <summary>
    /// MakeMono Input options
    /// </summary>
    public class MakeMonoOptions
    {
        [Option('f', "File", Required = true, HelpText = "Input file for monoisotopic peak correction")]
        public string InputFilePath { get; set; } = "";

        [Option('n', "NumOfScans", Required = false, HelpText = "The number of scans to average, default: +/- 6")]
        public int NumOfScans { get; set; } = 12;

        [Option('c', "ChargeDetection", Required = false, HelpText = "Toggle charge detection, default: false | F")]
        public bool ChargeDetection { get; set; } = false;

        [Option('z', "ChargeRange", Required = false, HelpText = "Range for Charge Detection, if enabled. default: 2:6")]
        public string ChargeRange { get; set; } = "2:6";

        [Option('u', "ChargesForUnknown", Required = false, HelpText = "For low-res scans, output multiple precursors with these charges. default: 2:3")]
        public string ChargeRangeUnknown { get; set; } = "2:3";

        [Option('w', "ForceCharges", Required = false, HelpText = "Output multiple precursors with charges set by -u even if charge is known. default: false")]
        public bool ForceCharges { get; set; } = false;

        [Option('m', "MsLevel", Required = false, HelpText = "Select the MS level at which monoisotopic m/z will be adjusted.")]
        public int MS_Level { get; set; } = 2;

        [Option('q', "QuietRun", Required = false, HelpText = "Do not display file progress in console.")]
        public bool RunQuiet { get; set; } = false;

        [Option('t', "OutputFileType", Required = false, HelpText = "Choose to output an mzXML \"mzxml\" or CSV file \"csv\".")]
        public OutputFileType OutputFileType { get; set; } = OutputFileType.csv;

        [Option('o', "OutputFilePath", Required = false, HelpText = "File to write. Include directory, filename, and extension")]
        public string OutputFilePath { get; set; } = "";

        [Option('d', "Debug", Hidden = true, Required = false, HelpText = "Verbose debug output.")]
        public bool WriteDebug { get; set; } = false;

        [Option('s', "WriteSps", Hidden = true, Required = false, HelpText = "Write SPS ions as independent precursors.")]
        public bool WriteSps { get; set; } = false;

        [Option('x', "ConvertOnly", Hidden = true, Required = false, HelpText = "Write output file without modifying precursors.")]
        public bool ConvertOnly { get; set; } = false;
    }
}
