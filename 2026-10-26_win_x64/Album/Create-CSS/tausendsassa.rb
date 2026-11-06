#!/usr/bin/env ruby
# -*- coding: utf-8 -*-
#
# Erstellt die css-Datei für eine der Formen;
# Dafür müssen die Arrays 'rahmenarray' und
# 'textfeldarray' angegeben werden.
# Bei nur ein, zwei oder drei Rahmen müssen die nicht
# benötigten Rahmen durch Füllwerte vertreten werden.
# Die Dateien 'auto...css' und '16x12...css' müssen angepasst werden.

def masse_eintragen(weg, formatindex, rahmenarray, textfeldarray, autocsspfad, cssdatei)
displaywidth      = $displaywidth    # Displaybreite 
displayhigh       = $displayhigh     # Displayhöhe
rahmenwidth       = $rahmenwidth     # Rahmenbreite
rahmenhigh1612    = $rahmenhigh16x12 # Rahmenhöhe
rahmenhigh1609    = $rahmenhigh16x09 # Rahmenhöhe
bildwidth1612     = $bildwidth16x12  # Bildbreite
bildwidth1609     = $bildwidth16x09  # Bildbreite
bildhigh1612      = $bildhigh16x12   # Bildhöhe
bildhigh1609      = $bildhigh16x09   # Bildhöhe
rahmenhoehe       = [rahmenhigh1612, rahmenhigh1609]
bildhoehe         = [bildhigh1612,   bildhigh1609  ]
bildbreite        = [bildwidth1612,  bildwidth1609 ]
rahmenhigh        = rahmenhoehe[formatindex]
bildhigh          = bildhoehe[formatindex]
bildwidth         = bildbreite[formatindex]
abstand           = $abstand         # Spaltenbreite
h_linkmass        = $h_linkmass      # Abstand der Naviknöpfe
fontsize          = $fontsize        # Schriftgröße

headerhigh        = (displayhigh/22.5).round # 'gross': headerhigh = 48
# Hilfslinien:
#   h1: horizontale Bildmitte:
h1_line = (displaywidth / 2).round
#   h2: horizontale Mittelachse links:
h2_line = ((h1_line - rahmenhigh1612 / 2) / 2).round
#   h3: horizontale Mittelachse rechts:
h3_line = displaywidth - h2_line
#   v1: obere Bildmitte:
v1_line          = headerhigh + (rahmenhigh1612 / 2).round
#   v2: exakt zwischen oberen und unteren Rahmen:
v2_line          = headerhigh + rahmenhigh1612 + (abstand / 2).round
#   v3: untere Bildmitte:
v3_line          = headerhigh + rahmenhigh1612 + abstand + (rahmenhigh1612 / 2).round
stabwidth1612     = ((rahmenwidth - bildwidth1612)/2).round
stabwidth1609     = ((rahmenwidth - bildwidth1609)/2).round

# Hochkantbilder:
h_rahmenwidth     = rahmenhigh1612
h_rahmenhigh1612  = rahmenwidth
# Versatz bei einem Querbild:
h_versatz         = ((rahmenwidth - bildwidth)/2).round

# puts "h_versatz: " + h_versatz.to_s + " Rahmenbreite: " + rahmenwidth.to_s + " Bildbreite: " + bildwidth.to_s

v_versatz1612     = ((rahmenhigh1612  - bildhigh1612)/2 ).round
v_versatz1609     = ((rahmenhigh1609  - bildhigh1609)/2 ).round
v_versatz         = [v_versatz1612, v_versatz1609][formatindex]
# Versatz bei einem Hochkantbild:
hh_versatz        = v_versatz
vh_versatz        = h_versatz
h_linkback        = displaywidth - 3 * h_linkmass
h_linktop         = displaywidth - 2 * h_linkmass
h_linknext        = displaywidth - 1 * h_linkmass
v_link            = (displayhigh / 100).round
v_ueber           = (displayhigh / 108).round
v_seite           = 2 * (displayhigh / 100).round 
h_seite           = displaywidth - 4 * h_linkmass
marginleft        = ((displaywidth - (5 * rahmenwidth / 2).round + abstand) / 2).round

