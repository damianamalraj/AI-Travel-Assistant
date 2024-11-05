# Travelbuddy API

## Instructions

**Note:** Before running the project, ensure that you have .NET 8.0 installed on your machine.

1. **Create an `.env` File**
   Create a `.env` file in the root directory (`backend/API/.env`) of the project with the following content:

   ```json
   // Ask Damian for the values
   ```

2. **Run the Project**
   To run the project, navigate to the `backend/API` directory and execute the following commands:

   ```bash
   cd backend/API
   dotnet run
   ```

## API Documentation

This documentation provides an overview of the available endpoints.

### Register

- **Method:** POST
- **URL:** `https://localhost:7038/register`
- **Request Body (JSON):**

  ```json
  {
    "email": "o@ex.se",
    "password": "Lion123!"
  }
  ```

### Login

- **Method:** POST
- **URL:** `https://localhost:7038/login?useCookies=true`
- **Query Params:**

  - `useCookies`: true

- **Request Body (JSON):**

  ```json
  {
    "email": "o@ex.se",
    "password": "Lion123!"
  }
  ```

**Note:** After a successful login, the response will contain a cookie named `.AspNetCore.Identity.Application`. Copy this cookie value and include it in the `Cookie` header of subsequent requests.

### Login with Google

- **Method:** POST
- **URL:** `https://localhost:7038/api/Auth/login-google`

To log in with Google, paste the above URL in your browser.
After successful login, you'll be redirected to `https://localhost:5173/signin-google`, where the `.AspNetCore.Identity.Application` cookie will be set directly in the browser. Extract its value and use it in the `Cookie` header of subsequent requests.

### Ask AI Assistant

- **Method:** POST
- **URL:** `https://localhost:7038/api/OpenAi/AskAiAssistant`
- **Request Body (JSON):**

  ```json
  {
    "question": "give me restaurant near Gamla Stan",
    "prompt": "" // Optional
  }
  ```

### Get User

- **Method:** GET
- **URL:** `https://localhost:7038/api/Auth/user`

### Update User

- **Method:** PATCH
- **URL:** `https://localhost:7038/api/Auth/user`
- **Request Body (JSON):**

  ```json
  {
    "firstName": "Daniel",
    "lastName": "Amalraj",
    "userName": "damianamalraj",
    "phoneNumber": "0731234567",
    "city": "Stockholm",
    "country": "Sweden"
  }
  ```

### Delete User

- **Method:** DELETE
- **URL:** `https://localhost:7038/api/Auth/user`

### Reset Password

- **Method:** POST
- **URL:** `https://localhost:7038/manage/info`
- **Request Body (JSON):**

  ```json
  {
    "oldPassword": "Lion123!",
    "newPassword": "Tiger123!"
  }
  ```

### Logout

- **Method:** GET
- **URL:** `https://localhost:7038/api/Auth/logout`
