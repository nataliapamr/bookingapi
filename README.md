# Hello :D 

# Welcome to Booking Api 

## Technologies used on this project:
    - .NET 5
    - Docker
    - MongoDB 

## Before you run the project you will need: 
    - .NET 5 SDK installed
    - Docker-compose installed (if you want to run locally via docker)

## To run the project
    ## Open Visual Studio
        - Set Booking.Api as Startup Project
        - Run :D 

    ## Open Visual Studio Code
        - Go to the Booking.Api folder ('cd .\Booking.Api\')
        - Execute the command: dotnet run
        
        - Open your prefered browser at https://localhost:5001/swagger/

## To publish the api using docker
    - Have Docker installed in your Desktop/Laptop
    - Go to the root folder
    - Execute the command: docker-compose.exe build
    - You should have the image exported to run :D

## In case you want to test in an already created application you can try via the link
- http://booking-api-cancun.herokuapp.com/api/BookARoom/
    ## Endpoints:
        #GET
            - /getAll
            - /getById/{id}
        #POST
            -/book
        #PUT
            - /{id}
        #DELETE
            - /{id}
