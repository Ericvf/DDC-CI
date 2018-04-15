using System;
using System.Collections.Generic;

namespace DDCCI
{
    public class NodeFormatter : INodeFormatter
    {
        private static Dictionary<string, Func<string, string>> lookupTables = new Dictionary<string, Func<string, string>>()
        {
            { "vcp", (i) => FormatVCPControlName(i) },
            { "vcp_14", (i) => FormatVCPColorPreset(i) },
            { "vcp_d6", (i) => FormatVCPPowerControl(i) },
            { "vcp_60", (i) => FormatVCPInputSource(i) }
        };

        static string FormatVCPControlName(string vcpControlName)
        {
            switch (vcpControlName)
            {
                case "00": return "Degauss";    /* ACCESS.bus */
                case "01": return "Degauss";    /* USB */
                case "02": return "Secondary Degauss";  /* ACCESS.bus */
                case "04": return "Reset Factory Defaults";
                case "05": return "SAM: Reset Brightness and Contrast"; /* ??? */
                case "06": return "Reset Factory Geometry";
                case "08": return "Reset Factory Default Color";    /* ACCESS.bus */
                case "0a": return "Reset Factory Default Position"; /* ACCESS.bus */
                case "0c": return "Reset Factory Default Size";     /* ACCESS.bus */
                case "0e": return "SAM: Image Lock Coarse"; /* ??? */
                case "10": return "Brightness";
                case "12": return "Contrast";
                case "14": return "Select Color Preset";    /* ACCESS.bus */
                case "16": return "Red Video Gain";
                case "18": return "Green Video Gain";
                case "1a": return "Blue Video Gain";
                case "1c": return "Focus";  /* ACCESS.bus */
                case "1e": return "SAM: Auto Size Center";  /* ??? */
                case "20": return "Horizontal Position";
                case "22": return "Horizontal Size";
                case "24": return "Horizontal Pincushion";
                case "26": return "Horizontal Pincushion Balance";
                case "28": return "Horizontal Misconvergence";
                case "2a": return "Horizontal Linearity";
                case "2c": return "Horizontal Linearity Balance";
                case "30": return "Vertical Position";
                case "32": return "Vertical Size";
                case "34": return "Vertical Pincushion";
                case "36": return "Vertical Pincushion Balance";
                case "38": return "Vertical Misconvergence";
                case "3a": return "Vertical Linearity";
                case "3c": return "Vertical Linearity Balance";
                case "3e": return "SAM: Image Lock Fine";   /* ??? */
                case "40": return "Parallelogram Distortion";
                case "42": return "Trapezoidal Distortion";
                case "44": return "Tilt (Rotation)";
                case "46": return "Top Corner Distortion Control";
                case "48": return "Top Corner Distortion Balance";
                case "4a": return "Bottom Corner Distortion Control";
                case "4c": return "Bottom Corner Distortion Balance";
                case "50": return "Hue";    /* ACCESS.bus */
                case "52": return "Saturation"; /* ACCESS.bus */
                case "54": return "Color Curve Adjust"; /* ACCESS.bus */
                case "56": return "Horizontal Moire";
                case "58": return "Vertical Moire";
                case "5a": return "Auto Size Center Enable/Disable";    /* ACCESS.bus */
                case "5c": return "Landing Adjust"; /* ACCESS.bus */
                case "5e": return "Input Level Select"; /* ACCESS.bus */
                case "60": return "Input Source Select";
                case "62": return "Audio Speaker Volume Adjust";    /* ACCESS.bus */
                case "64": return "Audio Microphone Volume Adjust"; /* ACCESS.bus */
                case "66": return "On Screen Display Enable/Disable";   /* ACCESS.bus */
                case "68": return "Language Select";    /* ACCESS.bus */
                case "6c": return "Red Video Black Level";
                case "6e": return "Green Video Black Level";
                case "70": return "Blue Video Black Level";
                case "a2": return "Auto Size Center";   /* USB */
                case "a4": return "Polarity Horizontal Synchronization";    /* USB */
                case "a6": return "Polarity Vertical Synchronization";  /* USB */
                case "a8": return "Synchronization Type";   /* USB */
                case "aa": return "Screen Orientation"; /* USB */
                case "ac": return "Horizontal Frequency";   /* USB */
                case "ae": return "Vertical Frequency"; /* USB */
                case "b0": return "Settings";
                case "ca": return "On Screen Display";  /* USB */
                case "cc": return "SAM: On Screen Display Language";    /* ??? */
                case "d4": return "Stereo Mode";    /* USB */
                case "d6": return "SAM: DPMS control (1 - on/4 - stby)";
                case "dc": return "SAM: MagicBright (1 - text/2 - internet/3 - entertain/4 - custom)";
                case "df": return "VCP Version";    /* ??? */
                case "e0": return "SAM: Color preset (0 - normal/1 - warm/2 - cool)";
                case "e1": return "SAM: Power control (0 - off/1 - on)";
                case "ed": return "SAM: Red Video Black Level";
                case "ee": return "SAM: Green Video Black Level";
                case "ef": return "SAM: Blue Video Black Level";
                case "f5": return "SAM: VCP Enable";
                default: return null;
            }
        }

        static string FormatVCPColorPreset(string vcpControlName)
        {
            switch (vcpControlName)
            {
                case "01": return "sRGB";
                case "02": return "Display Native";
                case "03": return "4000 K";
                case "04": return "5000 K";
                case "05": return "6500 K";
                case "06": return "7500 K";
                case "07": return "8200 K";
                case "08": return "9300 K";
                case "09": return "10000 K";
                case "0a": return "11500 K";
                case "0b": return "User 1";
                case "0c": return "User 2";
                case "0d": return "User 3";
                default: return null;
            }
        }

        static string FormatVCPPowerControl(string vcpControlName)
        {
            switch (vcpControlName)
            {
                case "01": return "DPM: On,  DPMS: Off";
                case "04": return "DPM: Off, DPMS: Off";
                case "05": return "Write only value to turn off display";
                default: return null;
            }
        }

        static string FormatVCPInputSource(string vcpControlName)
        {
            switch (vcpControlName)
            {
                case "01": return "VGA-1";
                case "02": return "VGA-2";
                case "03": return "DVI-1";
                case "04": return "DVI-2";
                case "05": return "Composite video 1";
                case "06": return "Composite video 2";
                case "07": return "S-Video-1";
                case "08": return "S-Video-2";
                case "09": return "Tuner-1";
                case "0a": return "Tuner-2";
                case "0b": return "Tuner-3";
                case "0c": return "Component video (YPrPb/YCrCb) 1";
                case "0d": return "Component video (YPrPb/YCrCb) 2";
                case "0e": return "Component video (YPrPb/YCrCb) 3";
                case "0f": return "DisplayPort-1";
                case "10": return "DisplayPort-2";
                case "11": return "HDMI-1";
                case "12": return "HDMI-2";
                default: return null;
            }
        }

        public string FormatNode(INode node)
        {
            var parentKey = node.Parent?.ToString()?.ToLower();
            string result = null;

            if (parentKey != null && lookupTables.ContainsKey(parentKey))
                result = lookupTables[parentKey](node.Value.ToLower());

            return result;
        }
    }
}
