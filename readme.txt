* Welcome
Thanks for downloading the Charge Up Gun scripts. this script came about due to
someone on Unity 3D forums asking for an efficient way to charge up a weapon
similar to the alien weapons in Halo.

Visit https://bitbucket.org/JustinLloyd/chargeupgun/overview for 
more information and to download the latest version.


* License
                      DO WHATEVER PUBLIC LICENSE*
   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

  0. You can do whatever you want to with the work.
  1. You cannot stop anybody from doing whatever they want to with the work.
  2. You cannot revoke anybody elses DO WHATEVER PUBLIC LICENSE in the work.

 This program is free software. It comes without any warranty, to
 the extent permitted by applicable law. You can redistribute it
 and/or modify it under the terms of the DO WHATEVER PUBLIC LICENSE
 
 Software originally created by Justin Lloyd @ http://otakunozoku.com/


* About
Original URL:
http://forum.unity3d.com/threads/174426-How-to-do-efficient-charging-or-buildup-on-a-gun

My implementation notes:
Thought about it for a couple of minutes and came came up with the following 
code. There may be lurking bugs, I didn't give it a thorough testing but it 
should work for what you want.

The ChargeGUI.cs is only useful for a quick display of the current charge and 
should be replaced with whatever effects you actually want.

The ChargeInputHandlers.cs is for taking player input. You will want to change
this to whatever your control scheme happens to be.

Attach the three separate scripts to a GameObject or two and wire up the
ChargeUp references in the ChargeUpInputHandler and ChargeGUI scripts.

* Support
Absolutely none provided.


                Software originally created by Justin Lloyd 
         and distributed via the DO WHATEVER PUBLIC LICENSE in 2013.