# To do API

## Auth controller
- POST ...​/api​/authorization​/register
- POST ...​/api​/authorization​/login

#### Request body
```
{
  "username": "string",
  "password": "string"
}
```
---
## Todo controller
#### Authorization: Bearer + JWT-token
- GET .../api/todo/task/{id}
- GET .../api/todo/tasks
- POST .../api/todo/create{task}
- POST .../api/todo/complete/{id}
- PUT .../api/todo/update/{id}
-DELETE .../api/todo/delete/{id}
---

#### Responses body
```
{
  "data": {},
  "message": "string"
}
```
