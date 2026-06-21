# ProjectNoctis

## Deployment

To package the build output for deployment:

1. Build the project in Visual Studio (or `dotnet build`) — output lands in `ProjectNoctis/bin/Debug/netcoreapp5.0/`
2. From the repo root, run:
   ```
   tar -cvzf rikku.tgz -C ProjectNoctis/bin/Debug/netcoreapp5.0 .
   ```
3. Deploy `rikku.tgz` to the server (e.g. via `scp`).

`rikku.tgz` is git-ignored and should never be committed.
