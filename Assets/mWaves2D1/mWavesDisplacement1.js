// mWaves2D for Unity + Test vertex offset shader - mgear - http://unitycoder.com/blog

// ORIGINAL CODE:
/* Processing Water Simulation
* adapted by: Rodrigo Amaya
* Based on "Java Water Simulation", by: Neil Wallis
* For more information visit the original article here:
* http://neilwallis.com/projects/java/water/index.php
* How does it work? "2D Water"
* http://freespace.virgin.net/hugo.elias/graphics/x_water.htm
*
*/

public var sourceimage:Texture2D;
private var targettexture:Texture2D;
 
private var size:int;
private var hwidth:int;
private var hheight:int;
private var riprad:int;

private var ripplemap:int[];
private var data:int;

private var ripple : Color[];
private var texture : Color[];
private var pixels:int[];

private var oldind:int;
private var newind:int;
private var mapind:int;
 
private var i:int;
private var a:int;
private var b:int;

private var width:int;
private var height:int;

public var disturbsize:int=512;
 
private var dmin:float = 999999999;
private var dmax:float = -999999999;
 
// init 
function Awake ()
{

  width =sourceimage.width;
  height = sourceimage.height;
  
	targettexture = new Texture2D(width,height);
//	renderer.material.mainTexture = targettexture;
	GetComponent.<Renderer>().material.SetTexture("_ExtrudeTex", targettexture);
   
  hwidth = width>>1;
  hheight = height>>1;
  riprad=5; //test with 3
   
  size = width * (height+2) * 2;
   
  ripplemap = new int[size];
  ripple = new Color[width*height];
  texture = new Color[width*height];
  pixels = new int[width*height];
  
  oldind = width;
  newind = width * (height+3);

	var counter:int = 0;
	for (var y:int=0;y<height;y++) 
	{
		for (var x:int=0;x<width;x++) 
		{
			texture[counter] = sourceimage.GetPixel (x,y);
			counter++;
		}
	}
	
	//  smooth();

}
 
function Update()
{
//  image(img, 0, 0); //Displays images to the screen
//  loadPixels(); // Loads the pixel data for the display window into the pixels[] array
//  texture = pixels;
   
  newframe();
  var px:int = 0;
  var py:int = 0;
  
  for (var i:int = 0; i < pixels.length; i++) 
  {
//	todo: use Texture2D.SetPixels instead..
//	targettexture.SetPixel (px, py, ripple[i]);
	targettexture.SetPixel (px, py, ripple[i]);
	px++;
	if (px>=width) {px=0; py++;}
  }
   
  //updatePixels(); //Updates the display window with the data in the pixels[] array
  	targettexture.Apply();
	
	// left mouse button is pressed down
	if(Input.GetMouseButton(0))
	{
		// raycast to mousecursor location
		var hit : RaycastHit;
		if (!Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), hit))	return;
		
		// get real coordinates
		var pixelUV = hit.textureCoord;
		pixelUV.x *= width;
		pixelUV.y *= height;
		
		// then apply waves on that position
		disturb(pixelUV.x,pixelUV.y);
	}

}
 
// ripples 
function disturb(dx:int,dy:int)
{
  for (var j:int=dy-riprad;j<dy+riprad;j++) {
    for (var k:int=dx-riprad;k<dx+riprad;k++) {
      if (j>=0 && j<height && k>=0 && k<width) {
        ripplemap[oldind+(j*width)+k] += disturbsize;
      }
    }
  }
}


// processing
function newframe() 
{
  //Toggle maps each frame
  i=oldind;
  oldind=newind;
  newind=i;

 
  i=0;
  mapind=oldind;
  for (var y:int=0;y<height;y++) {
    for (var x:int=0;x<width;x++) {
      data = (ripplemap[mapind-width]+ripplemap[mapind+width]+ripplemap[mapind-1]+ripplemap[mapind+1])>>1;
      data -= ripplemap[newind+i];
      data -= data >> 5;
	  
      ripplemap[newind+i]=data;
	var col:float = remap(data,-2560,1537,0,1); //Mathf.Abs(data);
	
	if (data<dmin) dmin=data;
	if (data>dmax) dmax=data;
	//if (x==50 && y==50)	print (col);

      //where data=0 then still, where data>0 then wave
      data = (1024-data);
 
      //offsets
      a=((x-hwidth)*data/1024)+hwidth;
      b=((y-hheight)*data/1024)+hheight;
 
      //bounds check
      if (a>=width) a=width-1;
      if (a<0) a=0;
      if (b>=height) b=height-1;
      if (b<0) b=0;
 
//      ripple[i]=texture[a+(b*width)];
	//ripple[i]=A[a+(b*w)];
	
      ripple[i]=Color(col,col,col,1);
      mapind++;
      i++;
    }
  }
  
//  print ("data:"+dmin+","+dmax);
  
}



function remap(value:float, from1:float, to1:float, from2:float, to2:float) 
{
	return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
}
