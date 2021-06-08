# Yamaanco
![1](https://user-images.githubusercontent.com/16042726/119264158-9e72d800-bbea-11eb-8e19-d488b8bde8cf.png)

# What is Yamaanco?

Yamaanco is a Headless social network project designed for general purpose and as starting point of a social network project.
It is a Clean Architecture Solution Template-based upon ASP.NET Core 5.0. Built with Onion Architecture and incorporates the most
essential Packages your Social Netowrk projects will ever need.

# Overview
The layering of an application's codebase is a widely accepted technique to help reduce complexity and to improve code reusability. To achieve a layered architecture, Yamaanco  follows the principles of Domain Driven Design.

There are four fundamental layers in Domain Driven Design (DDD):


## Domain
This  contains all entities, enums , types and logic specific to the domain layer.

## Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. 

## Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

## WebApi
These are remote clients that use the application as a service via HTTP APIs . A remote client can be a SPA (Single Page App), a mobile application, or a 3rd-party consumer. 

# Web API Documentation 

https://bit.ly/3oLAEaf
