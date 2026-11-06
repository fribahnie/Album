#!/usr/bin/env ruby
# -*- coding: utf-8 -*-
# Die Maße für den 4K-Bildschirm:

module Masse
  $displaywidth      = 1920 * 2 # nutzbare Breite
  $displayhigh       = 1080 * 2 # nutzbare Höhe
  $rahmenwidth       =  674 * 2 # Rahmenbreite
  $rahmenhigh16x12   =  499 * 2 # Rahmenhöhe
  $rahmenhigh16x09   =  407 * 2 # Rahmenhöhe
  $bildwidth16x12    =  562 * 2 # Bildbreite
  $bildwidth16x09    =  612 * 2 # Bildbreite
  $bildhigh16x12     =  421 * 2 # Bildhöhe
  $bildhigh16x09     =  344 * 2 # Bildhöhe
  $abstand           =   15 * 2 # Abstand
  $h_linkmass        = ($displaywidth/20).round
  $ropenwidth        = 1224 * 2 # Rahmenbreite für Opener 
  $ropenhigh         =  935 * 2 # Rahmenhöhe für Opener
  $bopenwidth        = 1006 * 2 # Bildbreite für Opener
  $bopenhigh         =  810 * 2 # Bildhöhe für Opener
  $fontsize          =   24 * 2 # Schriftgröße
end
