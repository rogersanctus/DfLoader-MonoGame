# DfLoader-MonoGame

An animation and spritesheet (sprite atlas) loader for [DarkFunction Editor](http://darkfunction.com/editor/) Editor XML files.
With this library you can load animations and spritesheet information to use with [MonoGame](http://www.monogame.net/).

##Instructions##

** Building from sources **

You will need at least Visual Studio 2013 to use this library. But you can try it on other version. Another thing is, of course, [MonoGame](http://www.monogame.net/), so download it from this website [http://www.monogame.net/](http://www.monogame.net/), install and you are ready;
In [https://github.com/rogersanctus/DfLoader-MonoGame](https://github.com/rogersanctus/DfLoader-MonoGame) click on the button: Download ZIP. Or if you want you can also clone the repository;
Chose a location to where to download this file;
Go to that place, select the file master.zip and extract its content to where you want the project to be.
Navigate to inside this place, and you can open the DfLoaderMonogame solution file and then, run the Build command. After that you must have the file: DfLoaderMonoGame.dll inside bin/[CONFIGURATION]/ directory. This directory is inside of your project path;
Where [CONFIGURATION] is either debug or release.

** Setting it up **

Create a template MonoGame project;
Right click in the References folder at Solution Explorer panel and choose Add Reference;
You can also do that by clicking Project menu, then clicking at Add Reference;
In the window Reference Manager, click on the Browse... button at the bottom of that window;
Navigate to the folder where you build the DLL in the Building from the sources section;
Select the file DfLoaderMonoGame.dll;
Click OK;
And you are ready to use the library.

** Samples **

*** Using animations ***
Copy the files: spritesheet.sprites, animations.anim and spritesheet.png that are inside the Samples folder in the DfLoaderMonoGame solution folder and then paste them to the Content folder of you project you created a few steps ago.
Go back to the Solution Explorer, inside the folder Content, click in the file animation.anim;
In its properties go to Copy to Output Directory and select: Copy if newer;
Do the same with the file spritesheet.sprites;
Since you have done the previous step, open the MonoGame Pipeline;
Click on File menu, Open;
Navigate to where is you project Content folder;
There must be a file with the name: Content.mgcb, open it;
Right click on Content item in the panel at the left, just under the Edit menu and go to Add, Existing Item;
Navigate to you project Content folder and select the file: spritesheet.png, then click Open;
Click on File menu, Save;
Now, just go to the Build menu and click on Build; (This step is not really necessary as it will be built as soon as your build your project);

Open the Game1 class;

After the lines of using Microsoft.Xna.Framework... add the line:

```cs
using DfLoaderMonoGame;
```

Just before the class constructor: public Game1(), add the lines bellow:

```cs
DfAnimations animations;
DfAnimation smyle;
```

Now inside the method LoadContent(), right after spriteBatch = new SpriteBatch(GraphicsDevice); insert the following code:

```cs
try
{
	var path = System.IO.Path.Combine(Content.RootDirectory, "animations.anim");
	var reader = new System.IO.StreamReader(path);
	animations = new DfAnimations(reader.ReadToEnd(), Content);
	reader.Close();
}
catch (System.Exception e)
{
	System.Console.WriteLine("Error: " + e.ToString());
	this.Exit();
}

if(animations != null)
{
	smyle = new DfAnimation(animations, new Vector2(80, 132));
	smyle.animation = "smyle_a";
	smyle.Play();
}
```

Inside the UnloadContent() method add the line:

```cs
Content.Unload();
```

Now inside Update(GameTime gameTime) and after Exit(); insert:

```cs
smyle.Update(gameTime);
```

And finally, inside the Draw method and after the GraphicsDevice.Clear(Color.CornflowerBlue); call, add:

```cs
smyle.Draw(spriteBatch);
```

And that's it!