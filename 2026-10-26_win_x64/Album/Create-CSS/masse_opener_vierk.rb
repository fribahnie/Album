#!/usr/bin/env ruby
# -*- coding: utf-8 -*-
# Die Maße für den 4K Bildschirm (= 1K * 2):

module Masse
  $displaywidth      = 1920 * 2  # nutzbare Breite
  $displayhigh       = 1080 * 2  # nutzbare Höhe
  $rahmenwidth       = 1224 * 2  # Rahmenbreite für Opener 
  $rahmenhigh16x12   =  890 * 2  # Rahmenhöhe für Opener
  $rahmenhigh16x09   =  784 * 2  # Rahmenhöhe für Opener
  $bildwidth16x12    = 1006 * 2  # Bildbreite für Opener
  $bildwidth16x09    = 1006 * 2  # Bildbreite für Opener
  $bildhigh16x12     =  754 * 2  # Bildhöhe für Opener
  $bildhigh16x09     =  566 * 2  # Bildhöhe für Opener
  $abstand           =   15 * 2  # Abstand
  $h_linkmass        = ($displaywidth/20).round
  $fontsize          =   24 * 2  # Schriftgröße
end

