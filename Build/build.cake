#tool nuget:?package=NUnit.ConsoleRunner&version=3.2.1
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var zipDir = Argument("zipDir","./");
var outputDirectory = Argument("OutputDirectory", "./");

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
    Information(zipDir);
    Information(outputDirectory);
});

Task("Run-Unit-Tests")
    .IsDependentOn("Build")
    .Does(() =>
{
    Information("Running Unit Tests...");
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
