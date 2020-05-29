# Unity In-game Debug Console
Easy to use Unity package for debugging in game

![gif](https://user-images.githubusercontent.com/32217921/83310712-a1c0c700-a215-11ea-91bb-f5b99f2c2a60.gif)

You can clone or download the project directly, or download the package from releases section and import it to your project.

# How To Use
After importing the package into your Unity project, drag the UnityLog prefab into your scene.

![prefab](https://user-images.githubusercontent.com/32217921/83310837-0d0a9900-a216-11ea-9895-d8c24ac215b8.png)

You may see this chat bubble in your game view now.

![bubble](https://user-images.githubusercontent.com/32217921/83310861-24498680-a216-11ea-8231-351d9645d74d.png)

If this blocks your view, you can select the bubble from the hierarchy and drag it anywhere you would like (e.g. offscreen). This will have no effect for the runtime since UnityLog will arrange it's layout automatically when the game starts.

![bubble-hierarchy](https://user-images.githubusercontent.com/32217921/83310884-31667580-a216-11ea-9758-7ae52d236489.png)

Click on the UnityLog gameobject in the hierarchy and you will see some properties in the inspector.

![inspector](https://user-images.githubusercontent.com/32217921/83306862-0840e780-a20c-11ea-85c5-d669693fe9c4.png)

- With <em>don't destroy on load</em> enabled, UnityLog will persist after scene transitions. Any other instances of this prefab will destroy itself.

- If you want UnityLog to operate on spesific scenes, you can place the prefab to those scenes and remove the <em>don't destroy on load</em> ticks.

- You can also change the other self explanotary properties as you wish.

## Sending Debug Messages

Use UnityLog namespace on the scripts which you desire to send debug messages.
```csharp
using UnityLog;
```

Log.DebugLog is a static method for debugging which takes an object as a parameter, just like Unity's regular Debug.Log method.

```csharp
void SendMessage()
{
    Log.DebugLog("Hello, World!");
}

void PrintPosition()
{
    Log.DebugLog(transform.position);
}
```

## Important Notes
- UnityLog will automatically arrange it's layout based the screen. It will update itself when screen dimetions change, such as game window in the editor or screen orientation on handheld devices.

- UnityLog requires EventSystem in order to operate. After a new scene is loaded if there is no EventSystem in the scene, UnityLog will automatically create one.

- You can see the sample scene in Assets/UnityLog/Samples folder.
