def suchenundersetzen(weg, laengenhash, autocsspfad, cssdatei)
mystring = ""
IO.foreach(autocsspfad, "r"){|line| 
  mystring = mystring + line }

laengenhash.map{|k,v|
  suchstring = k.to_s
  wert = v.to_s + "px"
  # print suchstring + ": " + wert + "\n"
  mystring.gsub!(suchstring, wert)}

f = File.new("../Baukasten/css-Dateien/#{weg}/" + cssdatei, "w+") 
f.print mystring
f.close
end