kombi1612_1609  = ((rahmenhigh1612 - rahmenhigh) / 2).round
v_Textfeld_oben = headerhigh + v_versatz + kombi1612_1609

v1 = headerhigh + kombi1612_1609                # v Rahmen oben
v2 = headerhigh + rahmenhigh1612 + abstand + kombi1612_1609 # v Rahmen unten
v3 = v2_line - (rahmenhigh1612 / 2).round + kombi1612_1609 # v Rahmen mittig
v4 = ((displayhigh - rahmenhigh) / 2).round     # Bezugspunkt Bildschirmmitte
v5 = v2 + rahmenhigh1612 - rahmenwidth          # Hochkantbild 16x12 unten
v6 = v2_line - (rahmenwidth / 2).round         # Hochkantbild vertikal mittig
v7 = v3_line + (rahmenhigh1609 / 2).round - rahmenwidth # Hochkantbild 16x09 unten
v8 = headerhigh                                 # Hochkantbild 16x12 oben
v9 = v3_line + (rahmenhigh1612 / 2).round - rahmenwidth # Hochkantbild 16x12 unten
v10 = v1_line - (rahmenhigh1609 / 2).round      # Hochkantbild 16x09 oben
v11 = v6 + rahmenwidth - rahmenhigh1612          # Rahmen quer 16x12 bündig mit Hochk.
v12 = v6 + rahmenwidth - rahmenhigh1609          # Rahmen quer 16x09 bündig mit Hochk.
v13 = v6 + rahmenhigh1612 - rahmenwidth
v14 = headerhigh + rahmenwidth - rahmenhigh1612
v15 = headerhigh + rahmenwidth - rahmenhigh1609
v16 = v5 + ((v5 - v9) / 2).round
h1 = marginleft                                 # 4 Bilder: Rahmen ganz links
h2 = marginleft + (rahmenwidth / 2).round       # 4 Bilder: Rahmen eingerückt links
h3 = marginleft + rahmenwidth + abstand         # 4 Bilder: 2. Rahmen von links
h4 = h2 + rahmenwidth + abstand                 # 4 Bilder: 2. Rahmen eingerückt von links
h5 = (displaywidth / 2).round - bildwidth1612 - h_versatz  # 2 Bilder: h Rahmen links
h6 = (displaywidth / 2).round - h_versatz       # 2 Bilder: h Rahmen rechts
h7 = ((displaywidth - rahmenwidth) / 2).round   # 1 Bild:   h Rahmen mittig
h8 = h2_line - (rahmenhigh1612 / 2).round       # Hochkantbild links
h9 = h1_line - (rahmenhigh1612 / 2).round       # Hochkantbild horizontal mittig
h10 = h3_line - (rahmenhigh1612 / 2).round + abstand     # Hochkantbild rechts
h11 = ((displaywidth - (rahmenwidth + rahmenhigh + abstand)) / 2).round
h12 = h11 + rahmenwidth + abstand               # rechts das Hochkantbild
h13 = h11 + rahmenhigh  + abstand               # links das Hochkantbild
h14 = ((displaywidth - 2 * rahmenhigh1612) / 3).round # linkes Hochkantbild
h15 = 2 * h14 + rahmenhigh1612                  # rechtes Hochkantbild
h16 = ((displaywidth - 2 * rahmenhigh1612 - rahmenwidth - 2 * abstand) / 2).round # HQH links 1612
h17 = h16 + rahmenhigh1612 + abstand            # HQH Mitte
h18 = h17 + rahmenwidth + abstand               # HQH rechts
h19 = ((displaywidth - 2 * rahmenhigh1609 - rahmenwidth - 2 * abstand) / 2).round # HQH links 1609
h20 = h19 + rahmenhigh1609 + abstand            # HQH Mitte
h21 = h20 + rahmenwidth + abstand               # HQH rechts
h22 = h1_line - (rahmenwidth / 2).round         # Q horizontal mittig
ht1 = marginleft + h_versatz                    # h Textfeld links
ht2 = ht1 + 2 * rahmenwidth + 0 * abstand       # h Textfeld rechts
ht3 = h5 + h_versatz                            # 2 Bilder: h Textfeld links
ht4 = h5 + rahmenwidth + abstand                # 2 Bilder: h Textfeld rechts
ht5 = h7 + h_versatz                            # 1 Bild:   h Bild mittig
ht6 = h3 + h_versatz
ht7 = h13 + h_versatz
ht8 = h20 + 3 * rahmenhigh1612 / 2
ht9 = h20 + rahmenhigh1612
ht10 = (displaywidth / 2).round
ht11 = h22 + rahmenwidth + abstand              # Textfeld rechts, wenn links Hochkantbild
vt1 = v_Textfeld_oben                           # v Textfeld oben
vt2 = v_Textfeld_oben + abstand + rahmenhigh1612 # v Textfeld unten
vt3 = v3 + rahmenhigh1612 + abstand + v_versatz + kombi1612_1609
vt4 = v4 + rahmenhigh + 1 * abstand             # Textfeld 1 unter Opener
vt5 = v4 + rahmenhigh + 3 * abstand             # Textfeld 2 unter Opener
vt6 = v11 + rahmenhigh1612 +  abstand           # unter dem Hochkantbild
vt7 = vt1 + rahmenwidth - rahmenhigh1612
vt8 = vt1 + rahmenwidth - rahmenhigh1609
vt9 = v_Textfeld_oben + rahmenhigh1612
vt10 = v1 + v_versatz + rahmenwidth

