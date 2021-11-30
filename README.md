# InputApi
Simple API for mouse and keyboard inputs.

## Importing
InputApi has 2 most important namespaces : `InputApi.Keyboard` and `InputApi.Mouse`. Both are pretty self-explanatory.
```C#
using InputApi.Keyboard;
using InputApi.Mouse;
```

## Keyboard input
In order to create keyboard input or mouse input, you have to use constructor `KeyboardSender`. You can use it in 3 ways.
Then, you can just call method `Send()`.
```C#
KeyboardInput input = new KeyboardInput();
input.keyboardMethod = KeyboardMethod.KeyUp;
input.Keys = new Keys[] { Keys.CapsLock, Keys.A };
KeyboardSender sender = new KeyboardSender(input);
KeyboardSender sender2 = new KeyboardSender(new Keys[] { Keys.CapsLock, Keys.A }, KeyboardMethod.KeyDown);
KeyboardSender sender3 = new KeyboardSender(new Keys[] { Keys.CapsLock, Keys.A }, KeyboardMethod.Both);

sender.Send();
```
## KeyboardInput struct

KeyboardInput struct is pretty self-explanatory too. Just look at the source code ☺️
```C#
KeyboardInput input = new KeyboardInput();
input.keyboardMethod = KeyboardMethod.KeyUp;
input.Keys = new Keys[] { Keys.CapsLock, Keys.A };
```
Now, keyboardMethods. You've got 3 of them.
```C#
KeyboardMethod.KeyUp
KeyboardMethod.KeyDown
KeyboardMethod.Both // KeyUp + KeyDown
```
You can also use `MultiKeyboardSender`. It's the same as `KeyboardSender`, but you can send multiple KeyboardInput structs!
```C#
List<KeyboardInput> list = new List<KeyboardInput>();
KeyboardInput input = new KeyboardInput();
input.keyboardMethod = KeyboardMethod.Both;
input.Keys = new Keys[] { Keys.A };
list.Add(input);
input.Keys = new Keys[] { Keys.B };
list.Add(input);
MultiKeyboardSender sender = new MultiKeyboardSender(list.ToArray());
sender.Send();
```


## Mouse input
It's pretty much same as KeyboardInput. Only diffrience is that you've got MouseInput instead of KeyboardInput.
```C#
MouseInput input = new MouseInput();
input.Button = Button.Left; // InputApi.Mouse.Button, not System.Windows.Forms one!
input.X = 1920 / 2;
input.Y = 1080 / 2;
MouseSender sender = new MouseSender(input);
sender.Send();
```
## Converting to byte[]
You can convert KeyboardInput and MouseInput to byte[]! Before doing anything, you have to use namespace named `InputApi.Serializer`.
```C#
using InputApi.Serializer;
```
Now, if you want to serialize Keyboard or Mouse input, just follow this code.
```C#
KeyboardSerializer serializer = new KeyboardSerializer(input); // you can pass here KeyboardInput and KeyboardInput[]
byte[] bytes = serializer.Serialize();

MouseSerializer miserializer = new MouseSerializer(minput); // you can pass here MouseInput and MouseInput[]
byte[] array = miserializer.Serialize();
```
For deserialization, it's even simpler!
```C#
KeyboardSerializer deserializer = new KeyboardSerializer(bytes);
KeyboardInput[] inputs = deserializer.Deserialize();

MouseSerializer mideserializer = new MouseSerializer(mibytes);
MouseInput[] minputs = mideserializer.Deserialize();
```
##
Hope it helps!<br>
~ 0x34c
