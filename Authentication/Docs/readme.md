# Notes from ASP.NET Core Authentication

Original code from Raw Coding's YT: https://www.youtube.com/playlist?list=PLOeFnOV9YBa4yaz-uIi5T4ZW3QQGHJQXi

* This example doesn't deal with actually validating a user's username and password. It assumes the user has successfully authenticated with the database. This would typically happen in the first part of the /login call, before establishing `Claims` or a `ClaimsPrincipal`.

* We can add different authorization policies depending on the different grants we expect authenticated users to have. In this example, that is demonstrated by adding a "passport" claim.

* .NET Identity deals with the different significant attributes of a user loosely. A new `ClaimsIdentity` can accept a list of `Claim` key-value pairs to denote that a particular identity carries these properties.