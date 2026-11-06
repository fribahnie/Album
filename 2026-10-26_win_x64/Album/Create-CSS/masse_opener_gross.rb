#!/usr/bin/env ruby
# -*- coding: utf-8 -*-
# Die Maße für den 1K Bildschirm:

module Masse
  $displaywidth      = 1920  # nutzbare Breite
  $displayhigh       = 1080  # nutzbare Höhe
  $rahmenwidth       = 1224  # Rahmenbreite für Opener 
  $rahmenhigh16x12   =  890  # Rahmenhöhe für Opener
  $rahmenhigh16x09   =  784  # Rahmenhöhe für Opener
  $bildwidth16x12    = 1006  # Bildbreite für Opener
  $bildwidth16x09    = 1006  # Bildbreite für Opener
  $bildhigh16x12     =  754  # Bildhöhe für Opener
  $bildhigh16x09     =  566  # Bildhöhe für Opener
  $abstand           =   15  # Abstand
  $h_linkmass        = ($displaywidth/20).round
  $fontsize          =   24  # Schriftgröße
end

