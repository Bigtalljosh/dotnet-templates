# Introduction

This is a tempalted subscriber which meets our internal standards.
You can use this template to kickstart the development of your new subscriber.

Detailed explanation of the different aspects of this template can be found in Documentation/Details.md

---

## What does this template give me?

Out of the box you now have set up and working:

- Logging
- Application Insights
- Telemetry
- CI/CD Pipelines
- SonarCloud Analysis*
- Example Handler

---

## Configuration

This project does not yet use the Azure config stores for it configuration values.
This project currently just uses the traditional `appsetings.json` in the Configuration folder.

---

## Build and Test

When you first create the template, best to hit build and make sure everything is fine. The template should spin up the example subscriber for you.

The template also takes care of creating your CI/CD Pipelines for you. You should have the `build-and-deploy.yml` and `build-and-test.yml` files in the pipelines folder. The only thing you should need to do here in Azure DevOps is actually create a new Pipeline and point it to those .yml files.

\* If you want the SonarCloud analysis to work you will need to create the project in SonarCloud, the same name as the application is very important, that's the project key.

---

## Support

If you're having any issues with this template, please post in the #architecture-guild channel in Slack.
