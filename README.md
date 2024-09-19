
# Project Title

A brief description of what this project does and who it's for

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
```
## Steps to RUN 
I added a apiKey  security layer.


-add a secret.json file with this value 
{
  "ApiKey": "youApiKey"
}

##Challenges
  - The implementation of Cahce with pagination , that need to changhe the each new request
#How to solve them 
  - Implement a key to identify the cache pagination
##Opinion about the challenge
  -  I really like this approach , because you need to create something real scenario, the only thing  its very compliceted to limit an scope because when you are developing you think "Hum will be nice if I add this or I think I need to add this"

 

