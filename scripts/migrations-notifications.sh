#!/bin/bash

if [ -z "$1" ]; then
    echo "Error: Please provide a migration name."
    exit 1
fi

migration_name=$1
project_path="../src/Modules/Notifications/Hyre.Modules.Notifications.Infrastructure/Hyre.Modules.Notifications.Infrastructure.csproj"
startup_project="../src/Bootstrapper/Hyre.Bootstrapper/Hyre.Bootstrapper.csproj"
context="Hyre.Modules.Notifications.Infrastructure.NotificationsRepositoryContext"
configuration="Debug"
output_dir="Migrations"

echo "Adding migration with name: $migration_name"

if dotnet ef migrations add \
    --project $project_path \
    --startup-project $startup_project \
    --context $context \
    --configuration $configuration "$migration_name" \
    --output-dir $output_dir; then
    echo "Migration added successfully."
else
    echo "Error: Migration failed."
    exit 1
fi
