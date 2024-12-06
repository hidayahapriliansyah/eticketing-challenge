### **Administrator Endpoints**

1. **Login**  
   `POST /api/admin/login`  

    | Request

    Auth: -
    Body Payload:
    ```json
    {
      "email": "string",
      "password": "string"
    }
    ```

    | Response

    HTTP Status: 200 OK
    Body
    ```json
      {
        "status": "success",
        "message": "success login",
        "data": {
          "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
          "role": "admin"
        }
      }
    ```

2. **Create Event**  
   `POST /api/events`  

    | Request  

    Auth: Header Authorization  
    Body Payload:
    ```json
    {
      "name": "string",
      "description": "string",
      "event_date": "string (yyyy-MM-ddTHH:mm:ss)",
      "location": "string",
      "max_participants": "int",
      "additional_info": "string",
      "ticket_price": "int",
      "status": "enum (published, unpublished)"
    }
    ```

    | Response  

    HTTP Status: 201 Created
    ```json
    {
      "status": "success",
      "message": "success to create event",
      "data": {
        "event": {
          "id": "f47ac10b-58cc-4372-a567-0e02b2c3d479",
          "name": "string",
          "description": "string",
          "event_date": "string (yyyy-MM-ddTHH:mm:ss)",
          "location": "string",
          "max_participants": "int",
          "additional_info": "string",
          "ticket_price": "int",
          "status": "enum (published, unpublished)"
        }
      }
    }
    ```

3. **Get Events**  
   `GET /api/events`

   | Request  

    Query Parameters:
    - `status` (optional): Filter status event, bisa "published", "unpublished"  
    - `date` (optional): Filter berdasarkan tanggal  
    Auth: Header Authorization (Optional)

    | Response   

    HTTP Status: 200 OK
    ```json
      {
        "status": "success",
        "message": "success to get events",
        "data": {
          "events": [
            {
              "id": 1,
              "name": "Concert Night",
              "event_date": "2024-12-25T18:00:00",
              "location": "Stadium A",
              "ticket_price": 100000,
              "max_participants": 500,
              "status": "published"
            },
            {
              "id": 2,
              "name": "Tech Expo",
              "event_date": "2024-11-20T09:00:00",
              "location": "Expo Center",
              "ticket_price": 100000,
              "max_participants": 300,
              "status": "unpublished"
            }
          ]
        }
      }
    ```

4. **Get Event Detail**  
   `GET /api/events/{eventId}`  

   | Request  

    Parameters: Event Id  
    Auth: -

    | Response  

    HTTP Status: 200 OK
    ```json
        {
          "status": "success",
          "message": "success to get event detail",
          "data": {
            "id": 1,
            "name": "Concert Night",
            "description": "An exciting music concert",
            "ticket_price": 100000,
            "event_date": "2024-12-25T18:00:00",
            "location": "Stadium A",
            "max_participants": 500,
            "additional_info": "Tickets available online",
            "status": "unpublished"
          }
        }
    ```

5. **Update Event**  
   `PUT /api/events/{eventId}`  

   | Request  
 
   Auth: Header authorization
   Body Payload:
   ```json
   {
     "name": "string",
     "description": "string",
     "event_date": "string (yyyy-MM-ddTHH:mm:ss)",
     "location": "string",
     "ticket_price": "decimal",
     "max_participants": "int",
     "additional_info": "string"
   }
   ```  

    | Response  

    HTTP Status: 200 OK
    ```json
      {
        "status": "success",
        "message": "success to update event",
        "data": {
          "event": {
            "id": 1,
            "name": "Updated Concert Night",
            "description": "An exciting and expanded music concert",
            "event_date": "2024-12-26T19:00:00",
            "location": "Stadium B",
            "ticket_price": 120000,
            "max_participants": 700,
            "additional_info": "VIP tickets available",
            "status": "published"
          }
        }
      }
    ```

6. **Delete Event**  
   `DELETE /api/events/{eventId}`  

    | Request  

    PathParameters: Event Id
    Auth: Header Authorization

    | Responst

    HTTP Status: 200 OK
    ```json
      {
        "status": "success",
        "message": "success to delete event",
      }
    ```

7. **Get Tickets**  
   `GET /api/tickets`  

    | Request  

    Auth: Header Authorization
    Body Payload: Tidak ada  
    Query Parameters:
    - `status` (optional): Filter status ticket, bisa "expired", "confirmed", "pending", "cancelled"
    - `date` (optional): Filter berdasarkan tanggal  
    - `page` (optional): Halaman
    - `limit` (optional): Limit setiap halaman

    | Response  

    HTTPS Status: 200 OK
    ```json
      {
        "status": "success",
        "message": "success to get ticket event",
        "data": {
          "ticket": [
            {
              "ticketId": 1,
              "status": "confirmed",
              "user": {
                "id": "sdfsdf",
                "name": "John Doe"
              },
              "valid_until": "2024-12-25T23:59:59",
              "event": {
                "id": "",
                "name": "Event name"
              },
              "code": "23834824293489"
            },
            {
              "ticketId": 2,
              "user": {
                "id": "sdfsdf",
                "name": "John Doe"
              },
              "status": "expired",
              "valid_until": "2024-11-01T23:59:59",
              "event": {
                "id": "",
                "name": "Event name"
              },
              "code": "23834824293489"
            }
          ]
        }
      }
    ```


