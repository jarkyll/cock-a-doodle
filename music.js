var current_track : AudioClip;
var replace_track : AudioClip;

#start the game, and the track should play    
audio.clip = current_track;
audio.Play();


#volume for the tracks since we only worry about the playing track and track we will now use.    
var current_track_vol : float = 1.0;
var replace_track_vol : float = 0.0;
var replace_playing : boolean = false;
    
    
/**
* Update consistently where you change the track depending on your location
* Ex. enter dog region, dog instrument fades in and main instrument fades out
* trigger a loop. My idea is to have an OnTriggerEnter for each location
* with the Update and other functions being there overall
*/
    
function OnTriggerEnter ( Collider zone){
  switch(zone.tag){
    case 'cow':
      Debug.log("entered the cowzone")
      replace_track : AudioClip;
    case 'hen':
      Debug.log("entered the henzone")
      replace_track : AudioClip;
    case 'sheep':
      Debug.log("entered the sheepzone")
      replace_track : AudioClip;
    case 'duck':
      Debug.log("entered the duckzone")
      replace_track : AudioClip;
    case 'horses':
      Debug.log("entered the horsezone")
      replace_track : AudioClip;
    case 'dog':
      Debug.log("entered the dogzone")
      replace_track : AudioClip;
    case 'pig':
      Debug.log("entered the pigzone")
      replace_track : AudioClip
    case 'cat':
      Debug.log("entered the catzone")
      replace_track : AudioClip
    default:
      Debug.log("entered the defaultzone")
      replace_track : AudioClip
            
    }
    replace_playing = false;
    current_track_vol = audio.volume;
    replace_track_vol = 0.0;                                  
  }
                                  
                                  
   #TODO: when you hit a trigger zone. make replace_track = track_animal
   # replace_playing = false
   # current_track_vol = audio.volume
   # replace_track_vol = 0.0
                                  
                                  
                                  
/**
* Fade in replacement track and fade out other track
*/
function Update(){
  fadeOut();
  if (current_track_vol <= 0.1) {
    if(replace_playing == false){
      replace_playing = true;
      audio.clip = replace_track;
      audio.Play();
    }
    fadeIn();
  }
}
    
/**
* GUI to display the volume changes
*/
function OnGUI(){
  GUI.Label(new Rect(10, 10, 200, 100), "Audio 1: " + current_track_vol.ToString());
  GUI.Label(new Rect(10, 30, 200, 100), "Audio 2: " + replace_track_vol.ToString());
}
    
/**
* Fades the current track's volume over time
*/
function fadeOut(){
  if(current_track_vol > 0.1){
    current_track_vol -= 0.1 * Time.deltaTime;
    audio.volume = current_track_vol;
  }
}
    
/**
*
*/
function fadeIn(){
  if(replace_track_vol < 1){
    replace_track_vol += 0.1 * Time.deltaTime;
    audio.volume = replace_track_vol;
  }
}
    
