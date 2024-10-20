# The Interpreter
Our interpreter is written in C# (.netstandrard2.1)

Its dynamically typed (im sorry), doesn't feauture semicolons (im sorry again) but does feauture brackets (you're welcome) and specialises with integers to cater to the simulator!

## Feautures
Lets get technical. shall we?

#### Variables
```c#
x = 10
y = 20
```

#### Functions
```cs
//general syntax
func name <arguments> => {
  <body>
}

func add x y => {
  x + y//returns x + y
}

//call node
add(10, 20)
```

> NOTE: the `=>` and `{` must be on the same line as the declaration statement

#### Conditions
> Its worth noting the language doesn't feauture booleans explicitly. It conforms to the standard:

> 1: true

> 0: false

```cs
//general syntax
if <condition> => {
  <body>
}

if x > 1 && x < 10 => { 
  x = x + 10
}
```

> NOTE: the `=>` and `{` must be on the same line as the condition

#### Loops
```cs
//general syntax
loop <condition> => {
  <body>
}

loop x > 1 && x < 10 => { 
  x = x + 1
}
```

> NOTE: the `=>` and `{` must be on the same line as the condition

#### Input and Output
The langauge features two keywords: `in` and `out` to access the input and output vectors of a node.

• `in[index]:` accesses the index'th element of the input vector

• `out[index]:` accesses the index'th element of the output vector

its worth noting that `in` is a constant vector and cannot be changed. 
`out` functions as any normal variable

```cs
loop out[0] < in[0] => {
  out[0] = in[0] + 2
}
```