eckpunktarray  = [] # Zwischenspeicher für ein Wertepaar für eine Rahmenecke
texteckenarray = [] # Zwischenspeicher für ein Wertepaar für ein Textfeld
ergarray       = [] # Alle Pixelwertepaare für die Rahmenecken
textergarray   = [] # Die beiden Pixelwertepaare für beide Textfelder

# Die Pixelwerte:
harray =     [ h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, 
               h11, h12, h13, h14, h15, h16, h17, h18, h19,
               h20, h21, h22 ] # h-Werte der Rahmen
varray =     [ v1, v2, v3, v4, v5, v6, v7, v8, v9, v10,
               v11, v12, v13, v14, v15, v16 ]      # v-Werte der Rahmen
htextarray = [ ht1, ht2, ht3, ht4, ht5, ht6, ht7, ht8, ht9, ht10, ht11 ]    # h-Werte der Textfelder
vtextarray = [ vt1, vt2, vt3, vt4, vt5, vt6, vt7, vt8, vt9, vt10 ]    # v-Werte der Textfelder

# Den Platzierungszahlen werden die Pixelwerten zugeordnet:
rahmenarray.each{|paar| 
  eckpunktarray.push( harray[paar[0]] )      # speichert den h-Wert
  eckpunktarray.push( varray[paar[1]] )      # speichert den v-Wert
  ergarray.push(eckpunktarray)               # speichert das Wertepaar
  eckpunktarray = []                         # leert das Array
}

# Das Gleiche jetzt für die Textfelder:
textfeldarray.each{|paar|
  texteckenarray.push( htextarray[paar[0]] ) # speichert den h-Wert
  texteckenarray.push( vtextarray[paar[1]] ) # speichert den v-Wert
  textergarray.push( texteckenarray )        # speichert das Wertepaar
  texteckenarray = []                        # leert das Array
}
  print "Die Eckwerte der Rahmen: "     + ergarray.to_s     + "\n"
  print "Die Eckwerte der Textfelder: " + textergarray.to_s + "\n"

  laengenhash = {}
