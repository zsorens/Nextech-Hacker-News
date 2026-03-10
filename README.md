### Nextech-Hacker-News-Coding-Challenge
Interview project


## URLs for hosted copy
UI: https://agreeable-glacier-00853ff0f.2.azurestaticapps.net/
API: https://hacker-news-challenge-api.azurewebsites.net (can add swagger/index.html)

## Running Locally
To run locally, you can use the key bind cmd (cntrl) + shift + b (this will start both applications)
    - make sure that the configuration file locally is pointed to the correct URL where the API is running locally 
    - it is defaulted to localhost:5200


## Automated Test


# UI Tests
    - app.spec.ts
        - Should create the app
        - Should render title
        - Should load stories on init
        - Should start on page 1
        - Should increment page on nextPage()
        - Should not go below page 1 on prevPage()
        - Should filter stories by search query
        - Should show all stories when search is empty
        - Should set error on API failure
    - hacker-news-api.proxy.spec.ts
        - Should call getNewStories with correct params
        - Should call getItem with Correct id

# API Tests
    - Unit Tests
        - HackerNewsClient
            - GetItemAsync_ReturnsItem_WhenIdIsValid
            - GetItemAsync_ReturnsNull_WhenItemDoesNotExist
            - GetTopStoryIdsAsync_ReturnsIds
    - Integration Tests
        - HackerNewsClient
            - GetNewStories_ReturnsOk
            - GetItem_ReturnsNotFound_WhenIdInvalid
            - GetNewStories_ReturnsPaginatedResults

# Run API Tests
For unit tests for the api from the base directory run 
 ''' dotnet test backend.Tests/Unit/Unit.csproj ''' 

For integration tests for the api from the base directory run 
 ''' dotnet test backend.Tests/Integration/Integration.csproj '''

# Run UI Tests
For testing in the UI navigate to the directory
From the base directory: 
    - cd frontend/Hacker-News-App
    - ng test --watch=false  
        - the two test files built in Angular are the spec.ts files
        - Both the app.spec.ts and the hacker-news-api.proxy.spec.ts contain tests