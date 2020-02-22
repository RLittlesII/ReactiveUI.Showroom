using System;
using System.Linq;
using System.Linq.Expressions;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.Execution;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Rocket.Surgery.Nuke;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using Rocket.Surgery.Nuke.Xamarin;

[CheckBuildProjectConfigurations]
[UnsetVisualStudioEnvironmentVariables]
internal class Showroom : XamariniOSBuild, IXamariniOSBuild
{
    /// <summary>
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode
    /// </summary>

    public static int Main() => Execute<Showroom>(x => x.Default);

    public override AbsolutePath InfoPlist { get; } = RootDirectory / "src" / "iOS" / "info.plist";

    public override string BaseBundleIdentifier { get; } = "com.reactiveui.showroom";

    private Target Default => _ => _
        .DependsOn(XamariniOS);

    public Target Restore => _ => _
        .With(this, XamariniOSBuild.Restore);

    public Target ModifyInfoPlist => _ => _
        .With(this, XamariniOSBuild.ModifyInfoPlist);

    public Target Build => _ => _
        .With(this, XamariniOSBuild.Build)
        .DependsOn(ModifyInfoPlist);

    public Target Test => _ => _
        .With(this, XamariniOSBuild.Test);

    public Target Package => _ => _
        .With(this, XamariniOSBuild.Package);

    public Target XamariniOS => _ => _
        .DependsOn(Restore)
        .DependsOn(ModifyInfoPlist)
        .DependsOn(Build)
        .DependsOn(Test)
        .DependsOn(Package);
}