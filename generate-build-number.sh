#!/bin/bash

# version=`cat version.txt`
build=$[`git log --oneline | wc -l`]
builddate=`date +"%F" `
buildtime=`date +"%T" `
commithash=`git show -s --format=%H`

echo "Generating build $build ($builddate $buildtime)..."

# For .NET Framework projects
echo "// Auto-generated build information code file" > BuildInfo.cs
echo "// (C) Xlfdll Workstation" >> BuildInfo.cs
echo "" >> BuildInfo.cs
echo "namespace Xlfdll" >> BuildInfo.cs
echo "    {" >> BuildInfo.cs
echo "    internal static class BuildInfo" >> BuildInfo.cs
echo "        {" >> BuildInfo.cs
echo "        internal const string Version = \"$buildversion\";" >> BuildInfo.cs
echo "        internal const string Build = \"$build\";" >> BuildInfo.cs
echo "        internal const string BuildDate = \"$builddate\";" >> BuildInfo.cs
echo "        internal const string BuildTime = \"$buildtime\";" >> BuildInfo.cs
echo "        internal const string CommitHash = \"$commithash\";" >> BuildInfo.cs
echo "        }" >> BuildInfo.cs
echo "    }" >> BuildInfo.cs

# For .NET Core projects
# echo "<!-- Auto-generated build information code file -->" > Directory.Build.props
# echo "<!-- (C) Xlfdll Workstation -->" >> Directory.Build.props
# echo "" >> Directory.Build.props
# echo "<Project>" >> Directory.Build.props
# echo "  <PropertyGroup>" >> Directory.Build.props
# echo "    <Version>$version.$build</Version>" >> Directory.Build.props
# echo "  </PropertyGroup>" >> Directory.Build.props
# echo "</Project>" >> Directory.Build.props

echo "Done."