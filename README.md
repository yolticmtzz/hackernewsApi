# Hacker News API

## Description

This API provides access to the latest data from Hacker News. It allows you to retrieve new stories and apply search filters.

## Features

- **Get Newest Stories**: Retrieve the most recent stories from Hacker News.
- **Filter Stories by Search Term**: Apply a search filter to the retrieved stories.
- **Pagination**: Supports pagination to navigate through the stories.

## Endpoints

### Get Newest Stories

**GET** /api/Stories/newest

Retrieves the most recent stories from Hacker News.

**Query Parameters:**

- `searchTerm` (optional): Search term to filter stories by title.
- `page` (required): The page number of results.
- `pageSize` (required): Number of stories per page.

**Example Request:**

```http
GET /api/Stories/newest?page=1&pageSize=10&searchTerm=angular

#Steps to Run
