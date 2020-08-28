We used FMOD to handle all the sounds, which i was in charge of
implementing

CSoundtrackManager handles the games soundtrack, both by controlling
which soundtrack is playing ("Main", "Victory" and "Menu") and the
blending between different states within the "Main" soundtrack.
There are three states "OutOfCombat" (default state),
"InCombat" (self explanatory) and "Investigating" which plays when
enemies are searching for the player.

CSoundSettings controll master volume but also contains a devmute
CSoundBank is a huge file containing all non-soundtrack sounds which are used
in the game and simple functions for playing them. It also contains some
helper functions for playing different sounds depending on parameters

CPlayerSFXEvents and CGoblinHorn are files used by unitys animation event system,
they make sure sounds like footsteps and jazz solos line up with their respective
animations

CLiquidAmbience control the sound emitter used by hazards such as lava or tar

CAmbienceController is somewhat incorrectly named, the only ambient sound it plays
is the players heartbeat, initially it was going to play wind and cavern echo sounds
as well but those sounds were scrapped.