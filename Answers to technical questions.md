**1. How long did you spend on the coding assignment? What would you add to your solution if you had more time? If you didn't spend much time on the coding assignment then use this as an opportunity to explain what you would add.**

I spent about 22 hours on the coding assignment. I’d add middleware for API invoke to record API response time, and then I’d try to find out how much time takes to update the exchange rate of currencies. As well as, I'd use suitable libraries for resilience and transient fault handling (e.g. Polly). Maybe I'd work on a Web UI also.

**2. What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.**

.Net has a wide range of features that make designing, coding and developing more easier. If I want to talk about .Net features in its growth journey as a framework, I can point to the cross-platform ability that lets us develop our applications for any OS without any struggling with codes and frameworks that we had in past. Also, I can point to the features that help us to follow the SOLID principles correctly. For example, the features for dependency injection.
As well as, I think one of the useful feature that was added to the C# 10 is [**Interpolated string handler**](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#interpolated-string-handler) and [**Lambda expression improvements**](https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/csharp-10#lambda-expression-improvements)

Snippet of code for the Interpolated string handler:
```
Console.WriteLine($"Cryptocurrency name: {crypto.Result.Name}\r\nSymbol: {crypto.Result.Symbol}\r\n" + $"Prices:\r\n{string.Join("\r\n", crypto.Result.Prices.Select(d => d.Key + " = " + d.Value).ToArray())} \r\n");
```
and the output is like this:

```
Cryptocurrency name: Bitcoin
Symbol: BTC
Prices:
AUD = 30400.774356
BRL = 101549.859766
EUR = 19695.961437
GBP = 17185.173549
USD = 19689.3655
```

**3. How would you track down a performance issue in production? Have you ever had to do this?**

First, I'd check the logs to find out where the problem occurred. If it's in coding logic, I'd optimize the code or refactor that part of code to improve the performance. If it's related to the Database, I'd optimize the query or stored procedure using execution plan tools.

In my previous company, we received some reports regarding software slowing down in calculating the patient bill. The bill calculator service had many causes to slow down because it related to any type of service that a patient gets during his treatment. I have followed the steps above on the code and then on the database and realized that one of the related tables has a missing index. After fixing this index everything worked correctly.

**4. What was the latest technical book you have read or tech conference you have been to? What did you learn?**

