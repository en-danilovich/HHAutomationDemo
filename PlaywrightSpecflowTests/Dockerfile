### Build the docker container:
###   docker build -t playwright-tests .
### Run the docker container:
###   docker run playwright-tests
### or -> docker run --rm --ipc=host -v $(pwd)/test-results:/app/test-results playwright-tests


FROM mcr.microsoft.com/playwright/dotnet:v1.47.0-noble

# Set the working directory inside the container
WORKDIR /app

# Copy the entire project into the container
COPY . /app

# Build the project
RUN dotnet build

# Command to run your tests
CMD ["dotnet", "test", "--no-build", "--logger:trx"]
