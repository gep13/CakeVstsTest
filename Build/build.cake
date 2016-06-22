#tool nuget:?package=NUnit.ConsoleRunner&version=3.2.1
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var root = "../";
var projectRoot =  MakeAbsolute(Directory(root));
var projectName =  Argument("projectName", ((DirectoryPath)(MakeAbsolute(Directory(root)).FullPath)).GetDirectoryName());
var solutionFile = $"../{projectName}.sln";
var binFolder =  projectRoot + Directory($"/{projectName}/bin");
var targetOutput = Directory(root) + Directory("../nuget-repository");

if (HasArgument("targetOutput"))
{
    targetOutput = Directory(Argument<string>("targetOutput"));
}

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var buildDir = Directory(binFolder) + Directory(configuration);

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("Restore-NuGet-Packages")
    .IsDependentOn("Clean")
    .Does(() =>
{
    NuGetRestore(solutionFile);
});

Task("Build")
    .IsDependentOn("Restore-NuGet-Packages")
    .Does(() =>
{
    Information("Building Solution...");
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