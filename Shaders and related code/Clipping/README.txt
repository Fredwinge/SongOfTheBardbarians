Dissolve_Lit is the clipping shader
CClipSettings simply contains settings for you guessed it, clipping

CClipBox, CWall and CWallMultiMaterial are used to determine which walls should be
clipped or not. 
As i weren't able to figure out how it could be decided by the shader 
itself within the timeframe allocated to making it these were created
as an ugly solution to the problem.
The way clipping works is by switching between two materials, one regular material 
and one clipping material whenever they are within a "ClipBox" collider which determines
if they are blocking the players view of the ingame player character.

PerlinNoise.hlsl contains a perlin noise function for the clipping shader
which takes in xyz-values as inputs, this file was shamelessly stolen from the
world wide web (and slightly modified to work with shadergraph) from the 
following links: 
(actual txt file of functions)
http://www.sci.utah.edu/~leenak/IndStudy_reportfall/PNoiseCode.txt
(larger study it was apart of)
http://www.sci.utah.edu/~leenak/IndStudy_reportfall/Perlin%20Noise%20on%20GPU.html