# Das Display:   
laengenhash[:h_halfdisplaystr]       = (displaywidth/2).round
laengenhash[:v_halfdisplaystr]       = (displayhigh /2).round - 2 * headerhigh
laengenhash[:w_displaystr]           = displaywidth
laengenhash[:displayhighstr]         = displayhigh
# Rahmen oben links: 
laengenhash[:h_rahmenobenlinksstr]   = ergarray[0][0]     # h Rahmen oben links
laengenhash[:v_rahmenobenlinksstr]   = ergarray[0][1]     # v Rahmen oben
laengenhash[:h_bildobenlinksstr]     = ergarray[0][0] + h_versatz
laengenhash[:v_bildobenlinksstr]     = ergarray[0][1] + v_versatz
# Hochkantrahmen oben links: 
laengenhash[:h_rahmenobenlinksstr]   = ergarray[0][0]     # h Rahmen oben links
laengenhash[:v_rahmenobenlinksstr]   = ergarray[0][1]     # v Rahmen oben
laengenhash[:hx_bildobenlinksstr]     = ergarray[0][0] + hh_versatz
laengenhash[:vx_bildobenlinksstr]     = ergarray[0][1] + vh_versatz
# Rahmen oben rechts: 
laengenhash[:h_rahmenobenrechtsstr]  = ergarray[1][0]     # h Rahmen oben rechts
laengenhash[:v_rahmenobenrechtsstr]  = ergarray[1][1]     # v Rahmen oben
laengenhash[:h_bildobenrechtsstr]    = ergarray[1][0] + h_versatz
laengenhash[:v_bildobenrechtsstr]    = ergarray[1][1] + v_versatz
# Hochkantrahmen oben rechts: 
laengenhash[:h_rahmenobenrechtsstr]  = ergarray[1][0]     # h Rahmen oben rechts
laengenhash[:v_rahmenobenrechtsstr]  = ergarray[1][1]     # v Rahmen oben
laengenhash[:hx_bildobenrechtsstr]    = ergarray[1][0] + hh_versatz
laengenhash[:vx_bildobenrechtsstr]    = ergarray[1][1] + vh_versatz
# Rahmen unten links: 
laengenhash[:h_rahmenuntenlinksstr]  = ergarray[2][0]     # h Rahmen unten links
laengenhash[:v_rahmenuntenlinksstr]  = ergarray[2][1]     # v Rahmen unten
laengenhash[:h_bilduntenlinksstr]    = ergarray[2][0] + h_versatz
laengenhash[:v_bilduntenlinksstr]    = ergarray[2][1] + v_versatz
# Hochkantrahmen unten links: 
laengenhash[:h_rahmenuntenlinksstr]  = ergarray[2][0]     # h Rahmen unten links
laengenhash[:v_rahmenuntenlinksstr]  = ergarray[2][1]     # v Rahmen unten
laengenhash[:hx_bilduntenlinksstr]   = ergarray[2][0] + hh_versatz
laengenhash[:vx_bilduntenlinksstr]   = ergarray[2][1] + vh_versatz
# Rahmen unten rechts: 
laengenhash[:h_rahmenuntenrechtsstr] = ergarray[3][0]     # h Rahmen unten rechts
laengenhash[:v_rahmenuntenrechtsstr] = ergarray[3][1]     # v Rahmen unten
laengenhash[:h_bilduntenrechtsstr]   = ergarray[3][0] + h_versatz
laengenhash[:v_bilduntenrechtsstr]   = ergarray[3][1] + v_versatz
# Hochkantrahmen unten rechts: 
laengenhash[:h_rahmenuntenrechtsstr] = ergarray[3][0]     # h Rahmen unten rechts
laengenhash[:v_rahmenuntenrechtsstr] = ergarray[3][1]     # v Rahmen unten
laengenhash[:hx_bilduntenrechtsstr]   = ergarray[3][0] + hh_versatz
laengenhash[:vx_bilduntenrechtsstr]   = ergarray[3][1] + vh_versatz
# Textplatzierung: 
laengenhash[:h_textobenlinksstr]     = textergarray[0][0]
laengenhash[:v_textobenlinksstr]     = textergarray[0][1]
laengenhash[:h_textuntenrechtsstr]   = textergarray[1][0]
laengenhash[:v_textuntenrechtsstr]   = textergarray[1][1]
laengenhash[:w_textstr]              = h2 - h1 - abstand - stabwidth1612
# Navigation:
laengenhash[:h_linkbackstr]          = displaywidth - 3 * h_linkmass
laengenhash[:h_linktopstr]           = displaywidth - 2 * h_linkmass
laengenhash[:h_linknextstr]          = displaywidth - h_linkmass
laengenhash[:v_linkstr]              = v_link
laengenhash[:fontsizestr]            = fontsize
# Seitenzahl:
laengenhash[:v_seitestr]             = v_seite
laengenhash[:h_seitestr]             = h_seite
# Überschrift:
laengenhash[:h_ueberstr]             = marginleft + h_versatz
laengenhash[:v_ueberstr]             = v_ueber

