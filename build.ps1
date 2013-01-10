Task Default -depends Build

Task Build {
   Exec { msbuild "solution.sln" }
}