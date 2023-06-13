# UnityFootstepTerrainDetector

The following code is a Terrain Detector that will swap the footstep sound depending on what it detects.

It uses two scripts. The first is the Terrain Detector and when attached to the player via a sphere object (so adding a sphere to the player and then attaching the script) it will return the name of the plane or object it is standing on.
It does this with a raycast.

The second simply gets the keycode and then via conditions will swap the audio based on the exact name of the terrain object (essentially what ever is beneth the player)