I prefer to participate in the online cources on online learning platforms like [Udemy](https://www.udemy.com), and watch the training videos on Youtube (e.g. [Freecodecamp](https://www.youtube.com/c/Freecodecamp) channel). 

- The last course that I participated is [Domain Driven Design & Microservices for Architects](https://www.udemy.com/course/domain-driven-design-and-microservices/)
- I watched some videos on Youtube to get acquainted with [.Net MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui)

**5. What do you think about this technical assessment?**

It's good. I think the analyzers considered many appropriate skills for me as one who does the assignment as a software engineer position candidate. I tried to do it in an efficient way until the deadline without any stress.

**6. Please, describe yourself using JSON.**

```
{
  "FirstName": "Vahid",
  "LastName": "Rosoukhi Sefidanjadid",
  "Title": "Senior Software Developer",
  "Birthdate": "1987-11-14T00:00:00",
  "Summary": "With a background of 16 years of experience in a diverse range of roles, from decision making and architecting to implementing and deploying in Health, Financial, and Industrial fields. Love working on the back-end side, architectures, establishing infrastructures, code debugging, and struggling with data like a detective!",
  "Contact": {
    "Location": "Istanbul - Turkey",
    "Phone": "+905467121865",
    "Email": "v.rosoukhi@gmail.com",
    "Social": [
      {
        "Name": "LinkedIn",
        "Address": "https://www.linkedin.com/in/vahidrsj"
      },
      {
        "Name": "Github",
        "Address": "https://github.com/vahidrsj"
      }
    ]
  },
  "Skills": {
    "Technologies": [
      ".Net Core",
      ".Net5 & 6",
      "MVC",
      "WCF",
      "SQL",
      "EF",
      "Dapper",
      "REST API",
      "AWS",
      "RabbitMQ",
      "Docker",
      "Kubernetes",
      "Git",
      "Azure",
      "Jira"
    ],
    "Databases": [
      "MS SQL Server",
      "MongoDB",
      "Redis"
    ],
    "Concepts": [
      "Design patterns",
      "OOP",
      "DDD",
      "EDD",
      "SOLID",
      "Scrum",
      "CI / CD",
      "Microservices",
      "Tests"
    ]
  },
  "Languages": [
    {
      "Name": "Azeri",
      "Proficiency": "Native"
    },
    {
      "Name": "Farsi",
      "Proficiency": "Native / Bilingual Proficiency"
    },
    {
      "Name": "Turkish",
      "Proficiency": "Advanced"
    },
    {
      "Name": "English",
      "Proficiency": "Advanced"
    }
  ],
  "Experiences": [
    {
      "CompanyName": "TestOne Teknoloji Çözümleri (testone.com.tr)",
      "Location": "Istanbul - Turkey",
      "Title": "Senior Software Developer",
      "StartDate": "2021-04-11T00:00:00",
      "EndDate": "0001-01-01T00:00:00",
      "CurrentlyWorking": true,
      "Responsibilities": [
        "Refactor old .Net meter reading windows and web projects to .Net 6 with DDD, MVC, EF.",
        "Develop calibration software for transformer tester devices such as digital micro–ohm meters, winding resistance testers, and circuit breaker testers which decreases at least %80 of the calibration process time for each device.",
        "Maintain and continuously develop the CE-Test software for Electrical Safety Testing Equipment used in Home Appliances Factories production line, with UWP, Xamarin, WPF.",
        "Maintain and continuously develop DMP (Data management platform) windows project designed in microservices architecture for electric tester devices.",
        "Design and develop the production software used for production process in TestOne, from ordering to production and follow up after delivery with DDD, Dapper, .Net 6. The TestOne can manage the orders, schedule the productions easily, and has a good view of the current status of production and future plans.",
        "Write the technical documents, design class diagrams, and use case diagrams for old windows and web projects."
      ]
    },
    {
      "CompanyName": "Urom Tarashe Pardazan (utpco.ir)",
      "Location": "Tabriz - Iran",
      "Title": "Senior Software Developer",
      "StartDate": "2012-07-01T00:00:00",
      "EndDate": "2021-04-01T00:00:00",
      "CurrentlyWorking": false,
      "Responsibilities": [
        "Designed and developed Hospital Information System (HIS) from start and maintain all of the subsystems. (DDD, Windows forms, Asp.net MVC, Windows Services, WCF, XAMARIN)",
        "Implemented communication services for other software and the Ministry of Health Services (Web Service, WCF)",
        "Designed databases and optimized queries.",
        "Developed modules for communicating with Laboratory Devices such as Biochemistry and Cell Counter devices.",
        "Designed and developed PACS software for acquiring images from imaging devices in DICOM format. (Windows forms, ASP.net Webforms, WCF, Windows Services)",
        "Refactored Web parts of HIS project using .Net Core.",
        "Reviewed written codes and designed solutions created by the team members to control the quality of codes and fix business and technical problems.",
        "Resolved customer issues by working with the Support team."
      ]
    },
    {
      "CompanyName": "Azarbaijan Software Developers (ASD.ir)",
      "Location": "Tabriz - Iran",
      "Title": "Software Developer",
      "StartDate": "2010-06-01T00:00:00",
      "EndDate": "2012-07-01T00:00:00",
      "CurrentlyWorking": false,
      "Responsibilities": [
        "Established R&D department to talk about products and make decisions to increase their quality.",
        "Designed and implemented new features for Logistics and Inventory software. (ASP.net web forms)",
        "Optimized SQL Queries for Logistics, Inventory and financial projects.",
        "Refactored and developed old windows form software using ASP Web Form and SQL Tuning.",
        "Resolved customer issues by working with the Support team.",
        "Designed and developed a Report generator module for all projects. (ASP.Net web forms, JavaScript, jQuery)",
        "Instructed and coached new developers and Interns."
      ]
    }
  ],
  "Educations": [
    {
      "Degree": "Associate degree",
      "School": "Technical College of Tabriz",
      "FieldOfStudy": "Computer - Software",
      "StartDate": "2005-10-01T00:00:00",
      "EndDate": "2007-10-01T00:00:00"
    },
    {
      "Degree": "High School Diploma",
      "School": "Shahid Beheshti technical school",
      "FieldOfStudy": "Software Technology",
      "StartDate": "2003-10-01T00:00:00",
      "EndDate": "2005-04-01T00:00:00"
    }
  ]
}
```
