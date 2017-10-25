using Elmatgar;
//using HibernatingRhinos.Profiler.Appender.EntityFramework;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EntityFrameworkProfilerBootstrapper), "PreStart")]
namespace Elmatgar
{
	public static class EntityFrameworkProfilerBootstrapper
	{
		public static void PreStart()
		{
			// Initialize the profiler
			 
			
			// You can also use the profiler in an offline manner.
			// This will generate a file with a snapshot of all the EntityFramework activity in the application,
			// which you can use for later analysis by loading the file into the profiler.
			// var filename = @"c:\profiler-log";
			// EntityFrameworkProfiler.InitializeOfflineProfiling(filename);

			// You can use the following for production profiling.
			// EntityFrameworkProfiler.InitializeForProduction(11234, "A strong password like: ze38r/b2ulve2HLQB8NK5AYig");
		}
	}
}

