using System;
using System.Diagnostics;
using System.IO;

namespace CollaborativeCodingServer.Services
{
    public class CompileService
    {
        public CompileResult CompileCode(string content, int fileID)
        {
            string rootPath = Path.Combine(Path.GetTempPath(), "CollaborativeCodingHubCompile");
            string workPath = Path.Combine(rootPath, Guid.NewGuid().ToString());

            try
            {
                Directory.CreateDirectory(workPath);

                string projectFile = Path.Combine(workPath, "CompileTemp.csproj");
                string sourceFile = Path.Combine(workPath, $"File_{fileID}.cs");

                File.WriteAllText(projectFile,
                     "<Project Sdk=\"Microsoft.NET.Sdk\">\n" +
                     "  <PropertyGroup>\n" +
                     "    <OutputType>Exe</OutputType>\n" +
                     "    <TargetFramework>net8.0</TargetFramework>\n" +
                     "    <ImplicitUsings>enable</ImplicitUsings>\n" +
                     "    <Nullable>enable</Nullable>\n" +
                     "  </PropertyGroup>\n" +
                     "</Project>\n");

                File.WriteAllText(sourceFile, content);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "build --nologo --configuration Release",
                    WorkingDirectory = workPath,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = Process.Start(startInfo)!;
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                bool success = process.ExitCode == 0;

                string combined = string.IsNullOrWhiteSpace(error)
                    ? output.Trim()
                    : output.Trim() + "\n" + error.Trim();

                if (success)
                {
                    ProcessStartInfo runInfo = new ProcessStartInfo
                    {
                        FileName = "dotnet",
                        Arguments = "run --no-build --configuration Release",
                        WorkingDirectory = workPath,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    using Process runProcess = Process.Start(runInfo)!;
                    string runOutput = runProcess.StandardOutput.ReadToEnd();
                    string runError = runProcess.StandardError.ReadToEnd();
                    runProcess.WaitForExit();
                    string result = combined;
                    if (!string.IsNullOrWhiteSpace(runOutput))
                    {
                        result += "\n\n===== PROGRAM OUTPUT =====\n";
                        result += runOutput;
                    }
                    if (!string.IsNullOrWhiteSpace(runError))
                    {
                        result += "\n\n===== RUNTIME ERROR =====\n";
                        result += runError;
                    }
                    return new CompileResult(true, result);
                }
                return new CompileResult(false, string.IsNullOrWhiteSpace(combined) ? "Compilation failed." : combined);
            }
            catch (Exception ex)
            {
                return new CompileResult(false, ex.Message);
            }
            finally
            {
                try
                {
                    if (Directory.Exists(workPath))
                        Directory.Delete(workPath, true);
                }
                catch
                {
                }
            }
        }
    }

    public class CompileResult
    {
        public CompileResult(bool success, string output)
        {
            Success = success;
            Output = output;
        }

        public bool Success { get; }
        public string Output { get; }
    }
}
