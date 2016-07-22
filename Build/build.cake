#tool "nuget:?package=NUnit.ConsoleRunner&version=3.2.1"
#tool "nuget:?package=GitVersion.CommandLine"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var zipDir = Argument("zipDir","./");
var outputDirectory = Argument("OutputDirectory", "./");
var buildScriptsDirectory = Argument("BuildScriptsDirectory", "./");
var majorVersion = Argument("MajVer", "1");
var minorVersion = Argument("MinVer", "2");
var patchNumber = Argument("Patch", "3");
GitVersion versionInfo = null;

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    Information("Cleaning...");
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    Information("Restoring nuget packages...");
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    Information("Building Solution...");
    Information("ZipDir: " + zipDir);
    Information("OutputDirectory: " + outputDirectory);
    Information("MajorVersion: " + majorVersion);
    Information("MinorVersion: " + minorVersion);
    Information("Patch: " + patchNumber);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Running Unit Tests...");
    
    versionInfo = GitVersion(new GitVersionSettings{ OutputType = GitVersionOutput.Json });
});

Task("Patch-Assembly-Info")
    .Does(() =>
{
	Information("Patching Assembly Info...");
});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

Task("Build-Nuget")
    .IsDependentOn("Run-Unit-Tests")
    .Does(() =>
{
    Information("Running Unit Tests...");
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
