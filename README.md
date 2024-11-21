# restAPI
This repository contains a REST API that helps track points and point transactions for the Fetch Backend Internship Challenge. The API contains endpoints to add points, spend points, and get points balance.
## Setup Instructions
1. Clone the repository and navigate to the project
   ```
   git clone https://github.com/bhnguyen76/restAPI.git
   cd restAPI
2. Restore the dependencies
   ```
   dotnet restore
3. Run the project
   ```
   dotnet run
## How to access and use the API via Swagger UI
1. Access the Swagger UI
   * Open your browser and navigate to the Swagger UI at the following URL:
   ```
   http://localhost:8000/swagger
   ```
   * This will open the API documentation and allow you to interact with the API directly through the UI.
2. Execute an Endpoint
   * Find the endpoint you want to test
   * Click on the "Try it out" button next to the desired route. This will unlock the input fields where you can modify the request body for the POST requests.
   * After making any necessary edits to the request, click the "Execute" button to send the request to the API
3. Results
   * Swagger will show you the response from the server, including the status code, response body, and headers.
   * After executing the request, Swagger UI will also display the equivalent curl command that you can use in the terminal or command prompt to run the request externally.
4. Resetting the Request Body (Note)
   * Important: Clicking the "Reset" button will not reset the API itself. It will only reset the request body fields to their default values as defined in the Swagger UI, not affect the server or the API state.
