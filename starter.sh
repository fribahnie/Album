##!/usr/bin/env bash

echo "START SCRIPT" > /tmp/album-debug.log
pwd >> /tmp/album-debug.log
whoami >> /tmp/album-debug.log
env >> /tmp/album-debug.log

echo "XDG_DATA_DIRS=$XDG_DATA_DIRS" >> /tmp/album-debug.log
echo "GSETTINGS_SCHEMA_DIR=$GSETTINGS_SCHEMA_DIR" >> /tmp/album-debug.log

cd /home/frieder/csharp_Projekte/AlbumEditor/Album || exit 1

export DOTNET_ROOT="$(dirname "$(dirname "$(readlink -f "$(which dotnet)")")")"
export PATH="$PATH:$DOTNET_ROOT"

export XDG_DATA_DIRS=/run/current-system/sw/share
export GSETTINGS_SCHEMA_DIR=/run/current-system/sw/share/glib-2.0/schemas

exec ./result/bin/Album > /tmp/album.log 2>&1
