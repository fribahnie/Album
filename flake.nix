{
  description = "Album - GTK# .NET Application";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
    flake-utils.url = "github:numtide/flake-utils";
  };

  outputs = { self, nixpkgs, flake-utils }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};

        desktopFile = pkgs.makeDesktopItem {
          name = "album";
          desktopName = "Album";
          exec = "Album";
          icon = "album";
          comment = "GTK# .NET Application";
          categories = [ "Graphics" "Utility" ];
          terminal = false;
        };

        album = pkgs.buildDotnetModule {
          pname = "album";
          version = "1.0.0";

          src = ./.;

          projectFile = "Album.csproj";
          executables = [ "Album" ];

          dotnet-sdk = pkgs.dotnet-sdk_9;
          nugetDeps = ./deps.json;

          nativeBuildInputs = with pkgs; [
            pkg-config
            wrapGAppsHook3
          ];

          buildInputs = with pkgs; [
            gtk3
            glib
            gsettings-desktop-schemas
          ];

          runtimeDeps = with pkgs; [
            gtk3
            glib
            icu
          ];

	  postInstall = ''
          # Icon installieren
	  install -Dm644				      \
	  Baukasten/Beispielbilder/Logo/album.png			\
	  $out/share/icons/hicolor/256x256/apps/album.png
		  
          # Ressourcen ins Store-Verzeichnis kopieren
	  mkdir -p $out/share/Album
	  cp -r Baukasten $out/share/Album/Baukasten
	  cp -r Create-CSS $out/share/Album/Create-CSS
	  cp -r Fotoalben $out/share/Album/Fotoalben
	  cp -r Muster $out/share/Album/Muster
		  
          # Wrapper-Skript: beschreibbares Home-Verzeichnis verwenden
	  mkdir -p $out/bin
	  printf '#!/bin/sh\nDIR="$(cd "$(dirname "$0")" && pwd)"\nWORKDIR="$HOME/.local/share/Album"\nmkdir -p "$WORKDIR"\n\n# Ressourcen beim ersten Start ins Workdir kopieren\nif [ ! -d "$WORKDIR/Baukasten" ]; then\n  cp -r "$DIR/../share/Album/"* "$WORKDIR/"\n  chmod -R u+w "$WORKDIR"\nfi\n\ncd "$WORKDIR"\nexec "$DIR/Album" "$@"\n' \
> $out/bin/album-launcher
	  chmod +x $out/bin/album-launcher
		  
	  # Desktop-Datei kopieren
	  install -Dm644 ${desktopFile}/share/applications/album.desktop \
	  $out/share/applications/album.desktop
		  
	  # Exec auf das Wrapper-Skript setzen
	  substituteInPlace $out/share/applications/album.desktop	\
	  --replace "Exec=Album" "Exec=$out/bin/album-launcher"
	  '';

          meta = with pkgs.lib; {
	    description = "GTK# .NET Application";
	    mainProgram = "Album";
	    license = licenses.gpl3Plus;
	    platforms = platforms.linux;
          };
        };
      in
      {
	devShells.default = pkgs.mkShell {
	  packages = with pkgs; [
	    dotnet-sdk_9
	    gsettings-desktop-schemas
	    glib
	    gtk3
	    gtk4
	    icu
	    pkg-config
	    nuget-to-json
	    libadwaita
	    wrapGAppsHook3
	  ];

	  shellHook = ''
	  export XDG_DATA_DIRS="${pkgs.gsettings-desktop-schemas}/share/gsettings-schemas/${pkgs.gsettings-desktop-schemas.name}:$XDG_DATA_DIRS"
          export LD_LIBRARY_PATH=${pkgs.lib.makeLibraryPath (with pkgs; [
            gtk3
            gtk4
            glib
            pango
            cairo
            gdk-pixbuf
            libadwaita
          ])}:$LD_LIBRARY_PATH
          '';
	};

        packages.default = album;

	apps.default = {
	  type = "app";
	  program = "${album}/bin/Album";
	};
    });
}
