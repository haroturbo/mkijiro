make -f sc
copy MKIJIRO_SC.prx MKIJIRO\MKIJIRO.prx
copy MKIJIRO_SC.prx patches\MKIJIRO.prx
del *.elf
del objects\*.o
move MKIJIRO_SC.prx g:\seplugins\MKIJIRO_SC.prx
move MKIJIRO_SC.prx f:\seplugins\MKIJIRO_SC.prx
move MKIJIRO_SC.prx e:\seplugins\MKIJIRO_SC.prx
del *.prx