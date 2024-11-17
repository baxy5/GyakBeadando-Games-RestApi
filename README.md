
# Game API Documentation

## Overview

This API allows for the management of games, including retrieving, adding, updating, and deleting game entries.

## Base URL

```
https://localhost:7228/api/Game
```

---

## Endpoints

### Get All Games

**GET** `/games`

- **Description**: Retrieves all games from the database.
- **Response**:
  - **200 OK**: Returns a list of all games.
  - **404 Not Found**: No games found.

---

### Get Game by ID

**GET** `/games/{id}`

- **Description**: Retrieves a specific game by its ID.
- **Parameters**:
  - `id` (int) - The ID of the game.
- **Response**:
  - **200 OK**: Returns the game with the specified ID.
  - **404 Not Found**: Game not found.

---

### Add a New Game

**POST** `/games/add-game`

- **Description**: Adds a new game to the database.
- **Request Body**:
  ```json
  {
    "name": "string",
    "developer": "string",
    "releaseDate": 2024, // (optional)
    "description": "string", // (optional)
    "isPlayed": true // (optional)
  }
  ```
- **Response**:
  - **201 Created**: The game is successfully added.
  - **400 Bad Request**: Invalid or missing data.
- **Example**:
  ```json
  {
    "name": "Game Name",
    "developer": "Developer Name",
    "releaseDate": 2023,
    "description": "A fun game description.",
    "isPlayed": false
  }
  ```

---

### Delete a Game by ID

**DELETE** `/games/delete-game/{id}`

- **Description**: Deletes a game from the database. Games with IDs ≤ 16 (dummy data) cannot be deleted.
- **Parameters**:
  - `id` (int) - The ID of the game.
- **Response**:
  - **204 No Content**: Game successfully deleted.
  - **400 Bad Request**: Cannot delete dummy data (IDs ≤ 16).
  - **404 Not Found**: Game not found.

---

### Update the `isPlayed` Field

**PATCH** `/games/isplayed/{id}`

- **Description**: Updates the `isPlayed` field of a specific game.
- **Parameters**:
  - `id` (int) - The ID of the game.
- **Request Body**:
  ```json
  true or false
  ```
- **Response**:
  - **200 OK**: The field was successfully updated.
  - **404 Not Found**: Game not found.
- **Example**:
  ```json
  true
  ```

---

## Models

### Game

```json
{
  "id": 1,
  "name": "string",
  "developer": "string",
  "releaseDate": 2024,
  "description": "string",
  "isPlayed": true
}
```

| Field         | Type      | Required | Description                           |
|---------------|-----------|----------|---------------------------------------|
| `id`          | int       | Yes      | Unique identifier for the game.       |
| `name`        | string    | Yes      | The name of the game.                 |
| `developer`   | string    | No       | The developer of the game.            |
| `releaseDate` | int       | No       | The release year of the game.         |
| `description` | string    | No       | A brief description of the game.      |
| `isPlayed`    | boolean   | No       | Indicates if the game has been played.|

---

## Error Handling

### Common Errors

- **400 Bad Request**: The request could not be processed due to invalid input or attempts to delete restricted data.
- **404 Not Found**: The specified resource could not be found.
- **500 Internal Server Error**: An unexpected error occurred on the server.

---

## Notes

- The `DELETE` endpoint does not allow deletion of dummy data (IDs ≤ 16).
- Ensure all requests to `POST` and `PATCH` endpoints use `Content-Type: application/json`.
