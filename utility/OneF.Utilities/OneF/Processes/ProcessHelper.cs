// Copyright (c) Maple512. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneF.Processes
{
    public static class ProcessHelper
    {
        /// <summary>
        /// 执行命令行
        /// </summary>
        /// <param name="throwOnError">执行中出现异常时，是否抛出异常，中断执行过程。默认：true</param>
        /// <returns></returns>
        public static async Task<ProcessResult> RunAsync(
            string filename,
            string? arguments = null,
            string? workingDirectory = null,
            bool throwOnError = true,
            IDictionary<string, string>? environments = null,
            Action<string>? outputDataReceived = null,
            Action<string>? errorDataReceived = null,
            Action<int>? onStart = null,
            Action<int>? onStop = null,
            CancellationToken cancellationToken = default)
        {
            using var process = new Process()
            {
                StartInfo =
                {
                    FileName = filename,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    WorkingDirectory = workingDirectory
                },
                EnableRaisingEvents = true,
            };

            if (environments != null)
            {
                foreach (var kvp in environments)
                {
                    process.StartInfo.Environment.Add(kvp!);
                }
            }

            var outputBuilder = new StringBuilder();
            process.OutputDataReceived += (_, e) =>
            {
                if (e.Data == null)
                {
                    return;
                }

                if (outputDataReceived != null)
                {
                    outputDataReceived.Invoke(e.Data);
                }
                else
                {
                    outputBuilder.AppendLine(e.Data);
                }
            };

            var errorBuilder = new StringBuilder();
            process.ErrorDataReceived += (_, e) =>
            {
                if (e.Data == null)
                {
                    return;
                }

                if (errorDataReceived != null)
                {
                    errorDataReceived.Invoke(e.Data);
                }
                else
                {
                    errorBuilder.AppendLine(e.Data);
                }
            };

            var processLifetimeTask = new TaskCompletionSource<ProcessResult>();

            process.Exited += (_, e) =>
            {
                if (throwOnError && process.ExitCode != 0)
                {
                    processLifetimeTask.TrySetException(new InvalidOperationException($"Command {filename} {arguments} returned exit code {process.ExitCode}"));
                }
                else
                {
                    processLifetimeTask.TrySetResult(new ProcessResult(outputBuilder.ToString(), errorBuilder.ToString(), process.ExitCode));
                }
            };

            process.Start();
            onStart?.Invoke(process.Id);

            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync(cancellationToken);

            var cancelledTcs = new TaskCompletionSource<object?>();
            await using var _ = cancellationToken.Register(() => cancelledTcs.TrySetResult(null));

            var result = await Task.WhenAny(processLifetimeTask.Task, cancelledTcs.Task);

            if (result == cancelledTcs.Task)
            {
                if (!process.CloseMainWindow())
                {
                    process.Kill();
                }

                if (!process.HasExited)
                {
                    var cancel = new CancellationTokenSource();
                    await Task.WhenAny(processLifetimeTask.Task, Task.Delay(TimeSpan.FromSeconds(5), cancel.Token));
                    cancel.Cancel();

                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
            }

            var processResult = await processLifetimeTask.Task;
            onStop?.Invoke(processResult.ExitCode);
            return processResult;
        }
    }
}