suchenundersetzen(weg, laengenhash, autocsspfad, cssdatei)
end


# Hauptprogramm
require_relative "suchenundersetzen"
# formatindex = 0: 16x12
# formatindex = 1: 16x09
# formatindex .....
openerflag    = false
alleCssDateien = ["16x12_1cmQ.css",
                  "16x09_1cmQ.css",
                  "16x12_2bmQcoH.css",
                  "16x09_2bmQcoH.css",
                  "16x12_2boHcmQ.css",
                  "16x09_2boHcmQ.css",
                  "16x12_2boQcuQ.css",
                  "16x09_2boQcuQ.css",
                  "16x12_2buHcoH.css",
                  "16x09_2buHcoH.css",
                  "16x12_2buQcoQ.css",
                  "16x09_2buQcoQ.css",
                  "16x12_3amHcuQdoQ.css",
                  "16x09_3amHcuQdoQ.css",
                  "16x12_3amQcuQdoQ.css",
                  "16x09_3amQcuQdoQ.css",
                  "16x12_3aoQbuQdmH.css",
                  "16x09_3aoQbuQdmH.css",
                  "16x12_3aoQbuQdmQ.css",
                  "16x09_3aoQbuQdmQ.css",
                  "16x12_3auHcmHdoH.css",
                  "16x09_3auHcmHdoH.css",
                  "16x12_3auHbmQdoH.css",
                  "16x09_3auHbmQdoH.css",
                  "16x12_3auQboQdmH.css",
                  "16x09_3auQboQdmH.css",
                  "16x12_3auQcoHduH.css",
                  "16x09_3auQcoHduH.css",
                  "16x12_4boQdoQauQcuQ.css",
                  "16x09_4boQdoQauQcuQ.css",
                  "16x12_4aoQcoQbuQduQ.css",
                  "16x09_4aoQcoQbuQduQ.css"
                 ]

# Das 'rahmenarray' übernimmt die Platzierung der linken oberen Eckpunkte der Rahmen.
# Bei einem Wertepaar ist jeweils der 1. Wert der horizontale Wert
# und der 2. Wert der vertikale Wert. Die Reihenfolge der Wertepaare:
# Zuerst die obere Zeile von links nach rechts,
# dann die untere ebenfalls von links nach rechts.
# 'formatindex' legt das verwendete Bildformat fest.

