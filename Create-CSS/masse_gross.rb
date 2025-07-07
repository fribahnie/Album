#!/usr/bin/env ruby
# -*- coding: utf-8 -*-
# Die Maße für den 1K Bildschirm:

module Masse
  $displaywidth      = 1920  # nutzbare Breite
  $displayhigh       = 1080  # nutzbare Höhe
  $rahmenwidth       =  674  # Rahmenbreite
  $rahmenhigh16x12   =  499  # Rahmenhöhe
  $rahmenhigh16x09   =  407  # Rahmenhöhe
  $bildwidth16x12    =  562  # Bildbreite
  $bildwidth16x09    =  612  # Bildbreite
  $bildhigh16x12     =  421  # Bildhöhe
  $bildhigh16x09     =  344  # Bildhöhe
  $abstand           =   15  # Abstand
  $h_linkmass        = ($displaywidth/20).round
  $ropenwidth        = 1224  # Rahmenbreite für Opener 
  $ropenhigh         =  935  # Rahmenhöhe für Opener
  $bopenwidth        = 1006  # Bildbreite für Opener
  $bopenhigh         =  810  # Bildhöhe für Opener
  $fontsize          =   12  # Schriftgröße
end

