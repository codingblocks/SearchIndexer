using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CommandLineParser.Arguments;

namespace App
{
    public class ArgumentParsingTarget
    {
        private const string Running_Modes = "get;create;update;delete";

        private const string Running_Modes_Helper =
            "Specify the running mode of the application with \"" + Running_Modes + "\""; // can't use string interpolation on a const D:

        [EnumeratedValueArgument(typeof(RunningMode), 'm', "mode", AllowedValues = Running_Modes, Optional = false, 
            Description = Running_Modes_Helper)]
        public RunningMode RunningMode;

        [SwitchArgument('u', "usage", true, Description = "Information on invoking app.")]
        public bool Help;
    }
}
