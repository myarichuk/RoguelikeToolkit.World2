# RoguelikeToolkit.World2

[![CI](https://github.com/myarichuk/RoguelikeToolkit.World2/actions/workflows/ci.yml/badge.svg)](https://github.com/myarichuk/RoguelikeToolkit.World2/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/RoguelikeToolkit.World2)](https://www.nuget.org/packages/RoguelikeToolkit.World2)
[![openupm](https://img.shields.io/npm/v/com.myarichuk.roguelike-toolkit.world2?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.myarichuk.roguelike-toolkit.world2/)

Procedural, planet-sized world generation for RPG and strategy games. Data is computed on demand
from a seed, so a whole planet needs no up-front storage.

## Layout

| Path | Purpose |
| --- | --- |
| `src/RoguelikeToolkit.World2.Core` | The library (`netstandard2.1`, Unity-compatible) |
| `src/RoguelikeToolkit.World2.Visualizer` | Avalonia app for inspecting generated worlds |
| `tests/RoguelikeToolkit.World2.Core.Tests` | xUnit v3 tests |
| `unity/` | Metadata used to build the OpenUPM package |

## Install

```bash
dotnet add package RoguelikeToolkit.World2
```

Unity (via [OpenUPM](https://openupm.com)):

```bash
openupm add com.myarichuk.roguelike-toolkit.world2
```

## Usage

```csharp
var planet = new RoguelikeToolkit.World2.Planet(seed: 42);
double elevation = planet.SampleElevation(latitudeDeg: 51.5, longitudeDeg: -0.12); // 0..1
```

## Build

```bash
dotnet build
dotnet test
dotnet run --project src/RoguelikeToolkit.World2.Visualizer
```

## Releasing

Push a tag like `v1.2.3`. The [release workflow](.github/workflows/release.yml) publishes to NuGet,
creates a GitHub release, and publishes the Unity package to the `upm` branch (tag `upm/v1.2.3`) for OpenUPM.
See [docs/RELEASING.md](docs/RELEASING.md) for one-time setup.

## License

MIT
