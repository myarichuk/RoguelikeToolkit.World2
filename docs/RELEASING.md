# Releasing

## One-time setup

1. **NuGet**: create an API key scoped to `RoguelikeToolkit.World2` and add it as repo secret `NUGET_API_KEY`.
2. **OpenUPM**: submit a PR at <https://openupm.com/packages/add/> with this spec (the package is
   served from the `upm` branch, which the release workflow maintains):

   ```yaml
   name: com.myarichuk.roguelike-toolkit.world2
   displayName: RoguelikeToolkit World2
   repoUrl: 'https://github.com/myarichuk/RoguelikeToolkit.World2'
   branch: upm
   gitTagPrefix: upm/v
   gitTagIgnore: '^(?!upm/v)'
   license: MIT
   ```
   Tags `upm/vX.Y.Z` are created by the workflow, so OpenUPM picks up new versions automatically.

## Each release

```bash
git tag v1.0.0 && git push origin v1.0.0
```

The workflow builds and tests, packs `Core` with the tag's version, pushes to NuGet, creates a GitHub
release (with the `.nupkg` and Unity `.tgz` attached), and force-updates the `upm` branch with the
Unity package contents (`package.json` + built `netstandard2.1` DLL).
