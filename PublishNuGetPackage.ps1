$ProjectPath = ".\Astrid.Windows\Astrid.Windows.csproj"
.\.nuget\NuGet Pack "$ProjectPath" -OutputDirectory "Builds" -IncludeReferencedProjects -Properties Configuration=Release

$Package = gci .\Builds | sort LastWriteTime | select -Last 1
.\.nuget\NuGet Push .\Builds\$Package
