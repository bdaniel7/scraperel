# scraperel

Job interview test

1. You can use the script scraperel.model/sql/init-database.sql to initialize the database, then apply the EF migrations.
2. You can test the api using the Postman collection from web scraper.postman_collection.json
3. To run in Docker, create-network.sh must be run first, to create the network used by the 3 containers, then bootstrap it with docker-compose up -d --build.
