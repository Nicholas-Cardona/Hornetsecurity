# BACKEND

The original SpaceX-API is no longer functional, so an alternative API provided by The Space Devs (Launch Library 2) was used.

The Space Devs API provides two environments:
- Development: not rate limited, but returns stale data
- Production: limited to 15 requests per hour

To avoid stale data while respecting production rate limits, a background worker process was implemented. This worker runs every 10 minutes and:
- Fetches the 20 most recent launches
- Fetches the next 20 upcoming launches
- Persists the data into a local database

Once the data is stored locally, the rest of the application only queries the internal database, completely removing any dependency on external API rate limits.

### DATA ACCESS

The backend intentionally uses two approaches for data access:
- Entity Framework Core for standard ORM usage
- MySqlConnector with raw SQL for selected queries, used to demonstrate SQL proficiency as required by the task description

## PROJECT STRUCTURE

Hornet.Api
- Provides the main API entry point

Hornet.Worker
- Background service responsible for syncing launch data
  from the external API into the database

Hornet.Domain
- Application-wide DTOs and shared contracts

Hornet.Data
- Database entities and AppDbContext
- Shared by both the API and worker projects


# FRONTEND

The frontend is built using:
- The latest version of Angular
- Tailwind CSS
- Vanilla CSS

The frontend does not make any calls to the external Space Devs API.
All data is retrieved exclusively through the backend API, ensuring:
- No exposure to third-party rate limits
- Consistent and controlled data access
- Clear separation of concerns

Both Tailwind CSS and vanilla CSS are used intentionally to demonstrate flexibility and adaptability with different styling approaches.


# RUNNING THE APPLICATION

The entire project is containerized using Docker.

To run the application locally:

```
docker compose up -d
```

This starts all required services, including:
- Backend API
- Background worker
- Database
- Frontend


KEY HIGHLIGHTS

- Handles external API rate limiting safely and efficiently
- Uses a background worker for scheduled data ingestion
- Demonstrates both ORM-based and raw SQL database access
- Clean separation between API, worker, domain, and data layers