alleCssDateien.each{|cssDatei|
  case cssDatei
  when "16x12_1cmQ.css"
    # 1 Bild 16x12:
    # =begin
    formatindex   = 0 # 16x12
    openerflag    = true
    autocsspfad   = "../Muster/auto1cmQ.css"
    cssdatei      = "16x12_1cmQ.css"
    rahmenarray   = [[6,3], [0,0], [0,0], [0,0]]
    textfeldarray = [[4,3], [9,3]]
  # =end
  when "16x09_1cmQ.css"
    # 1 Bild 16x09:
    # =begin
    formatindex   = 1 # 16x09
    openerflag    = true
    autocsspfad   = "../Muster/auto1cmQ.css"
    cssdatei      = "16x09_1cmQ.css"
    rahmenarray   = [[6,3], [0,0], [0,0], [0,0]]
    textfeldarray = [[4,3], [9,3]]
  #=end
  ##########################
  when "16x12_2bmQcoH.css"
  # 2 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto2bmQcoH.css"
    cssdatei      = "16x12_2bmQcoH.css"
    rahmenarray   = [[0,0], [10,10], [11,5], [0,0]]
    textfeldarray = [[2,6], [5,5]]
  #=end
  when "16x09_2bmQcoH.css"
    # 2 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    autocsspfad   = "../Muster/auto2bmQcoH.css"
    cssdatei      = "16x09_2bmQcoH.css"
    rahmenarray   = [[0,0], [10,11], [11,5], [0,0]]
    textfeldarray = [[2,6], [5,5]]
  #=end
  #################################
  when "16x12_2boHcmQ.css"
  # 2 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto2boHcmQ.css"
    cssdatei      = "16x12_2boHcmQ.css"
    rahmenarray   = [[0,0], [10,5], [12,10], [0,0]]
    textfeldarray = [[2,0], [6,6]]
  #=end
  when "16x09_2boHcmQ.css"
    # 2 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    autocsspfad   = "../Muster/auto2boHcmQ.css"
    cssdatei      = "16x09_2boHcmQ.css"
    rahmenarray   = [[0,0], [10,5], [12,11], [0,0]]
    textfeldarray = [[2,0], [6,7]]
  #=end
  #################################
  when "16x12_2boQcuQ.css"
    # 2 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto2boQcuQ.css"
    cssdatei      = "16x12_2boQcuQ.css"
    rahmenarray   = [[0,0], [4,0], [5,1], [0,0]]
    textfeldarray = [[3,0], [2,1]]
  #=end
  when "16x09_2boQcuQ.css"
    # 2 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    autocsspfad   = "../Muster/auto2boQcuQ.css"
    cssdatei      = "16x09_2boQcuQ.css"
    rahmenarray   = [[0,0], [4,0], [5,1], [0,0]]
    textfeldarray = [[3,0], [2,1]]
  #=end
  #################################
  when "16x12_2buHcoH.css"
    # 2 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto2buHcoH.css"
    cssdatei      = "16x12_2buHcoH.css"
    rahmenarray   = [[0,0], [13,8], [14,0], [0,0]]
    textfeldarray = [[2,0], [8,9]]
  #=end
  when "16x09_2buHcoH.css"
    # 2 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    autocsspfad   = "../Muster/auto2buHcoH.css"
    cssdatei      = "16x09_2buHcoH.css"
    rahmenarray   = [[0,0], [13,8], [14,0], [0,0]]
    textfeldarray = [[2,0], [8,9]]
  #=end
  #################################
  when "16x12_2buQcoQ.css"
    # 2 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto2buQcoQ.css"
    cssdatei      = "16x12_2buQcoQ.css"
    rahmenarray   = [[0,0], [4,1], [5,0], [0,0]]
    textfeldarray = [[2,0], [3,1]]
  #=end
  when "16x09_2buQcoQ.css"
    # 2 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    autocsspfad   = "../Muster/auto2buQcoQ.css"
    cssdatei      = "16x09_2buQcoQ.css"
    rahmenarray   = [[0,0], [4,1], [5,0], [0,0]]
    textfeldarray = [[2,0], [3,1]]
  #=end
    print "Das waren die Formate mit zwei Bildern.\n"
  #################################
  when "16x12_3amHcuQdoQ.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3amHcuQdoQ.css"
    cssdatei      = "16x12_3amHcuQdoQ.css"
    rahmenarray   = [[7,5], [21,1], [0,0], [3,0]]
    textfeldarray = [[0,0], [10,1]]
  #=end
  when "16x09_3amHcuQdoQ.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3amHcuQdoQ.css"
    cssdatei      = "16x09_3amHcuQdoQ.css"
    rahmenarray   = [[7,5], [21,1], [0,0], [3,0]]
    textfeldarray = [[0,0], [10,1]]
  #=end
    print "3amHcuQdoQ.css \n"
  ############################
  when "16x12_3aoQbuQdmH.css"
    # 3 Bilder 16x12: im 4. Rahmen unterschiedl. Höhen. So lassen!
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3aoQbuQdmH.css"
    cssdatei      = "16x12_3aoQbuQdmH.css"
    rahmenarray   = [[0,0], [1,1], [0,0], [3,8]]
    textfeldarray = [[3,0], [0,1]]
  #=end
  when "16x09_3aoQbuQdmH.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3aoQbuQdmH.css"
    cssdatei      = "16x09_3aoQbuQdmH.css"
    rahmenarray   = [[0,0], [1,1], [0,0], [3,6]]
    textfeldarray = [[3,0], [0,1]]
  #=end
    print "3aoQbuQdmH.css \n"
  #################################xxxx
  when "16x12_3amQcuQdoQ.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3amQcuQdoQ.css"
    cssdatei      = "16x12_3amQcuQdoQ.css"
    rahmenarray   = [[0,2], [3,0], [0,0], [2,1]]
    textfeldarray = [[0,0], [0,2]]
  #=end
  when "16x09_3amQcuQdoQ.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3amQcuQdoQ.css"
    cssdatei      = "16x09_3amQcuQdoQ.css"
    rahmenarray   = [[0,2], [3,0], [0,0], [2,1]]
    textfeldarray = [[0,0], [0,2]]
  #=end
    print "3amQcuQdoQ.css \n"
  ##################################
  when "16x12_3aoQbuQdmQ.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3aoQbuQdmQ.css"
    cssdatei      = "16x12_3aoQbuQdmQ.css"
    rahmenarray   = [[0,0], [1,1], [0,0], [3,2]]
    textfeldarray = [[0,8], [3,0]]
  #=end
  when "16x09_3aoQbuQdmQ.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3aoQbuQdmQ.css"
    cssdatei      = "16x09_3aoQbuQdmQ.css"
    rahmenarray   = [[0,0], [1,1], [0,0], [3,2]]
    textfeldarray = [[0,8], [3,0]]
  #=end
    print "3aoQbuQdmQ.css \n"
  ###############################
  #
  #    
  when "16x12_3auHcmHdoH.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3auHcmHdoH.css"
    cssdatei      = "16x12_3auHcmHdoH.css"
    rahmenarray   = [[7,6], [8,5], [0,0], [9,7]]
    textfeldarray = [[0,0], [7,9]]
  #=end
  when "16x09_3auHcmHdoH.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3auHcmHdoH.css"
    cssdatei      = "16x09_3auHcmHdoH.css"
    rahmenarray   = [[7,6], [8,5], [0,0], [9,7]]
    textfeldarray = [[0,0], [7,9]]
  #=end
    print "3auHcmHdoH.css \n"
  ########################
  when "16x12_3auHbmQdoH.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3auHbmQdoH.css"
    cssdatei      = "16x12_3auHbmQdoH.css"
    rahmenarray   = [[15,6], [16,15], [0,0], [17,7]]
    textfeldarray = [[0,0], [3,5]]
  #=end
  when "16x09_3auHbmQdoH.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3auHbmQdoH.css"
    cssdatei      = "16x09_3auHbmQdoH.css"
    rahmenarray   = [[18,6], [19,15], [0,0], [20,7]]
    textfeldarray = [[0,0], [3,5]]
    #=end
    print "3auHbmQdoH.css\n"
    print "Wir passieren diese markierte Stelle \n"
  ###############################   
  when "16x12_3auQboQdmH.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3auQboQdmH.css"
    cssdatei      = "16x12_3auQboQdmH.css"
    rahmenarray   = [[0,1], [21,0], [0,0], [9,8]]
    textfeldarray = [[0,0], [5,1]]
  #=end
  when "16x09_3auQboQdmH.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3auQboQdmH.css"
    cssdatei      = "16x09_3auQboQdmH.css"
    rahmenarray   = [[0,1], [21,0], [0,0], [9,6]]
    textfeldarray = [[0,0], [5,1]]
    #=end
    print "3auQboQdmH.css \n"
  ################################
  when "16x12_3auQcoHduH.css"
    # 3 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto3auQcoHduH.css"
    cssdatei      = "16x12_3auQcoHduH.css"
    rahmenarray   = [[0,1], [2,0], [0,0], [9,8]]
    textfeldarray = [[0,0], [5,5]]
  #=end
    print "Bis hierher traten keine Probleme auf \n"
  when "16x09_3auQcoHduH.css"
    # 3 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto3auQcoHduH.css"
    cssdatei      = "16x09_3auQcoHduH.css"
    rahmenarray   = [[0,1], [2,0], [0,0], [9,6]]
    textfeldarray = [[0,0], [5,5]]
    #=end
    print "3auQcoHduH.css \n"
    print "Das waren die Formate mit drei Bildern. \n"
  ################################
  #
  #
  when "16x12_4boQdoQauQcuQ.css"
    # 4 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto4boQdoQauQcuQ.css"
    cssdatei      = "16x12_4boQdoQauQcuQ.css"
    rahmenarray   = [[1,0], [3,0], [0,1], [2,1]]
    textfeldarray = [[0,0], [1,8]]
  #=end
  when "16x09_4boQdoQauQcuQ.css"
    # 4 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto4boQdoQauQcuQ.css"
    cssdatei      = "16x09_4boQdoQauQcuQ.css"
    rahmenarray   = [[1,0], [3,0], [0,1], [2,1]]
    textfeldarray = [[0,0], [1,8]]
  #=end
    print "4boQdoQauQcuQ.css \n"
  #####################################
  when "16x12_4aoQcoQbuQduQ.css"
    # 4 Bilder 16x12:
    #=begin
    formatindex   = 0 # 16x12
    openerflag    = false
    autocsspfad   = "../Muster/auto4aoQcoQbuQduQ.css"
    cssdatei      = "16x12_4aoQcoQbuQduQ.css"
    rahmenarray   = [[0,0], [2,0], [1,1], [3,1]]
    textfeldarray = [[1,0],[0,8]]
  #=end
  when "16x09_4aoQcoQbuQduQ.css"
    # 4 Bilder 16x09:
    #=begin
    formatindex   = 1 # 16x09
    openerflag    = false
    autocsspfad   = "../Muster/auto4aoQcoQbuQduQ.css"
    cssdatei      = "16x09_4aoQcoQbuQduQ.css"
    rahmenarray   = [[0,0], [2,0], [1,1], [3,1]]
    textfeldarray = [[1,0], [0,8]]
    #=end
    print "4aoQcoQbuQduQ.css \n"
    print "Alle Formate bearbeitet. \n"
    ##################################
  end             
                
  # orig: for i in (0..3)
  # neu orig: for i in (1...2)
  for i in 1..1
    case i
    when 0
      if openerflag
        require './masse_opener_mittel'
      else
        require './masse_mittel'
      end
      masse_eintragen('mittel', formatindex, rahmenarray, textfeldarray, autocsspfad, cssdatei)
    when 1
      if openerflag
        require './masse_opener_gross'
      else
        require './masse_gross'
      end
      masse_eintragen('gross', formatindex, rahmenarray, textfeldarray, autocsspfad, cssdatei)
    when 2
      if openerflag
        require './masse_opener_vierk'
      else
        require './masse_vierk'
      end
      masse_eintragen('vierk', formatindex, rahmenarray, textfeldarray, autocsspfad, cssdatei)    
    end
  end
}
