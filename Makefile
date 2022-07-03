debug:
	dotnet build PokerSharp/*.csproj --configuration Debug

release:
	dotnet build PokerSharp/*.csproj --configuration Release

test:
	dotnet test PokerSharp.Tests/*.csproj --no-restore --verbosity detailed

clean:
	git clean -xdf
