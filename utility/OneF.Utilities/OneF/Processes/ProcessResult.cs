// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace OneF.Processes
{
    public class ProcessResult
    {
        public ProcessResult(string standardOutput, string standardError, int exitCode)
        {
            StandardOutput = standardOutput;
            StandardError = standardError;
            ExitCode = exitCode;
        }

        public string StandardOutput { get; }

        public string StandardError { get; }

        public int ExitCode { get; }
    }
}
