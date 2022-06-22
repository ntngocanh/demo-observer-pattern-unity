# demo-observer-pattern-unity
A simple demo using observer pattern in unity
## How to use ##
1. Clone the repository into your local machine
```
git clone git@github.com:ntngocanh/demo-observer-pattern-unity.git
```
2. Open the project in Unity

3. Use the mouse to move the player character, collect stars and see how the scores and levels go up
## What is observer pattern? ##

The observer pattern is a software design pattern in which an object, called the subject, maintains a list of its dependents, called observers, and notifies them automatically of any state changes, usually by calling one of their methods.

Read more about observer pattern here: https://refactoring.guru/design-patterns/observer

Here in this demo, we don't use the class-based implementation but a built-in feature of C# called *delegate*

Read more about delegates, action and func here: https://www.tutorialsteacher.com/csharp/csharp-delegates

## Observer Pattern applied in this demo ##

Class Player controls the player object (which collects the stars). We declared an EventHandler called StarCollected.
```C#
//an eventHandler with default eventargs
  public static event EventHandler StarCollected;
```
When the player collides with a star, the EventHandler is invoked.
```C#
//Invoke the starcollected event with no eventargs
  StarCollected?.Invoke(this, EventArgs.Empty);
```
In the GUI class (which changes the score display every time a star is collected), Start method, we add a listener OnStarCollected which is called when StarCollected event is invoked.
```C#
Player.StarCollected += OnStarCollected;
```

There are several more events and listeners declared and used in other classes also (in different ways), feel free to explore the code yourself!

