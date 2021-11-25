# Dotnet Templates

This repository contains our templates for quickly spinning up a new service via the [.NET CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/) that meets our internal standards.

## Templates available

- **ms** - create a Microservice solution

## Usage

`dotnet new ms -n MyProjectName --azureApplicationName NameToBeOnAzure`

---

## Development

### Build

In order to build the templates, navigate into the 'working' directory that contains the 'templatepack.csproj' in a terminal and run the following command
`dotnet pack -c Release`

### Install

In order to install the template pack which we just built run the following command

`dotnet new -i PATH_TO_NUPKG_FILE`

Typically:

`dotnet new -i .\bin\Release\BasicTemplates.1.0.0.nupkg`

Note: On Mac/Linux you might need to swap those `/` to `\`

The template should be now installed on the local machine. Use the below command to see the installed list of templates

- `dotnet new --list`

### Uninstall

In order to uninstall the template pack which you are required to do to install a newer version,
 run the following command

`dotnet new -u BasicTemplates`

### Adding a new template

To add a new template, create a new folder in the `templates` directory.
Inside the new folder you must also add a `.template.config` folder, and in there a `template.json`.
Best to copy this folder from an existing template, and change the values as appropriate.
The `test` directory is used for you to test your new template using

`dotnet new TEMPLATE -n NAMEOFSOLUTION`

---
