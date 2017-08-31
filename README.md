# DfLoader-MonoGame

An animation and spritesheet (sprite atlas) loader for [DarkFunction Editor](http://darkfunction.com/editor/) Editor XML files.
With this library you can load animations and spritesheets to use with [MonoGame](http://www.monogame.net/).

##Instructions##

**Building from sources**

- You will need Visual Studio 2017 to use this library. But you can try it on other version. Another thing is, of course, [MonoGame](http://www.monogame.net/), so download it from this website [http://www.monogame.net/](http://www.monogame.net/). This project was build with MonoGame 3.6, so install the current release version;
- In [https://github.com/rogersanctus/DfLoader-MonoGame](https://github.com/rogersanctus/DfLoader-MonoGame) Download the repository as a Zip or, if you want, clone the repository;
- Chose a location to where to download this file;
- If you downloaded the Zip file. go to where you downloaded the file, select the file master.zip and extract its content to where you want the project to be;
- Navigate to inside this place, and you can open the DfLoaderMonoGame solution file and then, run the Build command. After that you must have the file: DfLoader.dll inside bin/[CONFIGURATION]/ directory. This directory is inside of your project path;

Where [CONFIGURATION] is either debug or release.

**Setting it up**

- In the Visual Studio create a MonoGame project;
- Right click in the References folder at Solution Explorer panel and choose Add Reference;
- You can also do that by clicking Project menu, then clicking at Add Reference;
- In the window Reference Manager, click on the Browse... button at the bottom of that window;
- Navigate to the folder where you build the DLL in the Building from the sources section;
- Select the file DfLoader.dll;
- Click OK;

And you are ready to use the library.

**Samples**

***Using animations*** <br /><br />
- Copy the files: spritesheet.sprites, animations.anim and image.png that are inside the Samples folder in the DfLoaderMonoGame solution folder and then paste them to the Content folder of your project;
- Open the MonoGame Pipeline by double clicking the file Content.mgcb (its inside Content folder) in the Solution Explorer;
- If MonoGame Pipeline don't open, right click the Content.mgcb file and click Open With... then chose MonoGame Pipeline Tool, clique in Set Default, then Ok. Now your can open Content.mgcb by double clicking it;
- Click on the Content item in the Project panel;
- In Properties panel, go to References, click in the fild in front of it;
- In the Reference Editor window, click Add;
- In the Open window, navigate to whre you placed the DfLoaderMonoGame project, then go to DfContentPipeline/bin/[CONFIGURATION] and select DfContentPipeline.dll and click on Open, then click on Ok;
- Now that MonoGame Pipeline knows how to deal with the DarkFunction files and with the Content item still selected in the Project panel;
- Click on Add Existing Item in the toolbar;
- Navigate to you project Content folder and select the files: image.png, spritesheet.sprites and animations.anim then click Open;
- Click on File menu, Save. Then close the Pipeline Tool;

- In the Solution Explorer, inside your project, open the Game1 class;

- After the lines of using Microsoft.Xna.Framework... add the line:

```cs
using DfLoader;
```

- Just before the class constructor: public Game1(), add the lines bellow:

```cs
Animations animations;
Animation smyle;
```

- Now inside the method LoadContent(), right after spriteBatch = new SpriteBatch(GraphicsDevice); insert the following code:

```cs

	animations = Content.Load<Animations>("animations");

	smyle = new Animation(animations, new Vector2(80, 132))
	{
		smyle.Name = "smyle_a"
	}
	smyle.Play();
```

- Inside the UnloadContent() method add the line:

```cs
Content.Unload();
```

- Now inside Update(GameTime gameTime) and after Exit(); insert:

```cs
smyle.Update(gameTime);
```

- And finally, inside the Draw method and after the GraphicsDevice.Clear(Color.CornflowerBlue); call, add:

```cs
spriteBatch.Begin(SpriteSortMode.BackToFront);	// This will make spritebatch draw textures accordingly to they depth order.
smyle.Draw(spriteBatch);
spriteBatch.End();
```

And that's it!