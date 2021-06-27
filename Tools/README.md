The tools are sourced from here:
https://github.com/plasturion/LA3-Tools

Usage:
Extract the LA3-Tools, and then copy the .exe's into the "tools" directory.  
then copy the raw scripts (.bin) into the input directory.  
After that, click on the buttons for step 1, then step 2. A messagebox should appear after it finishes each one.  
Then, you can go to "2-for-editing" for the scripts. This is where you would edit/replace the modified files.  
Then click on Reformat, then Re-Encode, then Convert to bin.  



## Extracting/Repacking the Scripts into the ROM  
Download and run DSLazy: https://www.romhacking.net/utilities/793/  
Click the three dots to choose the rom.  
Select NDS unpacker, and let it unpack the files.  
Follow the steps from the above to generate the .bin files  
Take the files from the "5-strBin" directory, and replace them in "$unpackedrom/data/usr2/eventdata"  
Click on NDS packer option to pack the rom, and save the file  

Download XDelta GUI: https://www.romhacking.net/utilities/598/  
Have a copy of an untouched LA3 japanese rom.  
Select the "Create Patch" Tab  
Select the untouched rom copy under "Original File"  
Selected your repacked copy under "Modified file"  
and make a name for the output patch under "Patch Destination"  
Then click "Create Patch"  
You may now distribute the .xdelta file  
