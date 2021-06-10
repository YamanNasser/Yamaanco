# Yamaanco
![1](https://user-images.githubusercontent.com/16042726/119264158-9e72d800-bbea-11eb-8e19-d488b8bde8cf.png)

# Introduction 
Today we have moved towards a more socially connected society. The usage of Social Media today is growing faster than any before. It’s spanning from socializing with friends to an involvement between the persons inside the companies and institutions. Social Media is a powerful tool for user engagement. Social Media has been used for service evaluation and understanding of the user’s needs. In addition, it is used by many companies to get their customer feedback and suggestions to build a better product or service, or for improving on some product and services. Furthermore, many companies today use Social Media to enhance the communication and collaboration between their employees who geographically distributed.

Public Social Media such as Facebook, Google, and Twitter become the primary way to user involvement. However, there is still a privacy concern with these public Social Media services. Public Social Networks today have more power than any government. They can know everything about us, what we like, dislike, our secret conversation, how we feel, what we, and who we talk.

# What is Yamaanco?
Yamaanco is a Headless (i.e not connecting directly with a front-end at all) Open Sourced social network Solution. It contains the building block of the starting point of the small social network project. It contains the main features that used to facilitate user interaction and collaboration. Some of these features include posting, comment on the post, real-time chat session, voting, user profile, and focusing groups. Yamaanco has developed with Microsoft modern technology stack, using Asp.net core 5.0.

# Overview
The layering of an application's codebase is a widely accepted technique to help reduce complexity and to improve code reusability. To achieve a layered architecture, Yamaanco  follows the following layer.

## Domain
This  contains all entities, enums , types and logic specific to the domain layer.

## Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. 

## Infrastructure
This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

## WebApi 
These are remote clients that use the application as a service via HTTP APIs . A remote client can be a SPA (Single Page App), a mobile application, or a 3rd-party consumer. 

# Web API Documentation and Features Description
https://bit.ly/3oLAEaf


# Development Requirements
1. PC/Laptop
2. Visual Studio 2019 or above Community Edition (FREE)
3. MS SQL Express Edition (FREE)
4. .NET Core 5.0
5. MS IIS 7/8 or above
6. ASP.NET Core
7. Cup of coffee☕

# Quick Start and Run
<ol>
  <li>Open the project using Visual Studio </li>
  <li>Change Connection string of database: Open appsettings.json file and change the following tag: </li>
  <ul>
    <li>ConnectionStrings tag as below:</li>
    <ul>
      <li>	Data Source: your server name</li>
      <li>Catalog: your database name</li>
      <li>user id: your username</li>
      <li>password: your password</li>
    </ul>
    <li>EmailSettings tag:</li>
    <ul>
      <li>SmtpServer: smtp.gmail.com</li>
      <li>Port: for example Gmail SMTP port (TLS): 587 or Gmail SMTP port (SSL): 465</li>
      <li>EnableSsl:true when port is 465.</li>
      <li>UserName: your email, for example: Your Gmail address (e.g. example@gmail.com).</li>
      <li>Password: your email password. For example Your Gmail password.</li>
      <li>IsBodyHtml: true</li>
    </ul>
  </ul>
  <li>Run Code first Migration Script via Package Manager console</li>
  <ul>
    <li>Open Package Manager Console </li>
    <li>select Yamaanco.Infrastructure.EF.Persistence.MSSQL as Default Project </li>
    <li>run EntityFrameworkCore\Update-Database  -context MsSqlYamaancoDbContext</li>
   <li>run EntityFrameworkCore\Update-Database  -context YamaancoIdentityDbContext </li>
  </ul>
  <li>Set Yamaanco.WebApi as default project</li>
  <li>Run the application and start explore the APIs.</li>
</ol>

# Contributing
Please create issues to report bugs, suggest new functionalities, ask questions or just share your thoughts about the project. We will really appreciate your contribution, thanks.

# License
Distributed under the MIT License.

# Founders
 <a href="https://www.linkedin.com/in/yamannasser/">Yaman Nasser</a> Software Engineer, Palestine

# Support
Has this Project helped you learn something New? or Helped you at work? Do Consider Supporting. Here are a few ways by which you can support.
1. Leave a star! ⭐
2. Recommend this awesome project to your colleagues.



