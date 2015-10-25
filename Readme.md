This adds some extra linq extensions I have found in the ruby community. Ruby dev's will recognize these from the ruby array api docs. If you are still confused with these explanations check out the docs of the ruby version(s)


## Reject

Reject works much like a `.WhereNot` Basically a linq method where things that return true will be exlcuded from the Returning IEnumerable. This is handy as sometimes you end up with a `Where` statement that basically says where not this and not that and not this other thing. Sometimes its easier to say reject this.
`List.Reject(a=>a.IShouldntBeHere)` 

this is the same as writing 

`List.Where(a=>a.IShouldntBeHere == false)`


## Cycle

Cycle will run an action for every element in an IEnumerable. This will run forever unless you pass the number of times to enumerate over the array

`Enumerable.Range(1,4).Cycle(a=>Console.Write(a + ","),2);`

this will print `1,2,3,4,1,2,3,4`

## ForEach

I hope I dont have to explain this one. `ListOfThings.ForEach(a=>a.DoSomething())`

## Distinct(with value)

Overload of Distinct that lets you select a property to distinct on instead of the root level object. `List.Distinct(a=>a.FullName)`

## Compact

Compact removes all nulls from a list. its a simplification of `.Where(a=>a!=null)` or perhapse `.Reject(a=>a==null)`

## AssocFirstOrDefault

This one is the most complex. This works on Enumerable of Enumerables. Basically pass in an expected object. If that object lives in one of the child arrays that child array is returned. Otherwise return nothing.

```csharp


var outer = new List<List<string>>();
			outer.Add(new List<string>() { "books", "school", "apple", "playtime" });
			outer.Add(new List<string>() { "star", "wars", "vader" });
			var assc = outer.AssocFirstOrDefault("wars");
//assc will be the 2nd list


```

