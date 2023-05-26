using System.IO;
using System.Threading.Tasks;

[Target(BuildTarget.Restore)]
public static class Restore
{
	public static async Task OnExecute(BuildContext context)
	{
		context.BuildStep("Restoring NuGet packages");

		await context.Exec("dotnet", $"restore --verbosity {context.Verbosity}");
	}
}