8. **Get Ticket Detail**  
   `GET /api/tickets/{ticketId}`  

    | Request  

    Path Parameters: Ticket Id
    Auth: Header Authorization

    | Response  

    HTTP Status: 200 OK
    ```json
    {
      "status": "success",
      "message": "success to get ticket detail",
      "data": {
        "ticket": {
          "id": 1,
          "code": "23834824293489",
          "event": {
            "id": 1,
            "name": "Updated Concert Night",
            "description": "An exciting and expanded music concert",
            "event_date": "2024-12-26T19:00:00",
            "location": "Stadium B",
            "ticket_price": 120000,
            "max_participants": 700,
            "additional_info": "VIP tickets available",
            "status": "published"
          },
          "user": {
            "id": "",
            "name": "John Doe",
            "email": "johndoeemail@gmail.vom"
          },
          "status": "confirmed",
          "valid_until": "2024-12-25T23:59:59",
          "price": 120000,
          "created_at": "2024-12-25T23:59:59",
        }
      }
    }
    ```


9. **Update Ticket**  
    `PUT /api/tickets/{ticketId}`  

    Auth: Ya (Header Authorization dengan JWT Token) 
    Query Parameters: Tidak ada  
    Body Payload:
    ```json
    {
      "status": "string (confirmed/canceled/expired)"
    }
    ```  

---

### **Customer Endpoints**

1. **Register**  
   `POST /api/customer/register`  

    | Request

    Auth: Tidak perlu
    Body Payload:
    ```json
    {
      "name": "string",
      "email": "string",
      "username": "string",
      "password": "string"
    }
    ```  

    | Response  

    HTTP Status: 201 Created
    ```json
      {
        "status": "success",
        "message": "success register",
        "data": {
          "userId": "23rjefusafwerhu"
        }
      }
    ```

2. **Login**  
   `POST /api/customer/login`  

    | Request  

    Auth: Tidak perlu  
    Body Payload:
    ```json
    {
      "email": "string",
      "password": "string"
    }
    ```  
    | Response  

    HTTP Status: 200 OK
    ```json
      {
        "status": "success",
        "message": "success register",
        "data": {
          "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
          "role": "customer"
        }
      }
    ```

3. **Browse Event**  
   `GET /api/events`  

   | Request  

    Query Parameters:
    - `status` (optional): Filter status event, bisa "published", "unpublished"  

    | Response  

    HTTP Status: 200 OK
    ```json
        {
          "status": "success",
          "message": "success get events",
          "data": {
            "events": [
              {
                "id": 1,
                "name": "Concert Night",
                "event_date": "2024-12-25T18:00:00",
                "location": "Stadium A",
                "ticket_price": 100000,
                "max_participants": 500,
                "status": "published"
              },
              {
                "id": 2,
                "name": "Tech Expo",
                "event_date": "2024-11-20T09:00:00",
                "location": "Expo Center",
                "ticket_price": 50000,
                "max_participants": 300,
                "status": "unpublished"
              }
            ]
          }
        }
    ```

4. **Get Tickets**  
   `GET /api/tickets`  

    | Request  

    Auth: Header Authorization
    Query Parameters:
      - `status` (optional): Filter status ticket, bisa "expired", "confirmed", "pending", "cancelled"
      - `page` (optional): Halaman
      - `limit` (optional): Limit setiap halaman  

    | Response  

    HTTP Status: 200 OK
    ```json
      {
        "status": "success",
        "message": "success to get tickets",
        "data": {
          "tickets": [
            {
              "id": 1,
              "userId": 5,
              "status": "confirmed",
              "valid_until": "2024-12-25T23:59:59"
            }
          ]
        }
      }
    ```

5. **Get Ticket Detail**  
   `GET /api/tickets/{ticketId}`  

    | Request  

    Path Parameters: Ticket Id
    Auth: Header Authorization

    | Response  

    HTTP Status: 200 OK
    ```json
    {
      "status": "success",
      "message": "success to get ticket detail",
      "data": {
        "ticket": {
          "id": 1,
          "event": {
            "id": 1,
            "name": "Updated Concert Night",
            "description": "An exciting and expanded music concert",
            "event_date": "2024-12-26T19:00:00",
            "location": "Stadium B",
            "ticket_price": 120000,
            "max_participants": 700,
            "additional_info": "VIP tickets available",
            "status": "published"
          },
          "user": {
            "id": "",
            "name": "John Doe",
            "email": "johndoeemail@gmail.vom"
          },
          "status": "confirmed",
          "valid_until": "2024-12-25T23:59:59"
        }
      }
    }
    ```

6. **Checkout Ticket**  
   `POST /api/checkout`  

    | Request  

    Auth: Header Authorization
    Body Payload:
    ```json
      {
        "eventId": "int",
        "ticket_count": "int",
      }
    ```  

    | Response

    HTTP Status: 201 OK
    ```json
      {
        "status": "success",
        "message": "success to checkout ticket"
      }
    ```
