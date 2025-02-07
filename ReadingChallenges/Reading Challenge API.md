# Reading Challenge API

## Endpoints

/challenges
  - Get

/challenge/{id}
  - Post
  - Get
  - Put (update)
  - Delete(?)

/users
  - Get

/user/{id}
  - Post
  - Get
  - Put (update)
  - Delete

## Models

```csharp
public class Challenge
{
    public Guid Id { get; set; }

    public int Year { get; set; }

    public int Target { get; set; }

    public int Completed { get; set; } = 0;

    public User User { get; set; }
}
```

```csharp
public class User
{
    public string Name { get; set; }

    public Challenge[] Challenges { get; set; }
}
```